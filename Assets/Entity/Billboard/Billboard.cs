using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    public Texture texture;
    public bool resizeToTexture;

    public float ScaleFactor = 1f;
    //public float Rotation = 0f;


    const int pixelsPerUnit = 412;
    Texture lastTexture;

    MeshRenderer renderer;

    CharacterHealth health;

    float hitFactor;
    public float HitFactor
    {
        get { return hitFactor; }
        set
        {
            hitFactor = Mathf.Clamp01(value);
            renderer.material.SetFloat("_HitFactor", hitFactor);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();    
    }

    private void Update()
    {
        if (resizeToTexture)
        {
            transform.localScale = new Vector3(((float)texture.width/pixelsPerUnit), ((float)texture.height / pixelsPerUnit), 1f) * ScaleFactor;
        }

        if (texture != null && texture != lastTexture)
        {
            Material m = Application.isPlaying ? renderer.material : renderer.sharedMaterial;
            m.SetTexture("_MainTex", texture);
        }

        lastTexture = texture;

        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);

        if (!Mathf.Approximately(HitFactor, 0f))
        {
            HitFactor -= Time.deltaTime * 3f;
        }
    }

}
