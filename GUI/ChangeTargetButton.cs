using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTargetButton : MonoBehaviour
{
    void Start(){ }
    //void Update(){ }

    public void ChangeTargetFromUI()
    {
        GetComponent<PlayerGeneralStatus>().GetDetectEnemyOnTriggerAndAsignerOfPlayer().AssignNextTarjet();
    }
    
}
