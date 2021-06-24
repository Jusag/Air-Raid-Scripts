using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeTargetScript : MonoBehaviour
{
    [SerializeField] private GameController AuxLocalGameController = null;

    void Start() 
    { 
        AuxLocalGameController = FindObjectOfType<GameController>();
    }

    //void Update() { }
    public void ChangeTargetMethod()
    {
        if(AuxLocalGameController != null)
            AuxLocalGameController.GetActualPlayer().GetComponent<ChangeTargetButton>().ChangeTargetFromUI();
    }

}