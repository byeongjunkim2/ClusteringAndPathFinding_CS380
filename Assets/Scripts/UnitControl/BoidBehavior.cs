using UnityEngine;
using System.Collections.Generic;

public class BoidBehavior : MonoBehaviour
{
    public enum SeparationMode { Linear, InverseSquare }
    public enum CohesionMode { Center, Weighted }

    public SeparationMode separationMode = SeparationMode.Linear;
    public CohesionMode cohesionMode = CohesionMode.Center;

    [Header("Radius & Weights data")]
    public BoidBehaviorData data;
    //public float speed = 2f;
    //public float neighborRadius = 3f;
    //public float separationRadius = 1f;

    //[Header("Weights")]
    //public float separationWeight = 1.5f;
    //public float alignmentWeight = 1f;
    //public float cohesionWeight = 1f;

    private List<Transform> neighbors;
    public Vector3 targetPosition; // 타겟 포지션 추가

    void Update()
    {
        neighbors = GetNeighbors();

        Vector3 separation = CalculateSeparation();
        Vector3 alignment = CalculateAlignment();
        Vector3 cohesion = CalculateCohesion();
        Vector3 targetDirection = (targetPosition - transform.position).normalized; // 타겟 방향

        Vector3 desiredDirection = (separation * data.separationWeight +
                                    alignment * data.alignmentWeight +
                                    cohesion * data.cohesionWeight +
                                    targetDirection).normalized;

        transform.forward = Vector3.Slerp(transform.forward, desiredDirection, Time.deltaTime * 5f);
        transform.position += transform.forward * data.speed * Time.deltaTime;
    }

    private Vector3 CalculateSeparation()
    {
        switch (separationMode)
        {
            case SeparationMode.Linear:
                return CalculateSeparationLinear();
            case SeparationMode.InverseSquare:
                return CalculateSeparationInverseSquare();
            default:
                return Vector3.zero;
        }
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
        switch (cohesionMode)
        {
            case CohesionMode.Center:
                return CalculateCohesionCenter();
            case CohesionMode.Weighted:
                return CalculateCohesionWeighted();
            default:
                return Vector3.zero;
        }
    }

    private Vector3 CalculateSeparationLinear()
    {
        Vector3 separation = Vector3.zero;
        foreach (Transform neighbor in neighbors)
        {
            float distance = Vector3.Distance(transform.position, neighbor.position);
            if (distance < data.separationRadius)
            {
                separation += (transform.position - neighbor.position) / distance;
            }
        }
        return separation;
        //return separation.normalized;
    }

    private Vector3 CalculateSeparationInverseSquare()
    {
        Vector3 separation = Vector3.zero;
        foreach (Transform neighbor in neighbors)
        {
            float distance = Vector3.Distance(transform.position, neighbor.position);
            if (distance < data.separationRadius)
            {
                separation += (transform.position - neighbor.position) / (distance * distance);
            }
        }
        return separation;
        //return separation.normalized;
    }

    private Vector3 CalculateCohesionCenter()
    {
        if (neighbors.Count == 0) return Vector3.zero;

        Vector3 centerOfMass = Vector3.zero;
        foreach (Transform neighbor in neighbors)
        {
            centerOfMass += neighbor.position;
        }
        centerOfMass /= neighbors.Count;

        return (centerOfMass - transform.position);//.normalized;
    }

    private Vector3 CalculateCohesionWeighted()
    {
        if (neighbors.Count == 0) return Vector3.zero;

        Vector3 weightedCenter = Vector3.zero;
        float totalWeight = 0f;

        foreach (Transform neighbor in neighbors)
        {
            float distance = Vector3.Distance(transform.position, neighbor.position);
            float weight = 1f / distance;
            weightedCenter += neighbor.position * weight;
            totalWeight += weight;
        }
        weightedCenter /= totalWeight;

        return (weightedCenter - transform.position);//.normalized;
    }

    public List<Transform> GetNeighbors()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, data.neighborRadius);
        List<Transform> neighbors = new List<Transform>();
        foreach (Collider collider in colliders)
        {
            if (collider.transform != this.transform) // 본인 제외
                neighbors.Add(collider.transform);
        }
        return neighbors;
    }

}
