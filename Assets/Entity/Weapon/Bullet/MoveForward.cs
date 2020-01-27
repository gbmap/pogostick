using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    Rigidbody rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction, float speed)
    {
        rbody.velocity = direction * speed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + rbody.velocity);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.DrawLine(transform.position, transform.position + rbody.velocity);
    }
}
