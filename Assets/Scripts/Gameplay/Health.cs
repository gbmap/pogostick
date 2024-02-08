using Frictionless;
using UnityEngine;

public struct DamageInfo
{
    public GameObject Object;
    public Vector3 Direction;
    public int Damage;
}

public class OnEntityTakeDamage
{
    public Health health { get; private set; }
    public OnEntityTakeDamage(Health health)
    {
        this.health = health;
    }
}

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public bool Invincible;

    public System.Action<DamageInfo> OnTakeDamage;
    public System.Action<DamageInfo> OnDeath;

    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(GameObject obj, int damage)
    {
        DamageInfo dmgInfo = new DamageInfo
        {
            Object = obj,
            Direction = (obj.transform.position-transform.position).normalized,
            Damage = damage
        };

        if (!Invincible)
        {
            CurrentHealth -= damage;

            OnTakeDamage?.Invoke(dmgInfo);
            MessageRouter.RaiseMessage(new OnEntityTakeDamage(this));

            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke(dmgInfo);
                Destroy(gameObject);
            }
        }

        //rigidbody.AddForce(dmgInfo.Direction * damage * 5, ForceMode.Impulse);
    }
}
