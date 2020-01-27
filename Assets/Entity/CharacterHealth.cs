using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public Vector3 Direction;
    public int Damage;
}

public class CharacterHealth : MonoBehaviour
{
    public int Health;

    public System.Action<DamageInfo> OnTakeDamage;
    public System.Action<DamageInfo> OnDeath;

    Billboard billboard;
    Rigidbody rigidbody;

    private void Awake()
    {
        billboard = GetComponentInChildren<Billboard>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(GameObject obj, int damage)
    {
        DamageInfo dmgInfo = new DamageInfo
        {
            Direction = (obj.transform.position-transform.position).normalized,
            Damage = damage
        };

        Health -= damage;

        OnTakeDamage?.Invoke(dmgInfo);

        if (billboard)
        {
            billboard.HitFactor = 1f;
        }

        FX.Instance.EmitBloodSplatter(transform.position, 75);

        if (Health < 0)
        {
            FX.Instance.EmitBloodSplatter(transform.position, 150);
            OnDeath?.Invoke(dmgInfo);
            Destroy(gameObject);
        }

        rigidbody.AddForce(dmgInfo.Direction * damage * 5, ForceMode.Impulse);
    }
}
