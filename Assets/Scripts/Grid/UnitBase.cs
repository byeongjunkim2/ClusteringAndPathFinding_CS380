using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Ground,
    Air
}

public class UnitBase : MonoBehaviour
{
    public UnitType unitType = UnitType.Ground;

    public CapsuleCollider itsCollider;

    // Start is called before the first frame update
    void Start()
    {
        // if collider is not assigned...
        if (itsCollider == null)
        {
            itsCollider = GetComponent<CapsuleCollider>();
            if (itsCollider == null)
            {
                foreach (Transform child in transform)
                {
                    itsCollider = child.GetComponent<CapsuleCollider>();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
