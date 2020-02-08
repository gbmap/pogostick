using System;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    public Bounds Bounds { get; private set; }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Bounds = GetBounds();
        Instantiate(Resources.Load("UIGameplay"));
    }
       
    private void OnDrawGizmos()
    {
        var b = GetBounds();   
        Gizmos.DrawWireCube(b.center, b.extents*2f);
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
        b.Expand(2f);
        return b;
    }

    public bool IsInsideLevel(Vector3 worldPos)
    {
        //Vector3 pos = transform.InverseTransformPoint(worldPos);
        return Bounds.Contains(worldPos);
    }
}
