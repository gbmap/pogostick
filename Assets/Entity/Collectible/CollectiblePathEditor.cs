using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CollectiblePath))]
public class CollectiblePathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var generator = target as CollectiblePath;
        if (GUILayout.Button("Spawn"))
        {
            generator.SpawnCollectibles();
        }
    }
}
