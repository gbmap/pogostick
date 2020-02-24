using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class DamageHandler : MonoBehaviour
{
    protected Health health;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        health.OnTakeDamage += OnTakeDamage;
        health.OnDeath += OnDeath;
    }

    protected virtual void OnDisable()
    {
        health.OnTakeDamage -= OnTakeDamage;
        health.OnDeath -= OnDeath;
    }

    protected virtual void OnTakeDamage(DamageInfo msg) { }
    protected virtual void OnDeath(DamageInfo msg) { }

}

public class SpawnParticlesOnDamage : DamageHandler
{
    public EParticle particle;

    [Header("Per Damage")]
    public int Count = 10;

    [Header("Per Damage")]
    public bool ScaleFromDamage;
    public int ParticlePerDamage = 1;

    protected override void OnTakeDamage(DamageInfo msg)
    {
        int count = ScaleFromDamage ? msg.Damage * ParticlePerDamage : Count;
        FX.Instance.EmitParticles(particle, transform.position, count); 
    }
}
