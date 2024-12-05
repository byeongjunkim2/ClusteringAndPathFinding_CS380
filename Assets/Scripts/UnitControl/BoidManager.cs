using UnityEngine;
using System.Collections.Generic;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab; // Boid 프리팹
    public int boidCount = 50;    // Boid 개수
    public Vector3 spawnArea = new Vector3(10, 10, 10); // Boid 생성 범위
    public Transform player;     // 플레이어 Transform (선택 사항)

    private List<BoidBehavior> boidBehaviors = new List<BoidBehavior>();

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
            BoidBehavior behavior = boid.GetComponent<BoidBehavior>();
            if (behavior != null)
            {
                boidBehaviors.Add(behavior);
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position;
            foreach (var behavior in boidBehaviors)
            {
                behavior.targetPosition = targetPosition; // 타겟 포지션 업데이트
            }
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, spawnArea * 2);
    }
}
