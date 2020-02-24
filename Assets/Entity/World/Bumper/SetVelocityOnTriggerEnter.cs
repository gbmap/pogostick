using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocityOnTriggerEnter : MonoBehaviour
{
    public Vector3 targetVelocity = Vector3.up;
    public Vector3 velocityMask = Vector3.up;
    public float Strength = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 vel = other.attachedRigidbody.velocity;
        vel.x = Mathf.Lerp(vel.x, targetVelocity.x*Strength, velocityMask.x);
        vel.y = Mathf.Lerp(vel.y, targetVelocity.y*Strength, velocityMask.y);
        vel.z = Mathf.Lerp(vel.z, targetVelocity.z*Strength, velocityMask.z);

        other.attachedRigidbody.velocity = vel;
    }
}
