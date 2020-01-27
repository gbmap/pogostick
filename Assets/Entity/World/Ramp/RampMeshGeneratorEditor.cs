using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RampMeshGenerator))]
public class RampMeshGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var generator = target as RampMeshGenerator;

        if (GUILayout.Button("Generate"))
        {
            generator.CreateRamp();
        }
    }
}
