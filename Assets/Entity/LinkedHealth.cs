using System;

public class LinkedHealth : Health
{
    public Health Target;
    public bool LinkMaxHealth;

    private void Awake()
    {
        if (Target == null)
        {
            throw new Exception("LinkedHealth has no target.");
        }

        if (LinkMaxHealth)
        {
            CurrentHealth = Target.CurrentHealth;
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        Target.OnTakeDamage += OnDamageCallback;
    }

    void OnDisable()
    {
        Target.OnTakeDamage -= OnDamageCallback;
    }

    private void OnDamageCallback(DamageInfo obj)
    {
        TakeDamage(obj.Object, obj.Damage);
    }
}
