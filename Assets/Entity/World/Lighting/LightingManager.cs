using UnityEngine;


public class LightingManager : MonoBehaviour
{
    public LightingConfig Config;

    // Start is called before the first frame update
    void Awake()
    {
        RenderSettings.skybox = Config.Skybox;
        RenderSettings.ambientLight = Config.AmbientLight;
        //RenderSettings.sun = GetComponent<Light>();
    }
}
