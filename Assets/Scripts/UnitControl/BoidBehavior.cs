using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    public float speed = 2f; // 기본 이동 속도
    public float rotationSpeed = 5f; // 방향 회전 속도
    public float neighborRadius = 3f;
    public float separationRadius = 1f;
    public float minimumDirChange = 0.01f;

    [Header("Weights")]
    public float separationWeight = 1.5f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;

    private List<Transform> neighbors;

    public Vector3 targetPosition; // 목표 위치

    void Update()
    {
        neighbors = GetNeighbors();
        Vector3 separation = CalculateSeparation();
        Vector3 alignment = CalculateAlignment();
        Vector3 cohesion = CalculateCohesion();

        // 가중치 기반 방향 계산
        Vector3 desiredDirection = (separation * separationWeight +
                                    alignment * alignmentWeight +
                                    cohesion * cohesionWeight +
                                    (targetPosition - transform.position).normalized).normalized;

        // 방향 전환을 부드럽게 처리
        Vector3 smoothedDirection = SmoothDirectionChange(transform.forward, desiredDirection);

        // 이동
        transform.forward = smoothedDirection; // 부드럽게 방향 전환
        transform.position = Vector3.Lerp(transform.position, transform.position + smoothedDirection * speed, Time.deltaTime); // 부드럽게 위치 이동
    }

    private List<Transform> GetNeighbors()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, neighborRadius);
        List<Transform> neighbors = new List<Transform>();
        foreach (Collider collider in colliders)
        {
            if (collider.transform != this.transform) // 본인 제외
                neighbors.Add(collider.transform);
        }
        return neighbors;
    }

    private Vector3 CalculateSeparation()
    {
        Vector3 separation = Vector3.zero;
        foreach (Transform neighbor in neighbors)
        {
            if (Vector3.Distance(transform.position, neighbor.position) < separationRadius)
            {
                separation += transform.position - neighbor.position;
            }
        }
        return separation.normalized;
    }

    private Vector3 CalculateAlignment()
    {
        if (neighbors.Count == 0) return transform.forward;

        Vector3 averageDirection = Vector3.zero;
        foreach (Transform neighbor in neighbors)
        {
            averageDirection += neighbor.forward;
        }
        return (averageDirection / neighbors.Count).normalized;
    }

    private Vector3 CalculateCohesion()
    {
        if (neighbors.Count == 0) return Vector3.zero;

        Vector3 centerOfMass = Vector3.zero;
        foreach (Transform neighbor in neighbors)
        {
            centerOfMass += neighbor.position;
        }
        centerOfMass /= neighbors.Count;

        return (centerOfMass - transform.position).normalized;
    }

    private Vector3 SmoothDirectionChange(Vector3 currentDirection, Vector3 desiredDirection)
    {
        if (Vector3.Distance(currentDirection, desiredDirection) < minimumDirChange) // 임계값 설정
            return currentDirection; // 변화 없음

        return Vector3.Slerp(currentDirection, desiredDirection, Time.deltaTime * rotationSpeed);
    }
}
