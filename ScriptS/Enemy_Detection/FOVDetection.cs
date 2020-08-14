using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDetection : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float maxAngle;
    [SerializeField]
    private float maxRadius;
    [SerializeField]
    private bool isinFOV = false;
    void Start()
    {
        
    }
    void Update()
    {
        /*
        if (target != null)
        {
            isinFOV = inFOV(transform, target, maxAngle, maxRadius);
            target = SearchForFOV();
        }
        else
        {
            target = SearchForFOV();
        }
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.forward) * transform.up * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.forward) * transform.up * maxRadius;
        Vector3 fovLine3 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine4 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
        
        //Rectas sobre el plano x para visualizar el angulo
        //Gizmos.DrawRay(transform.position, fovLine3);
        //Gizmos.DrawRay(transform.position, fovLine4);

        if (!isinFOV)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        if (target != null)
        {
            Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized * maxRadius);
        }

        Gizmos.color = Color.black;
        //Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }
    private bool inFOV(Transform origin, Transform t_target, float maxAngle, float maxRadius)
    {
        if (t_target != null)
        {

            Collider[] overlaps = new Collider[10];
            int count = Physics.OverlapSphereNonAlloc(origin.position, maxRadius, overlaps);

            for (int i = 0; i < count + 1; i++)
            {
                if (overlaps[i] != null)
                {
                    if (overlaps[i].transform == t_target)
                    {
                        Vector3 directionBetween = (t_target.position - origin.position).normalized;

                        float angleforward = Vector3.Angle(directionBetween, origin.up);

                        //Debug.Log(angleforward);
                        if (angleforward > maxAngle)
                        {
                            /*
                            Ray ray = new Ray(origin.position, t_target.position - origin.position);
                            RaycastHit hit;

                            if(Physics.Raycast(ray, out hit, maxRadius))
                            {
                                if(hit.transform == t_target) 
                                {
                                    return true;
                                }
                            }
                            */
                            //target = null;
                            return true;
                        }
                    }
                }
            }
        }
        target = null;
        return false;
    }
    private Transform SearchForFOV()
    {
        float distanceToCloseEnemy = Mathf.Infinity;
        enemy closeEnemy = null;
        enemy[] allEnemies = GameObject.FindObjectsOfType<enemy>();
        foreach(enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            if(distanceToEnemy < distanceToCloseEnemy)
            {
                distanceToCloseEnemy = distanceToEnemy;
                closeEnemy = currentEnemy;
            }
        }
        if (closeEnemy != null)
        {
            return closeEnemy.gameObject.transform;
        }
        return null;
    }
    
    public Transform inAreaControl(Transform aux)
    {
        isinFOV = inFOV(transform, aux, maxAngle, maxRadius);
        if (isinFOV)
        {
            return aux;
        }
        return null;
    }
    public Transform tarjet_close()
    {
        target = SearchForFOV();
        if (target != null)
        {
            isinFOV = inFOV(transform, target, maxAngle, maxRadius);
        }
        if (!isinFOV)
        {
            target = null;
        }
        return target;
    }
}
    