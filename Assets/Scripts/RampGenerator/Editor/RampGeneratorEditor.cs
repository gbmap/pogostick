using UnityEditor;
using UnityEngine;
using DRAP.Level.Generator;

[CustomEditor(typeof(RampGenerator))]
public class RampGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
            (target as RampGenerator).Generate();
    }

    public void OnSceneGUI()
    {
        RampGenerator generator = target as RampGenerator;
        Transform transformA = generator.transform.GetChild(0);
        Transform transformB = transformA.GetChild(0);
        Transform transformD = generator.transform.GetChild(1);
        Transform transformC = transformD.GetChild(0);

        transformA.position = Handles.FreeMoveHandle(
            transformA.position, 
            Quaternion.identity, 
            1f, 
            Vector3.one*0.1f, 
            Handles.RectangleHandleCap
        );

        transformB.position = Handles.FreeMoveHandle(
            transformB.position, 
            Quaternion.identity, 
            1f, 
            Vector3.one*0.1f, 
            Handles.CircleHandleCap
        );

        transformC.position = Handles.FreeMoveHandle(
            transformC.position, 
            Quaternion.identity, 
            1f, 
            Vector3.one*0.1f, 
            Handles.CircleHandleCap
        );

        transformD.position = Handles.FreeMoveHandle(
            transformD.position, 
            Quaternion.identity, 
            1f, 
            Vector3.one*0.1f, 
            Handles.RectangleHandleCap
        );

        Vector3 a = transformA.position;
        Vector3 b = transformB.position;
        Vector3 c = transformC.position;
        Vector3 d = transformD.position;

        for (int i = 0; i < generator.divisions; i++)
        {
            float t = ((float)i)/generator.divisions;
            Vector3 p1 = DRAP.Utils.Interp.Cubic(a, b, c, d, t);
            Vector3 p2 = DRAP.Utils.Interp.Cubic(a, b, c, d, t+1f/generator.divisions);

            Handles.DrawLine(p1, p2, 0f);
        }

    }
}
