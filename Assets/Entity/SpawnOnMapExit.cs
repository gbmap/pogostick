using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnMapExit : MonoBehaviour
{
    CharacterMovement movement;
    Gamemode gamemode;

    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        gamemode = FindObjectOfType<Gamemode>();
        if (gamemode == null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemode.IsInsideLevel(transform.position))
        {
            movement.ReturnToLastGroundPosition();
        }
    }
}
