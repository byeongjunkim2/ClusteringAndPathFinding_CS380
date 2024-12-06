using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int boidCount = 50;
    public Vector3 spawnArea = new Vector3(10, 10, 10);
    public Transform player;

    private List<BoidBehavior> boidBehaviors = new List<BoidBehavior>();

    private List<GameObject> boids = new List<GameObject>();

    void Start()
    {
        InitializeBoids();
    }

    public void InitializeBoids()
    {
        // ���� Boid �ı�
        foreach (var boid in boids)
        {
            Destroy(boid);
        }
        boids.Clear();
        boidBehaviors.Clear(); // boidBehaviors ����Ʈ �ʱ�ȭ

        // ���ο� Boid ����
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            GameObject boid = Instantiate(boidPrefab, spawnPosition, Quaternion.identity);
            BoidBehavior behavior = boid.GetComponent<BoidBehavior>();

            if (behavior != null)
            {
                boidBehaviors.Add(behavior); // boidBehaviors�� �߰�
            }

            boids.Add(boid);
        }
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
            behavior.separationMode = mode; // SeparationMode ������Ʈ
        }
    }

    public void UpdateCohesionMode(BoidBehavior.CohesionMode mode)
    {
        foreach (var behavior in boidBehaviors)
        {
            behavior.cohesionMode = mode; // CohesionMode ������Ʈ
        }
    }

    public void SetSpawnArea(Vector3 newSpawnArea)
    {
        spawnArea = newSpawnArea;
    }


    public void SetMode(BoidBehavior.SeparationMode separationMode, BoidBehavior.CohesionMode cohesionMode)
    {
        InitializeBoids();

        UpdateSeparationMode(separationMode);
        UpdateCohesionMode(cohesionMode);
    }


}
