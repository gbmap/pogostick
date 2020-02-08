using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOverTime : MonoBehaviour
{
    public GameObject Object;
    public float SpawnRate = 2f;
    public float SpawnVelocity = 5f;

    public bool OnlyOnRadius = true;
    public float SightRadius = 30f;

    private float lastSpawn;

    // Update is called once per frame
    void Update()
    {
        if (OnlyOnRadius)
        {
            if (Physics.OverlapSphere(transform.position, SightRadius, 1 << LayerMask.NameToLayer("Players")).Length > 0)
            {
                SpawnCheck();
            }
        }
        else
        {
            SpawnCheck();
        }
    }

    void SpawnCheck()
    {
        if (Time.time > lastSpawn + SpawnRate)
        {
            var obj = Instantiate(Object, transform.position + Vector3.up, Quaternion.identity);
            obj.transform.localScale = Vector3.one;
            var rbody = obj.GetComponent<Rigidbody>();
            rbody.velocity = (Random.insideUnitSphere + Vector3.up * 2f) * SpawnVelocity;
            lastSpawn = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SightRadius);
    }
}
