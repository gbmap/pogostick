using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Billboard))]
public class BillboardRandomize : MonoBehaviour
{
    public Texture[] textures;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Billboard>().texture = textures[Random.Range(0, textures.Length)];
        Destroy(this);
    }
}
