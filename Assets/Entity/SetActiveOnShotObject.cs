using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnShotObject : SetActiveOnShot<GameObject>
{
    public override void SetTargetActive(bool value)
    {
        Target.SetActive(value);
    }
}
