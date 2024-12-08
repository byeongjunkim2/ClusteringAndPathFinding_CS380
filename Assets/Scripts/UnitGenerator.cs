using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{

    public GameObject unitPrefab;

    public int unitCount = 250;
    public float spawnRange = 30;
    public float speed = 0.5f;

    public enum Types {Boid,}
    public Types type = Types.Boid;

    //----Boids---------------
    [Range(0, 10)]
    public float cohesionWeight = 1;
    [Range(0, 10)]
    public float alignmentWeight = 1;
    [Range(0, 10)]
    public float separationWeight = 1;


    [Range(0, 100)]
    public float boundsWeight = 1;
    [Range(0, 100)]
    public float obstacleWeight = 10;
    [Range(0, 10)]
    public float egoWeight = 1;

    //------------------------

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < unitCount; i++)
        {

            Vector3 randVec = Random.insideUnitSphere;
            randVec *= spawnRange;
            Quaternion randRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);

            GameObject currUnit = Instantiate(unitPrefab, randVec, randRotation);
            currUnit.transform.SetParent(this.transform);

            switch (type)
            {
                case Types.Boid:
                    currUnit.GetComponent<BoidMover>().InitializeUnit(this, speed);
                    break;
            }

        }





    }

    // Update is called once per frame
    void Update()
    {

    }



}
