using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
//using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class AICARSManager : MonoBehaviour
{
    [SerializeField] private GameObject activeCars;
    [SerializeField] private GameObject unActiveCars;

    [SerializeField] private GameObject[] carArray;
    [SerializeField] private int initialCarNumbers = 50;

    [Range(0f, 1f)] public float carFreq;
    [Range(0f, 1f)] public float reverseCarFreq;

    private Vector3 previousCarPos = Vector3.zero;

    [SerializeField] private PathManager pathManager;
    
    private void Start()
    {
        // initantiate cars
        for (int i = 0; i < initialCarNumbers; i++)
        {
            GameObject car = Instantiate(carArray[UnityEngine.Random.Range(0, carArray.Length)]);
            car.SetActive(false);
            car.transform.parent = unActiveCars.transform;
        }
    }

    // Find all the pos to create car
    List<Vector3>ListCarPosition(GameObject thePath)
    {
        List<Vector3> listPathPosition = new List<Vector3>();

        for (int i = 0; i < thePath.transform.childCount; i++)
        {
            Vector3 pos = thePath.transform.GetChild(i).transform.position + new Vector3(0, 1, 0);
            listPathPosition.Add(pos);
        }
        return listPathPosition;
    }

    /// <summary>
    /// Getting a random car
    /// </summary>
    /// <returns></returns>
    GameObject GetRandomCar()
    {
        GameObject car = unActiveCars.transform.GetChild(UnityEngine.Random.Range(0, unActiveCars.transform.childCount)).gameObject;
        car.transform.parent = null;
        car.SetActive(true);
        return car;
    }

    public void CheckAndDisableCarPath()
    {
        List<Vector3>listCarPos = ListCarPosition(pathManager.pathList[pathManager.listPathIndex]);

        //create a car
        if(UnityEngine.Random.value <= carFreq)
        {
            Vector3 carPos = listCarPos[UnityEngine.Random.Range(0, listCarPos.Count)];
            while(carPos.x == previousCarPos.x)
            {
                carPos = listCarPos[UnityEngine.Random.Range(0,listCarPos.Count)];
            }
            previousCarPos = carPos;
            //random cars
            GameObject car = GetRandomCar();
            car.transform.position = carPos;

            if(UnityEngine.Random.value <= reverseCarFreq)
            {
                car.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            car.transform.parent = activeCars.transform;
        }
    }

    public void FindCarAndReset()
    {
        for(int i = 0; i < activeCars.transform.childCount; i++)
        {
            if(activeCars.transform.GetChild(i).transform.position.z < pathManager.destroyDistance)
            {
                GameObject theCar = activeCars.transform.GetChild(i).gameObject;
                theCar.gameObject.SetActive(false);
                theCar.transform.parent = unActiveCars.transform;
            }
        }
    }
    
}
