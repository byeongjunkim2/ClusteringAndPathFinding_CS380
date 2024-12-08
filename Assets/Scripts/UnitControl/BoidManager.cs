using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

[System.Serializable]
public class BoidBehaviorData
{
    public float speed; //= 5;
    public float neighborRadius;// = 5;
    public float separationRadius;// = 2;
    public float separationWeight;//= 2;
    public float alignmentWeight;//= 1;
    public float cohesionWeight;// = 1.5f;
}

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public Transform boidPool;
    public int boidCount = 50;
    public Vector3 spawnArea = new Vector3(10, 10, 10);
    public Transform player;

    public List<BoidBehavior> boidBehaviors = new List<BoidBehavior>();

    private List<GameObject> boids = new List<GameObject>();

    [SerializeField]
    private BoidUISliderController boidController = null;

    [Header("current behavior data")]
    public BoidBehaviorData behaviorData;


    [Header("current modes")]
    public BoidBehavior.SeparationMode separationMode;
    public BoidBehavior.CohesionMode cohesionMode;


    void Start()
    {
        InitializeBoids(true);
    }

    public void InitializeBoids(bool firstInitialize = false)
    {
        // 기존 Boid 파괴
        foreach (var boid in boids)
        {
            Destroy(boid);
        }
        boids.Clear();
        boidBehaviors.Clear(); // boidBehaviors 리스트 초기화

        // 새로운 Boid 생성
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            GameObject boid = Instantiate(boidPrefab, spawnPosition, Quaternion.identity, boidPool);
            BoidBehavior behavior = boid.GetComponent<BoidBehavior>();

            // re-set the data when it is not first initialize
            if (firstInitialize == true)
            {
                behaviorData = behavior.data;
            }
            else
            {
                behavior.data = behaviorData;
            }

            // null check
            if (behavior != null)
            {
                boidBehaviors.Add(behavior); // boidBehaviors에 추가
                behavior.separationMode = separationMode;
                behavior.cohesionMode = cohesionMode;
            }


            boids.Add(boid);
        }

        // update slider information
        boidController?.UpdateSlider();

    }

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position;
            foreach (var behavior in boidBehaviors)
            {
                behavior.targetPosition = targetPosition;
            }
        }
    }

    void UpdateAllDataValues()
    {
        foreach (var behavior in boidBehaviors)
        {
            behavior.data = behaviorData;
        }
    }

    public float CalculateAverageNeighborDistance()
    {
        float totalDistance = 0f;
        int pairCount = 0;

        foreach (var boid in boidBehaviors)
        {
            foreach (var neighbor in boid.GetNeighbors())
            {
                totalDistance += Vector3.Distance(boid.transform.position, neighbor.position);
                pairCount++;
            }
        }

        return pairCount > 0 ? totalDistance / pairCount : 0f;
    }

    public float CalculateNeighborDistanceStdDev()
    {
        List<float> distances = new List<float>();

        foreach (var boid in boidBehaviors)
        {
            foreach (var neighbor in boid.GetNeighbors())
            {
                distances.Add(Vector3.Distance(boid.transform.position, neighbor.position));
            }
        }

        if (distances.Count == 0) return 0f;

        float mean = distances.Average();
        float sumSquaredDifferences = distances.Sum(d => Mathf.Pow(d - mean, 2));

        return Mathf.Sqrt(sumSquaredDifferences / distances.Count);
    }

    public void UpdateSeparationMode(BoidBehavior.SeparationMode mode)
    {
        foreach (var behavior in boidBehaviors)
        {
            behavior.separationMode = mode; // SeparationMode 업데이트
        }
    }

    public void UpdateCohesionMode(BoidBehavior.CohesionMode mode)
    {
        foreach (var behavior in boidBehaviors)
        {
            behavior.cohesionMode = mode; // CohesionMode 업데이트
        }
    }

    public void SetSpawnArea(Vector3 newSpawnArea)
    {
        spawnArea = newSpawnArea;
    }


    public void SetMode(BoidBehavior.SeparationMode separationMode)
    {
        this.separationMode = separationMode;

        UpdateSeparationMode(separationMode);
    }

    public void SetMode(BoidBehavior.CohesionMode cohesionMode)
    {
        this.cohesionMode = cohesionMode;

        UpdateCohesionMode(cohesionMode);
    }



}
