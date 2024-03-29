﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 wishDir;
    public bool IsCrouched;

    public MovementConfiguration MovementConfiguration;

    public float ground_accelerate => MovementConfiguration.GroundAcceleration;
    public float max_velocity_ground => MovementConfiguration.MaxVelocityGround;
    public float air_accelerate => MovementConfiguration.AirAcceleration;
    public float max_velocity_air => MovementConfiguration.MaxVelocityAir;
    public float friction => MovementConfiguration.Friction;

    public LayerMask groundCheckMask;

    Rigidbody rbody;
    new Collider collider;

    Vector3 lastVel;
    Vector3 biggestVel;
    float velFactor = 1f;

    public bool IsOnGround
    {
        get {
            Ray r = new Ray();
            r.origin = transform.position + (Vector3.down * collider.bounds.extents.y * 0.95f);
            r.direction = Vector3.down * 0.6f;

            RaycastHit hitInfo;
            Physics.Raycast(r, out hitInfo, 0.2f, groundCheckMask.value);
            return Vector3.Dot(Vector3.up, hitInfo.normal) > 0.75f;
        }
    }
    bool lastIsOnGround;

    Vector3 lastGroundPos;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<Collider>();
        lastGroundPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOnGround && lastIsOnGround)
        {
            biggestVel = Vector3.zero;
            lastGroundPos = transform.position;
        }

        else if (!IsOnGround)
        {
            if (biggestVel.y > rbody.velocity.y)
            {
                biggestVel = rbody.velocity;
            }
        }

        velFactor = Mathf.Lerp(velFactor, IsCrouched ? 0.85f : 1f, Time.deltaTime);

        Vector3 dir = wishDir;

        //rbody.MovePosition(rbody.position + (MoveGround(dir, rbody.velocity) * Time.deltaTime));
        float yy = rbody.velocity.y;
        Vector3 vel = rbody.velocity;
        vel = (IsOnGround && lastIsOnGround) ? (MoveGround(dir, vel)) : MoveAir(dir, vel);
        vel *= velFactor;
        vel.y = yy;
        rbody.velocity = vel;
        lastVel = rbody.velocity;
        lastIsOnGround = IsOnGround;
    }

    private Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float max_velocity)
    {
        float projVel = Vector3.Dot(prevVelocity, accelDir); // Vector projection of Current velocity onto accelDir.
        float accelVel = accelerate * Time.fixedDeltaTime; // Accelerated velocity in direction of movment

        // If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
        if (projVel + accelVel > max_velocity)
            accelVel = max_velocity - projVel;

        return prevVelocity + accelDir * accelVel;
    }

    private Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity)
    {
        // Apply Friction
        float speed = prevVelocity.magnitude;
        if (speed != 0) // To avoid divide by zero errors
        {
            float drop = speed * friction * Time.fixedDeltaTime;
            prevVelocity *= Mathf.Max(speed - drop, 0) / speed; // Scale the velocity based on friction.
        }

        // ground_accelerate and max_velocity_ground are server-defined movement variables
        return Accelerate(accelDir, prevVelocity, ground_accelerate, max_velocity_ground);
    }

    private Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity)
    {
        // air_accelerate and max_velocity_air are server-defined movement variables
        return Accelerate(accelDir, prevVelocity, air_accelerate, max_velocity_air);
    }

    public void Jump()
    {
        if (!IsOnGround) return;
        Vector3 vel2 = rbody.velocity;

        float velFactor = IsCrouched ? 0.5f : 0.75f;
        float defaultVelFactor = IsCrouched ? 5f : 20f;

        vel2.y = Mathf.Max(Mathf.Abs(biggestVel.y * 0.85f) * velFactor, defaultVelFactor);

        rbody.velocity = vel2;
    }
    
    public void ReturnToLastGroundPosition()
    {
        biggestVel = Vector3.zero;
        transform.position = lastGroundPos + Vector3.up;
        rbody.velocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Ray r = new Ray();
        r.origin = transform.position + (Vector3.down * (collider.bounds.extents.y * 0.95f));
        r.direction = Vector3.down;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(r.origin, r.origin + r.direction);
    }

    private void OnGUI()
    {
        if (!gameObject.CompareTag("Player")) return;
        GUILayout.Label("On Ground: " + IsOnGround);
    }
}
