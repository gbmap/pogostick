using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.DepthNormals;
    }
}
