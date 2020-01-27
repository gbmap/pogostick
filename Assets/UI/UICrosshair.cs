using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICrosshair : MonoBehaviour
{
    CharacterShooting shooting;

    public GameObject Lines;
    Image[] crosshairLines;
    Vector3[] crosshairLinesDefaultPos;

    public float recoilScale = 45f;

    // Start is called before the first frame update
    void Awake()
    {
        shooting = FindObjectOfType<CharacterShooting>();
        crosshairLines = Lines.GetComponentsInChildren<Image>();
        crosshairLinesDefaultPos = crosshairLines.Select(c => c.rectTransform.localPosition).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < crosshairLines.Length; i++)
        {
            Image line = crosshairLines[i];
            Vector3 pos = crosshairLinesDefaultPos[i];

            line.rectTransform.localPosition = pos + pos.normalized * shooting.trauma.Shake * recoilScale;
        }
    }
}
