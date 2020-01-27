using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceOnAwake : MonoBehaviour
{
    public bool Randomize;
    public float Strength;
    public Vector3 Direction;

    // Start is called before the first frame update
    void Awake()
    {
        Direction = Randomize ? (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))+Direction) : Direction;
        GetComponent<Rigidbody>().velocity = Direction * Strength;
        Destroy(this);
    }
}
