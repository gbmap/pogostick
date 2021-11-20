using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DRAP.Level.Generator {
    public class RampGenerator : MonoBehaviour
    {
        public MeshFilter meshFilter;


        [Range(2, 100)]
        public int divisions = 3;

        public static int GetVertexCount(int divisions, int baseShapeVertexCount)
        {
            return (2 + divisions)*baseShapeVertexCount;
        }

        public static int GetTriangleCount(int divisions, int baseShapeVertexCount)
        {
            return 2 + (divisions)*6;
        }


        // Start is called before the first frame update
        void Start()
        {
            meshFilter.mesh = Generate(
                divisions,
                transform.GetChild(0).position,
                transform.GetChild(0).GetChild(0).position,
                transform.GetChild(1).position,
                transform.GetChild(1).GetChild(0).position
            );
        }

        public void Generate()
        {
            meshFilter.mesh = Generate(
                divisions,
                transform.GetChild(0).position,
                transform.GetChild(1).position,
                transform.GetChild(0).GetChild(0).position,
                transform.GetChild(1).GetChild(0).position
            );
        }

        public Mesh Generate(
            int divisions, 
            Vector3 a, 
            Vector3 b, 
            Vector3 ca, 
            Vector3 cb
        ) {
            Vector3[] vertices = new Vector3[GetVertexCount(divisions, 3)];
            Vector2[] uvs1 = new Vector2[vertices.Length];
            Vector2[] uvs2 = new Vector2[vertices.Length];
            int[] triangles = new int[GetTriangleCount(divisions, 3)*3];

            // Vertex counter
            int ii = 0;
            for (int i = 0; i <= divisions; i++)
            {
                float t = (((float)i) / divisions);
                Vector3 p = SamplePoint(a, ca, b, cb, t);

                Vector3 fwd = Vector3.Normalize(SamplePoint(a, ca, b, cb, t+0.01f) - p);
                Vector3 right = Vector3.Normalize(Vector3.Cross(Vector3.up, fwd));
                Vector3 up = Vector3.Normalize(Vector3.Cross(fwd, right));


                int iv1 = i*3;
                int iv2 = i*3+1; 
                int iv3 = i*3+2;

                // Generate triangle
                vertices[iv1] = p+up;
                vertices[iv2] = p+right;
                vertices[iv3] = p-right;

                // Generate UVs (1)
                // x follows t from 0 to 1.
                uvs1[iv1] = new Vector2(t, 1f);
                uvs1[iv2] = new Vector2(t, 0f);
                uvs1[iv3] = new Vector2(t, 0f);

                // Generate UVs (2)
                //  x is in [0,1] for each subdivision.
                //  useful for textures
                float x = i % 2 == 0 ? 0f : 1f;
                uvs2[iv1] = new Vector2(x, 1f);
                uvs2[iv2] = new Vector2(x, 0f);
                uvs2[iv3] = new Vector2(x, 0f);

                // Generate triangles
                if (i == 0)
                {
                    /* cap */
                    triangles[ii++] = iv1;
                    triangles[ii++] = iv2;
                    triangles[ii++] = iv3;
                }
                else
                {
                    /* top faces */
                    triangles[ii++] = iv1-3;
                    triangles[ii++] = iv1;
                    triangles[ii++] = iv1-2;

                    triangles[ii++] = iv1-2;
                    triangles[ii++] = iv1;
                    triangles[ii++] = iv2;

                    triangles[ii++] = iv1;
                    triangles[ii++] = iv1-3;
                    triangles[ii++] = iv3;

                    triangles[ii++] = iv3;
                    triangles[ii++] = iv1-3;
                    triangles[ii++] = iv1-1;

                    /* bottom cap */
                    triangles[ii++] = iv1-2;
                    triangles[ii++] = iv2;
                    triangles[ii++] = iv3;

                    triangles[ii++] = iv3;
                    triangles[ii++] = iv1-1;
                    triangles[ii++] = iv1-2;
                }
                if (i == divisions)
                {
                    // End cap should be
                    // build counter-clockwise
                    triangles[ii++] = iv1;
                    triangles[ii++] = iv3;
                    triangles[ii++] = iv2;
                }

            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.uv = uvs1;
            mesh.uv2 = uvs2;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            return mesh;
        }

        private void GenerateVertices()
        {
        }

        Vector3 SamplePoint(
            Vector3 a, 
            Vector3 ca,
            Vector3 b, 
            Vector3 cb, 
            float t
        ) {
            return DRAP.Utils.Interp.Cubic(a, ca, cb, b, t);
            // return Vector3.Lerp(a, b, t);
        }

    }
}
