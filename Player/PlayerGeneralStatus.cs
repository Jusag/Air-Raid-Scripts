using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneralStatus : MonoBehaviour
{
    [SerializeField] private float Life = 100;
    [SerializeField] private float Defense = 0;
    
    
    
    [Header("GameObject Contains DetectEnemyOnTriggerAndAsigner Script")]
    [SerializeField] private DetectEnemyOnTriggerAndAsigner ConfigOfPlayerVehicle = null;
    [Header("Parameters Config Angles-Range-AirOrFloorDetection")]
    [SerializeField] private int Angle = 0;
    [SerializeField] private int Range = 0;
    [SerializeField] private bool AirDetect = true;

    
    
    [Header("GameObject Contains SmoothLook Script")]
    [SerializeField] private SmoothLook ConfigOfPlayerBaseCannon = null;
    [Header("Parameters Config Speed Of Cannon")]
    [SerializeField] private float BaseCannonSpeed = 0;



    [Header("GameObject Contains ShootConfig Script")]
    [SerializeField] private ShootConfig ConfigOfPlayerCannon = null;
    [Header("Parameters Config BulletPrefab-FireRate-HitEffect")]
    [SerializeField] private GameObject PrefabOfBullet = null;
    [SerializeField] private float FireRate = 0;
    [SerializeField] private GameObject HitEffect = null;




    void Start()
    {
        if(ConfigOfPlayerVehicle != null)
        {
            ConfigOfPlayerVehicle.SetAngle(Angle);
            ConfigOfPlayerVehicle.SetRange(Range);
            ConfigOfPlayerVehicle.SetAirDetection(AirDetect);
        }
        if(ConfigOfPlayerBaseCannon != null)
        {
            ConfigOfPlayerBaseCannon.SetSpeed(BaseCannonSpeed);
        }
        if(ConfigOfPlayerCannon != null)
        {
            ConfigOfPlayerCannon.SetBulletPrefab(PrefabOfBullet);
            ConfigOfPlayerCannon.SetFireRate(FireRate);
            ConfigOfPlayerCannon.SetHitEffect(HitEffect);
        }
            GetComponent<GoodPart>().SetLife(Life);
    }
    //void Update() {}
    public void LoseLife(float Amount)
    {
        Life -= Amount;
        //Debug.Log("My life is "+Life);
        if(Life<=0)
        {
            //Animation Die - Explode effect - Active GUI for continue or restart
            IDie();
        }
    }   
    public void IDie()
    {
        //Agregar Efectops al morir
        Destroy(gameObject);
    }
    public int HowRange()
    {
        return Range;
    }
    public float HowLife()
    {
        return Life;
    }
    public void ChangeTargetButton()
    {
        if(ConfigOfPlayerVehicle)
        {
            //ConfigOfPlayerVehicle.ChangeTarget();
        }
    }



    public DetectEnemyOnTriggerAndAsigner GetDetectEnemyOnTriggerAndAsignerOfPlayer()
    {
        return ConfigOfPlayerVehicle;
    }

}
