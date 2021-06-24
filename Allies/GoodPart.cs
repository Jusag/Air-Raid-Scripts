using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodPart : MonoBehaviour
{
    //"DONT EDIT VALUES HERE - This class Copy GENERALSTATUS")]
    private float Health = 0;
    private float HealthFull = 0;
    private float Defense = 0;
    
    [Header("GUI Life")]
    public Image HealthBar;
    void Start()
    {
        
    }
    //void Update(){ }
    public void DoDamage(float DamageAux)
    {
        if(GetComponent<PlayerGeneralStatus>())
        {
            GetComponent<PlayerGeneralStatus>().LoseLife(DamageAux);
        }
        else
        {   ///COMPLETE CREATE ALLYSTATUS!!!
            //if(GetComponent<AllyGeneralStatus>())
            {
                //GetComponent<AllyGeneralStatus>().LoseLife(DamageAux)
            }
        }

        if(GetComponent<PlayerGeneralStatus>())
        {
            Health = GetComponent<PlayerGeneralStatus>().HowLife();
        }
        else
        {   ///COMPLETE CREATE ALLYSTATUS!!!
            //if(GetComponent<AllyGeneralStatus>())
            {
                //Health = GetComponent<AllyGeneralStatus>().HowLife();
            }
        }

        HealthBar.fillAmount = (Health*100/HealthFull)/100;
        
    }
    public void SetLife(float LocalAux)
    {
        Health = LocalAux;
        HealthFull = LocalAux;
    }
}
