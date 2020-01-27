using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterMovementAIFollowPlayer : MonoBehaviour
{
    public float SightRadius;

    CharacterMovement movement;

    public GameObject PlayerTarget
    {
        get; private set;
    }

    float lastPlayerCheck;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTarget == null)
        {
            SearchTarget();
        }
        else
        {
            FollowTarget(PlayerTarget);
        }
    }

    private void FollowTarget(GameObject target)
    {
        Vector3 delta = (target.transform.position - transform.position).normalized;
        delta.y = 0f;
        movement.wishDir = delta;
    }

    private void SearchTarget()
    {
        if (Time.time > lastPlayerCheck + 1f)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, SightRadius, 1 << LayerMask.NameToLayer("Players"));
            Collider player = colliders.OrderBy(c => Vector3.Distance(transform.position, c.transform.position)).FirstOrDefault();
            if (player != null)
            {
                PlayerTarget = player.gameObject;
            }
            lastPlayerCheck = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SightRadius);
    }
}
