using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitOnBox : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToCallFather = null;
    //private Enemy ImEnemy;
    //private GoodPart ImAllyOrPlayer;
    void Start()
    {
        
    }
    //void Update(){ }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BulletConfig>()) 
        {
            if(ObjectToCallFather.transform.parent.GetComponent<Enemy>())
            {
                ObjectToCallFather.transform.parent.GetComponent<Enemy>().DoDamage(other.gameObject.GetComponent<BulletConfig>().GetDamage());
            }
            else
            {   
                if(ObjectToCallFather.transform.parent.GetComponent<GoodPart>())
                {
                    ObjectToCallFather.transform.parent.GetComponent<GoodPart>().DoDamage(other.gameObject.GetComponent<BulletConfig>().GetDamage());
                }
            }
            Destroy(other.gameObject);
        }
    }   
}
