using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BomberGeneralStatus : MonoBehaviour
{
    [SerializeField] private int Life = 0;    
    [SerializeField] private int Defense = 0;    
    [SerializeField] private int Speed = 0;
    [SerializeField] private int DiePoints = 0;
    void Start()
    {

    }

    public void IDie()
    {
        Destroy(gameObject);
    }
}

