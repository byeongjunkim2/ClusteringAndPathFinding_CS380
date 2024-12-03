using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    public float speed = 2f; // �⺻ �̵� �ӵ�
    public float rotationSpeed = 5f; // ���� ȸ�� �ӵ�
    public float neighborRadius = 3f;
    public float separationRadius = 1f;
    public float minimumDirChange = 0.01f;

    [Header("Weights")]
    public float separationWeight = 1.5f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;

    private List<Transform> neighbors;

    public Vector3 targetPosition; // ��ǥ ��ġ

    void Update()
    {
        neighbors = GetNeighbors();
        Vector3 separation = CalculateSeparation();
        Vector3 alignment = CalculateAlignment();
        Vector3 cohesion = CalculateCohesion();

        // ����ġ ��� ���� ���
        Vector3 desiredDirection = (separation * separationWeight +
                                    alignment * alignmentWeight +
                                    cohesion * cohesionWeight +
                                    (targetPosition - transform.position).normalized).normalized;

        // ���� ��ȯ�� �ε巴�� ó��
        Vector3 smoothedDirection = SmoothDirectionChange(transform.forward, desiredDirection);

        // �̵�
        transform.forward = smoothedDirection; // �ε巴�� ���� ��ȯ
        transform.position = Vector3.Lerp(transform.position, transform.position + smoothedDirection * speed, Time.deltaTime); // �ε巴�� ��ġ �̵�
    }

    private List<Transform> GetNeighbors()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, neighborRadius);
        List<Transform> neighbors = new List<Transform>();
        foreach (Collider collider in colliders)
        {
            if (collider.transform != this.transform) // ���� ����
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
        if (Vector3.Distance(currentDirection, desiredDirection) < minimumDirChange) // �Ӱ谪 ����
            return currentDirection; // ��ȭ ����

        return Vector3.Slerp(currentDirection, desiredDirection, Time.deltaTime * rotationSpeed);
    }
}
