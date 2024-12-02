using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingUnitsController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField] private LayerMask floorLayerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // do raycast to certain position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, floorLayerMask))
            {
                Vector3 destination = hitInfo.point;

                // call move function
                foreach (Transform unit in transform)
                {
                    UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
                    if (unitMovement != null)
                    {
                        unitMovement.Move(destination);
                    }
                }
            }
        }
    }
}