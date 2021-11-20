using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsDrawMesh : MonoBehaviour
{
    public Material material;
    public MeshFilter meshFilter;


    // Update is called once per frame
    void Update()
    {
        Graphics.DrawMesh(
            meshFilter.mesh, 
            transform.position, 
            transform.rotation,
            material,
            LayerMask.NameToLayer("Default"),
            Camera.main.transform.GetChild(0).GetComponent<Camera>()
        );
    }
}
