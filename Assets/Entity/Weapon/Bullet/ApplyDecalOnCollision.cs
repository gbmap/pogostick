using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDecalOnCollision : MonoBehaviour
{
    public GameObject Decal;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("World")) return;

        var contact = collision.GetContact(0);
        Instantiate(Decal, contact.point + contact.normal*0.02f, Quaternion.Euler(contact.normal.x + 90f, contact.normal.y, contact.normal.z + Random.Range(0, 360f)));
    }
}
