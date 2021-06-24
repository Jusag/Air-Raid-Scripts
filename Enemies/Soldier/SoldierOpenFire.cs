using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierOpenFire : MonoBehaviour
{
    [SerializeField] private GameObject MyBody = null;
    [SerializeField] private GameObject ActualTarget = null;
    //private GameObject StatusActualTarget = null;
    [SerializeField] private float DistanceToFire = 0;
    private bool InCooldown = false;
    [SerializeField]private float FireRateSeconds = 0;
    [SerializeField]private GameObject GameObjectBulletSpawner = null;
    [SerializeField]private GameObject BulletPrefab = null;

    void Start()
    {
        InCooldown = false;
    }

    void Update()   
    {
        if(MyBody != null)
        {
            /* if(ActualTarget != null && DistanceToFire == 0)
            {
                DistanceToFire = (ActualTarget.transform.parent.GetComponent<PlayerGeneralStatus>().HowRange());
            } */
            if(MyBody.GetComponent<OnTheFloor>().AreYouOnAir() == false)
            {
                if((GetComponent<NavAgentAI>().isActiveAndEnabled == true) && (ActualTarget == null))
                {
                    ActualTarget = GetComponent<NavAgentAI>().ActualTarget();
                }
                if(ActualTarget != null)
                {
                    float RangeControl = Vector3.Distance(ActualTarget.transform.position, transform.position);
                    bool CleanFireLine = false;
                    RaycastHit hit;
                    Vector3 AuxRayPosition = new Vector3(transform.position.x, ActualTarget.transform.position.y, transform.position.z);
                    Vector3 FollowRayDiference = ActualTarget.transform.position - GameObjectBulletSpawner.transform.position;
                    FollowRayDiference.y = 0;
                    Debug.DrawRay(AuxRayPosition, FollowRayDiference, Color.green, 0.1f, true);
                    //With ray search player or ally
                    if(Physics.Raycast(AuxRayPosition, FollowRayDiference, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
                    {
                        //Debug.Log(hit.transform.name);
                        if(hit.collider.gameObject.tag == "PlayerVehicle" || hit.collider.gameObject.tag == "Ally")
                        {
                            CleanFireLine = true;     
                        }
                    }

                    if(DistanceToFire >= RangeControl && CleanFireLine)
                    {
                        GetComponent<NavMeshAgent>().destination = transform.position;
                        Vector3 ActualTargetVector = new Vector3(ActualTarget.transform.position.x,transform.position.y, ActualTarget.transform.position.z );
                        transform.LookAt(ActualTargetVector);
                        StartCoroutine(Fire());
                    }
                    else
                    {
                        if(GetComponent<NavMeshAgent>())
                            GetComponent<NavMeshAgent>().destination = ActualTarget.transform.position;
                    }
                }
            }
        }
    }

    public void SetDistanceOpenFire(float LocalAux)
    {
        DistanceToFire = LocalAux;
    }

    IEnumerator Fire()
    {
        if(InCooldown == false)
        {
            InCooldown = true;
            Instantiate(BulletPrefab,GameObjectBulletSpawner.transform.position, transform.rotation);
            yield return new WaitForSeconds(FireRateSeconds);
            InCooldown = false;
        }
        yield return null;
    }
}
