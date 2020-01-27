using DRAP.Utils;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    EInterpType interpType;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var generator = target as Path;

        interpType = (EInterpType)EditorGUILayout.EnumPopup(interpType);

        if (GUILayout.Button("Smooth"))
        {
            Undo.RecordObject(target, "Path Smoothed");
            generator.Smooth(interpType);
            EditorUtility.SetDirty(target);
        }
    }
}
