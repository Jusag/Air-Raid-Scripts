using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smooth_look_at_with_FOV : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private Transform m_Cannon_Son;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private FOVDetection fov_local;

    void Start()
    {
        fov_local = GetComponent<FOVDetection>();
        m_Cannon_Son = gameObject.transform.GetChild(0).transform;
}

    void Update()
    {
        if (m_Target != null)
        {
            if (gameObject.tag == "Base_Tank")
            {
                Vector3 lTargetDir = m_Target.position - transform.position;
                lTargetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.deltaTime * m_Speed);
                m_Target = fov_local.inAreaControl(m_Target);
            }
            if (m_Cannon_Son.tag == "Canon_Tank")
            {
                if (m_Target != null)
                {
                    Vector3 lTargetDir2 = m_Target.position - m_Cannon_Son.position;
                    m_Cannon_Son.rotation = Quaternion.RotateTowards(m_Cannon_Son.rotation, Quaternion.LookRotation(lTargetDir2), Time.deltaTime * m_Speed / 2);
                    if (m_Cannon_Son.localRotation == Quaternion.identity) 
                    {
                        Debug.Log("FUEGO");
                    }
                }
            }
            
        }
        else
        {
            if (gameObject.tag == "Base_Tank")
            {
                m_Target = fov_local.tarjet_close();
            }
        }
    }
    public void assignTarget(Transform auxTransform)
    {
        m_Target = auxTransform;
    }
    public void assignSpeed(float auxSpeed)
    {
        m_Speed = auxSpeed;
    }
}
