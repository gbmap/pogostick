using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public string bulletLayer;

    public void Fire(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.down*0.5f + transform.position + direction * 2.0f, Quaternion.LookRotation(direction, Vector3.up));
        bullet.layer = LayerMask.NameToLayer(bulletLayer);
        bullet.GetComponent<MoveForward>().SetDirection(direction, 200f);
    }
    
}
