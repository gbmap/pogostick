using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnTouch : MonoBehaviour
{
    public GameObject Target;
    public bool Value;

    private void OnCollisionExit(Collision collision)
    {
        Target.SetActive(Value);
    }
}
