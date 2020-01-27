using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class RampMeshGenerator : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    Vector3[] vertices;
    int[] triangles;

    [Range(1, 16)]
    public int RampSmoothSegments;

    // Start is called before the first frame update
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        CreateRamp();
    }

    public void CreateRamp()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform point = transform.GetChild(i);

            Vector3 transformVert(Transform p, Vector3 v)
            {
                v.x *= p.localScale.x;
                v.y *= p.localScale.y;
                v.z *= p.localScale.z;

                v = Matrix4x4.Rotate(p.localRotation).MultiplyPoint(v);

                v += p.localPosition;
                return v;
            }

            Vector3 a = transformVert(point, new Vector3(0, 1, 0));
            Vector3 b = transformVert(point, new Vector3(1, 0, 0));
            Vector3 c = transformVert(point, new Vector3(-1, 0, 0));

            vertices.Add(a);
            uvs.Add(new Vector2(0.5f, 1f));
            normals.Add(Vector3.up);

            vertices.Add(b);
            uvs.Add(new Vector2(0.0f, 0f));
            normals.Add(Vector3.up + Vector3.right);

            vertices.Add(c);
            uvs.Add(new Vector2(1.0f, 0f));
            normals.Add(Vector3.up + Vector3.left);

            if (i == 0)
            {
                /* cap */
                triangles.Add(i * 3);
                triangles.Add(i * 3 + 1);
                triangles.Add(i * 3 + 2);
            }
            else
            {
                /* top faces */
                triangles.Add(i * 3 - 3);
                triangles.Add(i * 3);
                triangles.Add(i * 3 - 2);

                triangles.Add(i * 3 - 2);
                triangles.Add(i * 3);
                triangles.Add(i * 3 + 1);

                triangles.Add(i * 3);
                triangles.Add(i * 3 - 3);
                triangles.Add(i * 3 + 2);

                triangles.Add(i * 3 + 2);
                triangles.Add(i * 3 - 3);
                triangles.Add(i * 3 - 1);

                /* bottom cap */
                triangles.Add(i * 3 - 2);
                triangles.Add(i * 3 + 1);
                triangles.Add(i * 3 + 2);

                triangles.Add(i * 3 + 2);
                triangles.Add(i * 3 - 1);
                triangles.Add(i * 3 - 2);
            }

            if (i == transform.childCount - 1)
            {
                /* end cap */
                triangles.Add(i * 3 + 2);
                triangles.Add(i * 3 + 1);
                triangles.Add(i * 3);
            }
        }

        mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        //mesh.RecalculateNormals();
        //meshFilter.mesh = mesh;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }

    public void SmoothRamp()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == transform.childCount - 1) break;

            Transform a = transform.GetChild(i);
            Transform b = transform.GetChild(i + 1);

            for (int j = 1; j < RampSmoothSegments + 1; j++)
            {
                float t = ((float)j) / (RampSmoothSegments + 1);

                /* Linear */
                Vector3 p = Vector3.Lerp(a.localPosition, b.localPosition, t);
                //p = LinearInterp(a.localPosition, b.localPosition, t);
                p = QuadraticInterp(a.localPosition, a.localPosition + a.forward, b.localPosition, t);
                Vector3 r = Quaternion.Lerp(a.localRotation, b.localRotation, t).eulerAngles;
                Vector3 s = Vector3.Lerp(a.localScale, b.localScale, t);

                GameObject go = new GameObject("Point" + (i + 1));
                go.transform.parent = transform;
                go.transform.SetSiblingIndex(i + 1);
                go.transform.localPosition = p;
                go.transform.localRotation = Quaternion.Euler(r);
                go.transform.localScale = s;
                i++;
            }
        }
    }

    private Vector3 LinearInterp(Vector3 a, Vector3 b, float t)
    {
        return a + t * (b - a);
    }

    private Vector3 QuadraticInterp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * a;
        p += 2 * u * t * b;
        p += tt * c;
        return p;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(child.position, 2f);
            if (i < transform.childCount - 1)
            {
                Transform childB = transform.GetChild(i + 1);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(child.position, childB.position);
            }
        }
    }

    public Vector3 WorldToGuiPoint(Vector3 position)
    {
        var guiPosition = Camera.main.WorldToScreenPoint(position);
        guiPosition.y = Screen.height - guiPosition.y;

        return guiPosition;
    }
}
