using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pathPrefabs;
    [SerializeField] private GameObject firstPath;
    [SerializeField] private int pathCount;
    private float zPathSize;
    [SerializeField] private List<GameObject> pathList = new List<GameObject>();
    private const float positionBais = 0f;

    private void Start()
    {
        zPathSize = firstPath.transform.GetChild (0).GetComponent<Renderer>().bounds.size.z;
        pathList.Add(firstPath);
        SpawnPath();
    }

    private void SpawnPath()
    {
        for (int i = 0; i < pathCount; i++)
        {
            Vector3 pathPosition = pathList[pathList.Count - 1].transform.position + Vector3.forward * zPathSize;
            pathPosition.z += positionBais;
            GameObject path = Instantiate(pathPrefabs[Random.Range(0, pathPrefabs.Length)], pathPosition, Quaternion.identity);
            path.transform.parent = transform;
            pathList.Add(path);
        }
    }
}
