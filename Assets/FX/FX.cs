using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum EParticle
{
    Blood,
    Spark
}

public class FX : Singleton<FX>
{
    public ParticleSystem BloodSplatter;
    public ParticleSystem Sparks;

    private void Awake()
    {
        m_ShuttingDown = false;
    }

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

    public void EmitParticles(EParticle particleType, Vector3 position, int count)
    {
        EmitParticles(particleType, position, Quaternion.identity, count);
    }

    public void EmitParticles(EParticle particleType, Vector3 position, Quaternion rotation, int count)
    {
        ParticleSystem ps = GetParticleSystem(particleType);
        Emit(ps, position, rotation, count);
    }

    private void Emit(ParticleSystem ps, Vector3 position, Quaternion rotation, int count)
    {
        ps.transform.position = position;
        ps.transform.rotation = rotation;
        ps.Emit(count);
    }

    private ParticleSystem GetParticleSystem(EParticle particleType)
    {
        switch(particleType)
        {
            case EParticle.Blood: return BloodSplatter;
            case EParticle.Spark: return Sparks;
            default: return Sparks;
        }
    }
}
