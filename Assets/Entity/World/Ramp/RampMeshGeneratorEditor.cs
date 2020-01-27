using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RampMeshGenerator))]
public class RampMeshGeneratorEditor : Editor
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var generator = target as RampMeshGenerator;

        if (GUILayout.Button("Generate"))
        {
            generator.CreateRamp();
        }

        if (GUILayout.Button("Smooth Ramp"))
        {
            Undo.RecordObject(target, "Ramp Smoothed");
            generator.SmoothRamp();
            EditorUtility.SetDirty(target);
        }
    }
}
