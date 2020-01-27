using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trauma
{
    [Range(0f, 1f)]
    public float Value;
    public float DecayPerSecond = 1f;

    public float Shake { get { return Value * Value; } }

    public void Update()
    {
        Value = Mathf.Clamp01(Value - DecayPerSecond * Time.deltaTime);
    }
}
