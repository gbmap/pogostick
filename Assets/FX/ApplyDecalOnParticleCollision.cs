using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDecalOnParticleCollision : MonoBehaviour
{
    public GameObject decal;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        int i = 0;

        while (i < numCollisionEvents)
        {
            ParticleCollisionEvent evnt = collisionEvents[i];
            Instantiate(decal, evnt.intersection, Quaternion.Euler(evnt.normal.x + 90f, evnt.normal.y, evnt.normal.z + Random.Range(0f, 360f)));
            i++;
        }
    }
}
