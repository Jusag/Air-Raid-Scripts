using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    //"DONT EDIT VALUES HERE - This class Copy GENERALSTATUS")]
    private float Health = 0;
    private float HealthFull = 0;
    
    [Header("GUI Life")]
    public Image HealthBar;

    void Start()
    {
        //HealthFull = Health;
    }

    //void Update(){}

    public void DoDamage(float DamageAux)
    {
        if(GetComponent<SoldierGeneralStatus>())
        {
            GetComponent<SoldierGeneralStatus>().LoseLife(DamageAux);
        }
        else
        {   ///COMPLETE CREATE ALLYSTATUS!!!
            //if(GetComponent<AirplaneGeneralStatus>())
            {
                //GetComponent<AirplaneGeneralStatus>().LoseLife(DamageAux)
            }
            //else etc... 
        }

        if(GetComponent<SoldierGeneralStatus>())
        {
            Health = GetComponent<SoldierGeneralStatus>().HowLife();
        }
        else
        {   ///COMPLETE CREATE ALLYSTATUS!!!
            //if(GetComponent<AirplaneGeneralStatus>())
            {
                //Health = GetComponent<AirplaneGeneralStatus>().HowLife();
            }
            //else etc..
        }
        HealthBar.fillAmount = (Health*100/HealthFull)/100;
    }
    public void SetLife(float LocalAux)
    {
        Health = LocalAux;
        HealthFull = LocalAux;
    }
}
