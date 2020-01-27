using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeScale : MonoBehaviour
{
    public float scaleFactor = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = transform.localScale * Random.Range(1f, 1f+scaleFactor);
        Destroy(this);
    }
}
