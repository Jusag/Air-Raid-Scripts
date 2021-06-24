using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierGeneralStatus : MonoBehaviour
{
    
    [SerializeField] private float Life = 0;    
    [SerializeField] private float Defense = 0;    
    [SerializeField] private float Speed = 0;
    [SerializeField] private int DiePoints = 0;
    [SerializeField] private float DistanceForFire = 0;
    
    void Start()
    {
        GetComponent<Enemy>().SetLife(Life);
        
        GetComponent<SoldierOpenFire>().SetDistanceOpenFire(DistanceForFire);
    }

    //void Update(){ }

    public void IDie()
    {
        Destroy(gameObject);
    }
    public void LoseLife(float Amount)
    {
        Life -= Amount;
        if(Life<=0)
        {
            //Animation Die - Explode effect - Active GUI for continue or restart
            IDie();
        }
    }   
    public float HowLife()
    {
        return Life;
    }
}
