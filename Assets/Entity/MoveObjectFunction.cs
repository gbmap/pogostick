using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectFunction : MonoBehaviour
{
    public float Amplitude = 0.1f;
    public float Frequency = 1.25f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localPosition = Vector3.up * Mathf.Sin(Time.time * Frequency) * Amplitude;
    }
}
