using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
public class SpawnGibOnDeath : MonoBehaviour
{
    public GameObject Gib;
    public int Count;

    CharacterHealth health;

    // Start is called before the first frame update
    private void Awake()
    {
        health = GetComponent<CharacterHealth>();
    }

    private void OnEnable()
    {
        health.OnDeath += OnDeathCallback;
    }

    private void OnDisable()
    {
        health.OnDeath -= OnDeathCallback;
    }

    private void OnDeathCallback(DamageInfo obj)
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate(Gib, transform.position, Quaternion.identity);
        }
    }
}
