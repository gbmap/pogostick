using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HE_PlatformBaked : MonoBehaviour
{
    public float BumperStrength = 50f;

    public Material platformMaterial;
    public Material bumperMaterial;

    public void CookedCallback(HoudiniEngineUnity.HEU_HoudiniAsset asset, bool success, List<GameObject> outputList)
    {
        Debug.LogFormat("Cooked! Asset={0}, Success={1}, Outputs={2}", asset.AssetName, success, outputList.Count);

        SetMaterials(outputList);
        AddColliders(outputList);
        AddBumperComponent(outputList);
    }

    private void SetMaterials(List<GameObject> outputList)
    {
        var platforms = outputList.FirstOrDefault(o => o.transform.GetChild(0).name.Contains("Platform"));
        foreach (var platformRenderer in platforms.GetComponentsInChildren<MeshRenderer>())
        {
            platformRenderer.sharedMaterial = platformMaterial;
        }
        var bumpers = outputList.FirstOrDefault(o => o.transform.GetChild(0).name.Contains("Bumper"));
        foreach (var bumperRenderer in bumpers.GetComponentsInChildren<MeshRenderer>())
        {
            bumperRenderer.sharedMaterial = bumperMaterial;
        }
    }

    private static void AddColliders(List<GameObject> outputList)
    {
        foreach (var obj in outputList)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                var platform = obj.transform.GetChild(i);
                var collider = platform.GetComponent<MeshCollider>();
                if (collider == null)
                {
                    collider = platform.gameObject.AddComponent<MeshCollider>();
                }

                collider.convex = true;
            }
        }
    }

    private void AddBumperComponent(List<GameObject> outputList)
    {
        GameObject bumpers = GetBumpers(outputList);
        for (int i = 0; i < bumpers.transform.childCount; i++)
        {
            Transform bumper = bumpers.transform.GetChild(i);

            var b = bumper.GetComponent<SetVelocityOnTriggerEnter>();
            if (b == null)
            {
                b = bumper.gameObject.AddComponent<SetVelocityOnTriggerEnter>();
                b.targetVelocity = Vector3.up;
                b.Strength = BumperStrength;
            }
        }
    }

    GameObject GetBumpers(List<GameObject> outputList)
    {
        foreach (GameObject obj in outputList)
        {
            if (obj.transform.childCount == 0)
                continue;

            else if (obj.transform.GetChild(0).name.Contains("Bumper"))
                return obj;
        }
        return null;
    }
}
