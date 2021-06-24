using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLook : MonoBehaviour
{
    
    [SerializeField] private Transform m_Target;
    [SerializeField] private Transform Cannon_Son;
    [SerializeField] private float m_Speed;
    [SerializeField] private float DefineLocalRadius;


    private Vector3 LTargetDirectionBase;
    private Vector3 LTargetDirectionTurret;


    private GameObject AuxChildOfCannon;
    void Start()
    {
        AuxChildOfCannon = Cannon_Son.GetChild(0).gameObject;
    }

    void Update()
    {
        if (m_Target!=null && Vector3.Distance(m_Target.transform.position, transform.position) > DefineLocalRadius)
        {
            m_Target = null;
            LTargetDirectionBase = Vector3.zero;
            LTargetDirectionTurret = Vector3.zero;
        }
        if (m_Target != null)
        {
            if (gameObject.tag == "BaseTank")
            {
                LTargetDirectionBase = m_Target.position - transform.position;
                LTargetDirectionBase.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(LTargetDirectionBase), Time.deltaTime * m_Speed);
            }
            if (Cannon_Son.tag == "CanonTank")
            {
                LTargetDirectionTurret = m_Target.position - Cannon_Son.position;
                Cannon_Son.rotation = Quaternion.RotateTowards(Cannon_Son.rotation, Quaternion.LookRotation(LTargetDirectionTurret), Time.deltaTime * m_Speed);
                Vector3 DirectioCannonToTarget = (Cannon_Son.transform.position - m_Target.transform.position).normalized;
                float DotProduct = Vector3.Dot(DirectioCannonToTarget, Cannon_Son.transform.forward);
                if (DotProduct < 0)
                {
                    DotProduct = DotProduct * -1;
                }
                if (DotProduct > 0.9f) //In range for fire
                {
                    if (AuxChildOfCannon != null && AuxChildOfCannon.GetComponent<ShootConfig>() != null)
                    {
                        AuxChildOfCannon.GetComponent<ShootConfig>().OpenFire();
                    }
                }
                else
                { 
                    //Debug.Log(DotProduct); 
                }
            }
        }
    }
    public void SetSpeed(float AuxSpeed)
    {
        m_Speed = AuxSpeed;
    }
    public void SetRadio(float AuxRadio)
    {
        DefineLocalRadius = AuxRadio;
    }
    public void SetTarget(Transform TargetNear)
    {
        if (TargetNear != null && m_Target == null)
        {
            m_Target = TargetNear;
        }
    }
    public void SetNextTarget(Transform NextTarget)
    {
        m_Target = NextTarget;
    }
    
    public Transform GetTarget()
    {
        return m_Target;
    }

}
