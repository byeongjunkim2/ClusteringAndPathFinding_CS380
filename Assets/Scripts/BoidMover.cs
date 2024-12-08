using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMover : MonoBehaviour
{

    public UnitGenerator boidController;

    private float speed;

    List<BoidMover> neighbor = new List<BoidMover>();
    [SerializeField] LayerMask boidUnitLayer;

    Vector3 targetVec;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void InitializeUnit(UnitGenerator core, float _speed)
    {
        boidController = core;
        speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        FindNeighbor();

        Vector3 cohesionVec = GetCohesion() * boidController.cohesionWeight;
        Vector3 alignmentVec = GetAlignment() * boidController.alignmentWeight;
        Vector3 seperationVec = GetSeparation() * boidController.separationWeight;
        // Steer and Move

        targetVec = cohesionVec + alignmentVec + seperationVec;
        targetVec = Vector3.Lerp(this.transform.forward, targetVec, Time.deltaTime);
        targetVec = targetVec.normalized;
        this.transform.rotation = Quaternion.LookRotation(targetVec);
    


        transform.position += transform.forward * speed;
    }


    private void FindNeighbor()
    {
        neighbor.Clear();

        Collider[] colls = Physics.OverlapSphere(transform.position, 20f, boidUnitLayer);
        for (int i = 0; i < colls.Length; i++)
        {
            neighbor.Add(colls[i].GetComponent<BoidMover>());
        }



    }

    public Vector3 GetCohesion()
    {
        Vector3 cohesionVec = Vector3.zero;
        if (neighbor.Count > 0)
        {
            for (int i = 0; i < neighbor.Count; i++)
            {
                cohesionVec += neighbor[i].transform.position;
            }

        }
        else
        {
            return cohesionVec;
        }

        cohesionVec /= neighbor.Count;
        cohesionVec -= transform.position;
        cohesionVec.Normalize();
        return cohesionVec;
    }


    public Vector3 GetAlignment()
    {
        Vector3 alignmentVec = transform.forward;
        if (neighbor.Count > 0)
        {
            for (int i = 0; i < neighbor.Count; i++)
            {
                alignmentVec += neighbor[i].transform.forward;
            }


        }
        else
        {

            return alignmentVec;
        }

        alignmentVec /= neighbor.Count;

        return alignmentVec;

    }

    public Vector3 GetSeparation()
    {
        Vector3 separationVec = Vector3.zero;
        if (neighbor.Count > 0)
        {
            for (int i = 0; i < neighbor.Count; i++)
            {
                separationVec += (transform.position - neighbor[i].transform.position);
            }
        }
        else
        {
            return separationVec;
        }
        separationVec /= neighbor.Count;

        return separationVec;
    }

}
