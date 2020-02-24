using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    public Texture texture;

    [Header("Scaling")]
    public bool resizeToTexture;
    public float ScaleFactor = 1f;

    [Header("Collider Scaling")]
    public bool scaleCollider;
    private CapsuleCollider collider;
    public float colliderScaleFactor = 0.1f;

    const int pixelsPerUnit = 412;
    Texture lastTexture;

    MeshRenderer renderer;

    Health health;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponentInParent<CapsuleCollider>();
        health = GetComponentInParent<Health>();
    }

    private void ResizeToTexture()
    {
        if (resizeToTexture)
        {
            transform.localScale = new Vector3(((float)texture.width / pixelsPerUnit), ((float)texture.height / pixelsPerUnit), 1f) * ScaleFactor;
        }
    }

    private void ResizeCollider()
    {
        if (scaleCollider)
        {
            //collider.height
            var extents = renderer.bounds.size;
            collider.height = extents.y - colliderScaleFactor;
            //extents.Scale(transform.localScale);
        }
    }

    private void Update()
    {
        if (texture != null && texture != lastTexture)
        {
            Material m = Application.isPlaying ? renderer.material : renderer.sharedMaterial;
            m.SetTexture("_MainTex", texture);

            ResizeToTexture();
            ResizeCollider();
        }

        lastTexture = texture;

        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }

    private void OnDrawGizmos()
    {
        if (renderer != null)
        {
            var extents = renderer.bounds.size;
            //extents.Scale(transform.localScale);
            Gizmos.DrawWireCube(renderer.bounds.center, extents);
        }
    }

}
