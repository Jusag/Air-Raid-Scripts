using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class DetectEnemyOnTriggerAndAsigner : MonoBehaviour
{
    private Transform Target;
    [SerializeField] private float MaxAngle;
    [SerializeField] private float MaxRange;
    [SerializeField] private bool AirRangeDetection;
    [SerializeField] private GameObject AuxForAssignTarget;
    
    public bool GizmosOn = true;
    [SerializeField] private List<GameObject> CloseOverlaps;
    
    


    // Start is called before the first frame update
    void Start()
    {
        CloseOverlaps = new List<GameObject>();
        //MaxRange = GetComponent<SphereCollider>().radius;
        
        if(AuxForAssignTarget != null)
            AuxForAssignTarget.GetComponent<SmoothLook>().SetRadio(MaxRange);
    }

    //Update is called once per frame
    void Update()
    {
        ////////TRASNFERIR A ONTRIGGER STAY y redefinir en SMOOTLOOCK que devuelva un TARGET
        
            
    }

    private void AssignTarjetOrClear()
    {
        //if (CalculateEnemyInList() != null && CloseOverlaps.Count > 0)
        if (CloseOverlaps.Count > 0)
        {
            SmoothLook VariableForAsignTarget = AuxForAssignTarget.GetComponent<SmoothLook>();
            VariableForAsignTarget.SetTarget(CalculateEnemyInList().transform);
        }
        else CloseOverlaps.Clear();
    }
    public void AssignNextTarjet()
    {
        if(CloseOverlaps.Count>0)
        {
            SmoothLook VariableForAsignTarget = AuxForAssignTarget.GetComponent<SmoothLook>();
            GameObject ActualEnemy = VariableForAsignTarget.GetTarget().gameObject;
            int ControlPos = -1;
            for(int i=0; i<CloseOverlaps.Count; i++)
            {
                if(CloseOverlaps[i] == ActualEnemy)
                {
                    ControlPos = i;
                    Debug.Log("Controlando");
                }
            }
            Debug.Log(ControlPos);
            if(ControlPos != -1)
            {
                if(ControlPos+1 < CloseOverlaps.Count)
                {
                    VariableForAsignTarget.SetNextTarget(CloseOverlaps[ControlPos+1].transform);
                }
                else
                {
                    VariableForAsignTarget.SetNextTarget(CloseOverlaps[0].transform);
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        GameObject AuxParent = null;
        if(other.gameObject.name == "BoxTarget") //Name of one of childs in enemys GameObjects
        {
            
            AuxParent = other.transform.parent.parent.gameObject;

            if(AuxParent.GetComponent<Enemy>())
            {
                //Debug.Log("Estoy en enter!");
                Vector3 DirectionBetween = (other.transform.position - transform.position).normalized;
                float AngleForward = Vector3.Angle(DirectionBetween, transform.up);
                if (AirRangeDetection && AngleForward < MaxAngle)
                {
                    Debug.Log("Estoy en enter AIIIIIRE!");
                    CloseOverlaps.Add(other.gameObject);
                }
                else
                {
                    if(!AirRangeDetection && AngleForward > 90-MaxAngle)
                    {
                        Debug.Log("Estoy en enter TIERRA!");
                        CloseOverlaps.Add(other.gameObject);
                    }
                }
            }
            else 
            { 
                //Debug.Log("Es Nulo"); 
            }
        }
        AssignTarjetOrClear();
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject AuxParent = null;
        //if(other.gameObject.name == "Mesh") //Name of one of childs in enemys GameObjects
        if(other.gameObject.name == "BoxTarget") //Name of one of childs in enemys GameObjects
        {
            Debug.Log("Estoy en stay");
            AuxParent = other.transform.parent.gameObject;
            
            //if(AuxParent.GetComponent<Enemy>())
            //if(other && Vector3.Distance(transform.position,other.transform.position)> MaxRange) 
            
            
            
            bool StayInList = false;
            
            if(CloseOverlaps.Count > 0)
            {
                foreach (GameObject ListAuxGameObject in CloseOverlaps)
                {
                    if (ListAuxGameObject == other.gameObject)
                    {
                        StayInList = true;
                        //Debug.Log(StayInList);
                    }
                }
            }
            if (!StayInList)
            {
                //if (other.gameObject.GetComponent<Enemy>() != null)
                if (AuxParent.GetComponent<Enemy>())
                {
                    Vector3 DirectionBetween = (other.transform.position - transform.position).normalized;
                    float AngleForward = Vector3.Angle(DirectionBetween, transform.up);
                    if (AirRangeDetection && AngleForward < MaxAngle)
                    {
                        CloseOverlaps.Add(other.gameObject);
                    }
                    else
                    {
                        if (!AirRangeDetection && AngleForward > 90 - MaxAngle)
                        {
                            CloseOverlaps.Add(other.gameObject);
                        }
                    }
                }
            }
            
            //if(AuxParent && Vector3.Distance(transform.position,other.transform.position) > MaxRange) 
            if(other.gameObject && Vector3.Distance(transform.position,other.transform.position) > MaxRange) 
            {
               RemoveEnemyFromList(other.gameObject);
            }
        }
        ReAjustList();
        
        AssignTarjetOrClear();
    }
    private void OnTriggerexit(Collider other)
    {
        RemoveEnemyFromList(other.gameObject);
        
        AssignTarjetOrClear();
    }
    private void RemoveEnemyFromList(GameObject aux)
    {
        if(CloseOverlaps.Count > 0)
        {
            //Debug.Log(CloseOverlaps.Count);

            for(int i=0; i<CloseOverlaps.Count; i++)
            {
                if (CloseOverlaps[i] != null)
                {
                    if(CloseOverlaps[i] == aux)
                    {
                        CloseOverlaps.Remove(CloseOverlaps[i]);
                    }
                }
            }
        }
        ReAjustList();
    }
    private void ReAjustList()
    {
        List<GameObject> CloseOverlapsTemp= new List<GameObject>();
        for (int i=0; i < CloseOverlaps.Count; i++)
        {
            if (CloseOverlaps[i] != null)
                CloseOverlapsTemp.Add(CloseOverlaps[i]);
        }
        if (CloseOverlapsTemp == null)
        {
            CloseOverlapsTemp.Clear();
        }
        else 
        {
            CloseOverlaps = CloseOverlapsTemp;
        }
    }
    private GameObject CalculateEnemyInList()
    {
        GameObject AuxGameObjectNear = null;
        float MinValue = Mathf.Infinity;
        if(CloseOverlaps.Count > 0)
        {
            foreach (GameObject ListAuxGameObject in CloseOverlaps)
            {
                if (ListAuxGameObject != null)
                {
                    if (Vector3.Distance(ListAuxGameObject.transform.position,transform.position) < MinValue)
                    {
                        MinValue = Vector3.Distance(ListAuxGameObject.transform.position, transform.position);
                        AuxGameObjectNear = ListAuxGameObject;
                    }
                }
            }
        }
        return AuxGameObjectNear;
    }
    public float HowRadius()
    {
        return MaxRange;
    }
    //Returns And ModifiVoids
    public void SetRange(int AuxLocal)
    {
        MaxRange = AuxLocal;
        GetComponent<SphereCollider>().radius = MaxRange;
    }
    public void SetAngle(int AuxLocal)
    {
        MaxAngle = AuxLocal;
    }
    public void SetAirDetection(bool AuxLocal)
    {   
        AirRangeDetection = AuxLocal;
    }



    private void OnDrawGizmos() //Auxiliary GIZMOS for angles
    {
        if (GizmosOn)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, MaxRange);
            Vector3 fovLine1 = Quaternion.AngleAxis(MaxAngle, transform.forward) * transform.up * MaxRange;
            Vector3 fovLine2 = Quaternion.AngleAxis(-MaxAngle, transform.forward) * transform.up * MaxRange;
            Vector3 fovLine3 = Quaternion.AngleAxis(MaxAngle, transform.right) * transform.up * MaxRange;
            Vector3 fovLine4 = Quaternion.AngleAxis(-MaxAngle, transform.right) * transform.up * MaxRange;
            Gizmos.color = Color.blue;

            if (MaxAngle > 90) { MaxAngle = 90; }
            if (MaxAngle < 0) { MaxAngle = 0; }

            //Base Angle Gizmos
            //Gizmos.DrawRay(transform.position, fovLine1);
            //Gizmos.DrawRay(transform.position, fovLine2);
            //Gizmos.DrawRay(transform.position, fovLine3);
            //Gizmos.DrawRay(transform.position, fovLine4);
            Vector3 fovLine1b, fovLine2b, fovLine3b, fovLine4b;
            float MaxAngleAux = MaxAngle;
            for (float i = MaxAngleAux; i > 0; i--)
            {
                if (AirRangeDetection)
                {
                    fovLine1b = Quaternion.AngleAxis(i, transform.forward) * transform.up * MaxRange;
                    fovLine2b = Quaternion.AngleAxis(-i, transform.forward) * transform.up * MaxRange;
                    fovLine3b = Quaternion.AngleAxis(i, transform.right) * transform.up * MaxRange;
                    fovLine4b = Quaternion.AngleAxis(-i, transform.right) * transform.up * MaxRange;
                }
                else
                {
                    fovLine1b = Quaternion.AngleAxis(i, transform.forward) * transform.right * MaxRange;
                    fovLine2b = Quaternion.AngleAxis(-i, transform.forward) * (transform.right * -1) * MaxRange;
                    fovLine3b = Quaternion.AngleAxis(i, transform.right) * (transform.forward * -1) * MaxRange;
                    fovLine4b = Quaternion.AngleAxis(-i, transform.right) * transform.forward * MaxRange;
                }
                Gizmos.DrawRay(transform.position, fovLine1b);
                Gizmos.DrawRay(transform.position, fovLine2b);
                Gizmos.DrawRay(transform.position, fovLine3b);
                Gizmos.DrawRay(transform.position, fovLine4b);
            }

            //Rectas sobre el plano x para visualizar el angulo
            //Gizmos.DrawRay(transform.position, fovLine3);
            //Gizmos.DrawRay(transform.position, fovLine4);

            if (true)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                //Gizmos.color = Color.green;
            }
            if (Target != null)
            {
                Gizmos.DrawRay(transform.position, (Target.position - transform.position).normalized * MaxRange);
            }

            Gizmos.color = Color.black;
            //Gizmos.DrawRay(transform.position, transform.forward * MaxRange);
        }
    }
}
