using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BlinkOnDamage : MonoBehaviour
{
    Health health;
    MeshRenderer renderer;

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

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HitFactor > 0f)
        {
            HitFactor = Mathf.Clamp01(HitFactor - Time.deltaTime * 3f);
        }
    }

    private void OnEnable()
    {
        if (health)
        {
            health.OnTakeDamage += OnShotCallback;
        }
    }

    private void OnDisable()
    {
        if (health)
        {
            health.OnTakeDamage -= OnShotCallback;
        }
    }

    private void OnShotCallback(DamageInfo obj)
    {
        HitFactor = 1f;
    }
}
