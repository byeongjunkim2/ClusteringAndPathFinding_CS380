using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int boidCount = 50;
    public Vector3 spawnArea = new Vector3(10, 10, 10);
    public Transform player; // 플레이어의 Transform

    private List<GameObject> boids = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            GameObject boid = Instantiate(boidPrefab, spawnPosition, Quaternion.identity);
            boids.Add(boid);
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position; // 플레이어 위치
            SetTargetForAllBoids(targetPosition);
        }
    }

    private void SetTargetForAllBoids(Vector3 targetPosition)
    {
        foreach (var boid in boids)
        {
            boid.GetComponent<BoidBehavior>().targetPosition = targetPosition;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, spawnArea * 2);
    }
}
