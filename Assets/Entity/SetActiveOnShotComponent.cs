using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SetActiveOnShot<T> : DamageHandler
{
    public T Target;
    public bool TargetValue;

    protected override void OnTakeDamage(DamageInfo msg)
    {
        SetTargetActive(TargetValue);
    }

    public abstract void SetTargetActive(bool value);
}

public class SetActiveOnShotComponent : SetActiveOnShot<Behaviour>
{
    public override void SetTargetActive(bool value)
    {
        Target.enabled = value;
    }
}
