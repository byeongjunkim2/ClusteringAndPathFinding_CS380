using System.Collections;
using System.Collections.Generic;using TMPro;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;

    // -- private
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float stoppingDistanceSquared;

    void Update()
    {
        if (isMoving)
        {
            // move
            var unitType = GetComponent<UnitBase>().unitType;
            switch (unitType)
            {
                case UnitType.Ground:
                    OnGroundMove();
                    break;

                case UnitType.Air:
                    OnAirMove();
                    break;
            }

            // calc squared distance
            float distanceSquared = (transform.position - targetPosition).sqrMagnitude;

            if (distanceSquared < stoppingDistanceSquared)
            {
                isMoving = false;
            }
        }
    }

    // call move function from outside (pbulic)
    public void Move(Vector3 destination)
    {
        targetPosition = destination;
        isMoving = true;

        // based on radius, calculate stopDistance
        float unitRadius = GetComponent<UnitBase>().itsCollider.radius * transform.localScale.x;
        stoppingDistanceSquared = unitRadius * unitRadius;
    }



    private void OnGroundMove()
    {
        // TODO : A star algorithm
    }

    private void OnAirMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}