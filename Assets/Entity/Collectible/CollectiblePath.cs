using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Path))]
public class CollectiblePath : MonoBehaviour
{
    public GameObject prefab;

    Path path;

    private void Awake()
    {
        path = GetComponent<Path>();
    }

    public void SpawnCollectibles()
    {
        if (prefab == null) return;

        if (path == null)
        {
            path = GetComponent<Path>();
        }

        foreach (Transform point in path.Points)
        {
            if (point.childCount > 0) continue;

            var obj = Instantiate(prefab, point.transform.position, Quaternion.identity);
            obj.transform.SetParent(point, true);
        }
    }
}
