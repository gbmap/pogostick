using UnityEngine;

public class FireMsg
{
    public Vector3 dir;
}

public class CharacterShooting : MonoBehaviour
{
    public string bulletLayer;
    private int iBulletLayer;

    [Header("Weapon Configuration")]
    public Weapon weapon;

    public System.Action<FireMsg> OnFire;

    [Header("Recoil")]
    public Trauma trauma;
    public float traumaIncreasePerShot = 0.25f;

    private void Awake()
    {
        iBulletLayer = LayerMask.NameToLayer(bulletLayer);
    }

    private void Update()
    {
        trauma.Update();
    }

    public void Fire(Vector3 origin, Vector3 direction)
    {
        if (weapon == null)
        {
            return;
        }

        weapon.Shoot(origin, direction, iBulletLayer, ref trauma);
        /*GameObject bullet = Instantiate(bulletPrefab, origin, Quaternion.LookRotation(direction, Vector3.up));
        bullet.layer = LayerMask.NameToLayer(bulletLayer);
        bullet.GetComponent<MoveForward>().SetDirection(direction, 200f);

        OnFire?.Invoke(new FireMsg
        {
            dir = direction
        });

        trauma.Value += traumaIncreasePerShot;*/



    }
    
}
