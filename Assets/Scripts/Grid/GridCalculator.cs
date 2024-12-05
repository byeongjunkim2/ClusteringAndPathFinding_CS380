using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GridCalculator : MonoBehaviour
{
    private GameObject[,] objectArray = new GameObject[20, 20];

    void Start()
    {
        foreach (Transform child in transform)
        {
            Vector3 position = child.position;

            //int xIndex = Mathf.Clamp((int)((child.transform.position.x + 95) / 10), 0, 19);
            //int yIndex = Mathf.Clamp((int)((child.transform.position.z + 95) / 10), 0, 19);
            int xIndex = (int)(child.transform.position.x + 95) / 10;
            int yIndex = (int)(child.transform.position.z + 95) / 10;

            objectArray[xIndex, yIndex] = child.gameObject;
        }
    }

    void Update()
    {

    }
}