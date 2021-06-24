using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject PlayerMainGameObject = null;

    void Awake() 
    {
        //Change AWAKE for Instantiate and Assign 
        PlayerMainGameObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Start() { }

    //void Update() { }

    public GameObject GetActualPlayer()
    {
        return PlayerMainGameObject;
    }





    
    
    
    












    //OLD.....TO DELETE WHEN IT'S IMPLEMENTED

    public void avionetadestruido(){}
    public void helicopterodropdestruido(){}
    public void aviondestruido(){}
    public void soldadodestruido(){}
    public void game_over(){}
    public void soldadocreado(){}
    public void soldadoscreado(){}
}
