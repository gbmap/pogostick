using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterMovementPlayerInput : MonoBehaviour
{
    public bool IsOnPogo = true;

    CharacterMovement movement;

    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = IsOnPogo ? 0f : Input.GetAxis("Vertical");

        Vector3 fwd = Camera.main.transform.forward;
        fwd.y = 0f;

        Vector3 rgt = Camera.main.transform.right;
        rgt.y = 0f;

        Vector3 dir = (fwd * y) + (rgt * x);

        movement.wishDir = dir;

        if (IsOnPogo)
        {
            movement.Jump();
        }

        movement.IsCrouched = Input.GetKey(KeyCode.Space);

    }
}
