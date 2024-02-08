using UnityEngine;

[CreateAssetMenu(menuName="Pogostick/Movement Configuration")]
public class MovementConfiguration : ScriptableObject
{
    public float GroundAcceleration = 10f;
    public float MaxVelocityGround = 200f;
    public float AirAcceleration = 15f;
    public float MaxVelocityAir = 300f;
    public float Friction = 10f;
}
