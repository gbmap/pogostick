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
        if (shooting.weapon == null)
        {
            return;
        }

        if ( (Input.GetMouseButtonDown(0) || (shooting.weapon.firingType == EWeaponFiringType.Automatic && Input.GetMouseButton(0)) ) &&
            shooting.weapon.CanShoot)
        {
            Vector3 shotOrigin = Camera.main.transform.position + Vector3.down * 2f;
            Vector3 shotDirection = Camera.main.transform.forward;

            // BLERGH
            Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 1000f))
            {
                shotDirection = (hit.point - shotOrigin).normalized;
            }

            shooting.Fire(shotOrigin, shotDirection);
        }
    }
}
