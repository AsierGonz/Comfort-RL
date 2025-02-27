using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewarddetectColision : MonoBehaviour
{

    [SerializeField] public CobotAgent agenteCobot;
    [SerializeField] public GameObject sferaContactoCobot;


    private void OnCollisionEnter(Collision collision)
    {
        // DEPRECATED - NOT USED - ONLY USED WHEN DETECTING COLLISIONS AND REWARDING - NOW DISTANCE REWARD 
            //FINISH AND GOAL! 
        //agenteCobot.GoalReached();
        //Debug.Log("COLLISION DETECTED TO: " + collision.gameObject.name + " FROM: " + gameObject.name);
        //Debug.Log("GOAL REACH"); 

       
    }
}
