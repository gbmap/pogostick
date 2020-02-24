using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public int Damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(gameObject, Damage);
        }
        else
        {
            FX.Instance.EmitSparks(transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(gameObject, Damage);
        }
        else
        {
            var contact = collision.GetContact(0);
            FX.Instance.EmitSparks(contact.point, Quaternion.Euler(contact.normal));
        }

        Destroy(gameObject);
    }
}
