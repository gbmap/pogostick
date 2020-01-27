using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterShooting))]
public class CharacterShootingInput : MonoBehaviour
{
    CharacterShooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        shooting = GetComponent<CharacterShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shooting.Fire(Camera.main.transform.forward);
        }
    }
}
