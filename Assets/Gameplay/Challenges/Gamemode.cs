using System;
using UnityEngine;

public enum EBoundsSide
{
    Top = 0, Bot, Left, Right, Front, Back
}

public class Gamemode : MonoBehaviour
{
    public Bounds Bounds { get; private set; }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Bounds = GetBounds();
        Instantiate(Resources.Load("UIGameplay"));
        Instantiate(Resources.Load("Managers"));
    }

    private void OnDrawGizmos()
    {
        var b = GetBounds();
        Gizmos.DrawWireCube(b.center, b.extents * 2f);
    }

    private Bounds GetBounds()
    {
        Bounds b = new Bounds();
        var meshes = GetComponentsInChildren<MeshRenderer>();
        Array.ForEach(meshes, m => b.Encapsulate(m.bounds));

        //b.center = transform.position;
        b = ExpandBounds(b);
        return b;
    }

    private Bounds ExpandBounds(Bounds b)
    {
        b.extents = new Vector3(Mathf.Max(100f, b.extents.x), Mathf.Max(50f, b.extents.y), Mathf.Max(100f, b.extents.z));
        b.Expand(100f);
        return b;
    }

    public bool IsInsideLevel(Vector3 worldPos, EBoundsSide[] sides)
    {
        return IsInsideLevel(worldPos, CreateBoundsMask(sides));
    }

    public bool IsInsideLevel(Vector3 worldPos, int mask = -1)
    {
        //Vector3 pos = transform.InverseTransformPoint(worldPos);
        bool inside = Bounds.Contains(worldPos);
        if (mask == 0b0000_0000_0111_1110 || mask == -1)
            return inside;
        else
        {
            if ( (mask & CreateBoundsMask(EBoundsSide.Top)) > 0)
            {
                return worldPos.y < Bounds.center.y + Bounds.extents.y;
            }
            else if ((mask & CreateBoundsMask(EBoundsSide.Bot)) > 0)
            {
                return worldPos.y > Bounds.center.y - Bounds.extents.y;
            }
            else if ((mask & CreateBoundsMask(EBoundsSide.Back)) > 0)
            {
                return worldPos.z > Bounds.center.z - Bounds.extents.z;
            }
            else if ((mask & CreateBoundsMask(EBoundsSide.Front)) > 0)
            {
                return worldPos.z < Bounds.center.z + Bounds.extents.z;
            }
            else if ((mask & CreateBoundsMask(EBoundsSide.Left)) > 0)
            {
                return worldPos.x > Bounds.center.x - Bounds.extents.x;
            }
            else if ((mask & CreateBoundsMask(EBoundsSide.Right)) > 0)
            {
                return worldPos.x < Bounds.center.x + Bounds.extents.x;
            }
            return inside;
        }
    }

  

    public int CreateBoundsMask(EBoundsSide[] sides)
    {
        int m = 0;
        for (int i = 0; i < sides.Length; i++)
        {
            m |= CreateBoundsMask(sides[i]);
        }
        return m;
    }

    public int CreateBoundsMask(EBoundsSide boundSide)
    {
        int mask = 1 << (int)boundSide;
        return mask;
    }
}
