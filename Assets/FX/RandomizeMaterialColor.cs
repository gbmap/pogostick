using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeMaterialColor : MonoBehaviour
{
    public Color colorA;
    public Color colorB;

    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<MeshRenderer>();
        renderer.material.SetColor("_BaseColor", Color.Lerp(colorA, colorB, Random.value));
        Destroy(this);
    }
}
