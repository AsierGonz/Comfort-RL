using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectColision : MonoBehaviour
{

    [SerializeField] public CobotAgent agenteCobot;


    private bool lightBlink = false;

    private float temperatura = 0.0f; 

    [SerializeField] public LightBlink blink;

    private void OnCollisionEnter(Collision collision)
    {


        //agenteCobot.cobotControl.hayColision = true;

        if (agenteCobot.enEntranamiento == true) {


            if (!collision.gameObject.CompareTag("NotCollision") && !collision.gameObject.CompareTag("Goal"))
            {
                //Debug.Log("COLLISION DETECTED TO: " + collision.gameObject.name + " FROM: " + gameObject.name);

                if (collision.gameObject.CompareTag("Person"))
                {
                    // Llama a la función de colisión con personas.
                    //agenteCobot.CollisionRobot();

                    agenteCobot.GiveReward(-1f * temperatura); // temperatura del choque 

                    //Debug.Log(agenteCobot.GetCumulativeReward()); 

                    Debug.Log("HAS CHOCADO PERSONA"); 

                    if (lightBlink == true)
                    {

                        Debug.Log("LightBlink"); 

                        blink.StartBlinking(); 
                    }

                    agenteCobot.CollisionRobot();


                }
                else
                {
                    // Si no es una colisión con "persona", llama a la función estándar de colisión.
                    agenteCobot.CollisionRobot(); 
                    
                    
                    //agenteCobot.GiveReward(-1f); //NO ACTIVAR CUANDO SEA EL INICIO DEL ENTRENAMIENTO 
                    Debug.Log("HAS CHOCADO OTRA COSA");
                }
            }

            
        }
    }
}
