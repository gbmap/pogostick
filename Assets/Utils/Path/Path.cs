using DRAP.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Range(1, 16)]
    public int SmoothSegments;

    public Transform[] Points
    {
        get { return GetComponentsInChildren<Transform>(); }
    }

    public void Smooth(EInterpType interpType)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == transform.childCount - 1) break;

            Transform a = transform.GetChild(i);
            Transform b = transform.GetChild(i + 1);

            for (int j = 1; j < SmoothSegments + 1; j++)
            {
                float t = ((float)j) / (SmoothSegments + 1);

                /* Linear */
                Vector3 p = Vector3.Lerp(a.localPosition, b.localPosition, t);

                switch (interpType)
                {
                    default: p = Interp.Linear(a.localPosition, b.localPosition, t); break;
                    case EInterpType.Quadratic: p = Interp.Quadratic(a.localPosition, a.localPosition + a.forward, b.localPosition, t); break;
                }
                
                Vector3 r = Quaternion.Lerp(a.localRotation, b.localRotation, t).eulerAngles;
                Vector3 s = Vector3.Lerp(a.localScale, b.localScale, t);

                GameObject go = new GameObject("Point" + (i + 1));
                go.transform.parent = transform;
                go.transform.SetSiblingIndex(i + 1);
                go.transform.localPosition = p;
                go.transform.localRotation = Quaternion.Euler(r);
                go.transform.localScale = s;
                i++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(child.position, 1f);
            if (i < transform.childCount - 1)
            {
                Transform childB = transform.GetChild(i + 1);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(child.position, childB.position);
            }
        }
    }
}
