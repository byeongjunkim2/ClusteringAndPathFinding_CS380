using UnityEngine;
using System.Collections.Generic;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab; // Boid ������
    public int boidCount = 50;    // Boid ����
    public Vector3 spawnArea = new Vector3(10, 10, 10); // Boid ���� ����
    public Transform player;     // �÷��̾� Transform (���� ����)

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
                behavior.targetPosition = targetPosition; // Ÿ�� ������ ������Ʈ
            }
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, spawnArea * 2);
    }
}
