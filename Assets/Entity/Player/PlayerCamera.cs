using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public Vector2 sensitivity = new Vector2(15f, 15f);
    
    Vector3 targetRotation;

    [Header("Shake")]
    Vector3 shakeRotation;
    public Trauma trauma;
    float shake { get { return trauma.Shake; } }
    public float shakeMaxAngle = 25f;
    public float noiseTimeFactor = 10f;

    [Header("Recoil")]
    CharacterShooting shooting;
    Vector3 recoilRotation;
    public float recoilMaxAngle = 25f;

    private void Awake()
    {
        shooting = GetComponentInParent<CharacterShooting>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        targetRotation += new Vector3(-Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime, Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime);
        //targetRotation.y = Mathf.Clamp(targetRotation.y, -360f, 360f);
        targetRotation.x = Mathf.Clamp(targetRotation.x, -90f, 90f);
        
        shakeRotation = new Vector3(shakeMaxAngle * shake * ShakeNoise(0f),
                                    shakeMaxAngle * shake * ShakeNoise(0.5f),
                                    shakeMaxAngle * shake * ShakeNoise(0.75f));

        recoilRotation = new Vector3(recoilMaxAngle * -shooting.trauma.Shake, 0f, 0f);

        transform.localEulerAngles = targetRotation + shakeRotation + recoilRotation;
        trauma.Update();
    }

    float ShakeNoise(float y)
    {
        return (Mathf.PerlinNoise(Time.time * noiseTimeFactor, y) - 0.5f) * 2f;
    }

}