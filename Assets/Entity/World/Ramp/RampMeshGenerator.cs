using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Path))]
public class RampMeshGenerator : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    MeshRenderer renderer;

    Vector3[] vertices;
    int[] triangles;

    [Range(1f, 100f)]
    public float scale = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        renderer = GetComponent<MeshRenderer>();
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

            Vector3 a = transformVert(point, new Vector3(0, 1, 0)*scale);
            Vector3 b = transformVert(point, new Vector3(1, 0, 0) * scale);
            Vector3 c = transformVert(point, new Vector3(-1, 0, 0) * scale);

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

    private void OnDrawGizmos()
    {
        var r = renderer;
        if (r == null) r = GetComponent<MeshRenderer>();
        if (r != null)
        {
            var extents = r.bounds.size;
            //extents.Scale(transform.localScale);
            Gizmos.DrawWireCube(r.bounds.center, extents);
        }
    }
}
