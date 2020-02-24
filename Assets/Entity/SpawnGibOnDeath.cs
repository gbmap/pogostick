using UnityEngine;

public class SpawnGibOnDeath : DamageHandler
{
    public GameObject Gib;
    public int Count;

    protected override void OnDeath(DamageInfo msg)
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate(Gib, transform.position, Quaternion.identity);
        }
    }
}
