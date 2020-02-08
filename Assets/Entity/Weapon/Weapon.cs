using System.Collections.Generic;
using UnityEngine;

public enum EWeaponFiringType
{
    Semi,
    Burst,
    Automatic
}

[CreateAssetMenu()]
[System.Serializable]
public class Weapon : ScriptableObject
{
    public GameObject bullet;
    public EWeaponFiringType firingType = EWeaponFiringType.Semi;
    public float fireRate = 0.1f;
    public int bulletsPerFire = 1;
    public float spread = 5f;
    //public float delayPerBullet = 0.0f;
    public float bulletSpeed = 200f;
    public float traumaPerBullet = 0.25f;
    public float traumaPerShot = 0f;

    public bool CanShoot
    {
        get {  return Time.time > lastShot + fireRate; }
    }

    [System.NonSerialized]
    private float lastShot = -1f;

    private GameObject[] nullBullets = { };

    public bool Shoot(Vector3 origin, Vector3 direction, int layer, ref Trauma trauma)
    {
        return Shoot(origin, direction, layer, ref trauma, ref nullBullets);
    }

    public bool Shoot(Vector3 origin, Vector3 direction, int layer, ref Trauma trauma, ref GameObject[] bullets)
    {
        if (!CanShoot)
        {
            return false;
        }

        List<GameObject> bulletInstances = new List<GameObject>(bulletsPerFire);

        for (int i = 0; i < bulletsPerFire; i++)
        {
            Vector3 directionRandom = Mathf.Clamp01(i) * spread * new Vector3(Noise(i, Time.time),
                                                           Noise(i * 5f, Time.time * 25f),
                                                           Noise(i * 10f, Time.time * 5f));

            bulletInstances.Add(Shoot(ref trauma, bullet, layer, origin, direction + directionRandom));
        }

        trauma.Value += traumaPerShot;
        lastShot = Time.time;
        bullets = bulletInstances.ToArray();
        return true;
    }

    private GameObject Shoot(ref Trauma trauma, GameObject bullet, int layer, Vector3 origin, Vector3 direction)
    {

        GameObject instance = GameObject.Instantiate(bullet, origin, Quaternion.LookRotation(direction, Vector3.up));
        instance.layer = layer;
        instance.GetComponent<MoveForward>().SetDirection(direction, bulletSpeed);

        trauma.Value += traumaPerBullet;

        return instance;
    }

    private float Noise(float x, float y)
    {
        return (Random.value - 0.5f) * 2f;
    }
}