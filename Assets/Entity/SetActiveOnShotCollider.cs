using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnShotCollider : SetActiveOnShot<Collider>
{
    public override void SetTargetActive(bool value)
    {
        Target.enabled = TargetValue;
    }
}
