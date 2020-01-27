using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FX : Singleton<FX>
{
    public ParticleSystem BloodSplatter;
    public ParticleSystem Sparks;

    public void EmitBloodSplatter(Vector3 position, int count=100)
    {
        BloodSplatter.transform.position = position;
        BloodSplatter.Emit(Random.Range(count-20, count+20));
    }

    public void EmitSparks(Vector3 position, Quaternion direction)
    {
        Sparks.transform.position = position;
        Sparks.transform.rotation = direction;
        Sparks.Emit(Random.Range(5, 10));
    }
}
