using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    CharacterMovement movement;

    CharacterMovementAIFollowPlayer AI;

    public bool IsMoving
    {
        set
        {
            animator.SetBool(isMovingHash, value);
        }
    }

    public bool IsAttacking
    {
        set
        {
            animator.SetBool(isAttackingHash, value);
        }
    }

    int isMovingHash = Animator.StringToHash("Moving");
    int isAttackingHash = Animator.StringToHash("Attacking");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<CharacterMovement>();
        AI = GetComponentInParent<CharacterMovementAIFollowPlayer>();
    }

    private void Update()
    {
        IsAttacking = AI.PlayerTarget != null ? (Vector3.Distance(AI.PlayerTarget.transform.position, transform.position) < 3.5f) : false;
        IsMoving = movement.wishDir.sqrMagnitude > 0f;
    }
}
