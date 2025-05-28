using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Policies;
using Unity.Barracuda;


public class ModelSwitch : MonoBehaviour
{
    [SerializeField] private NNModel modelFaseOne;
    [SerializeField] private NNModel modelFaseTwo;
    [SerializeField] private NNModel modelFaseThree;
    [SerializeField] private BehaviorParameters behaviorParameters;

    public bool takeTheLow = true; 


    public void SwitchModel(int fase) //Called for example when ending the phase --> Fin fase 1 : SwitchModel(1) Meaning the model fase 2 will be On
    {
        
        if (fase == 1) {
            //behaviorParameters.BrainParameters.VectorObservationSize = 14; 
            if(takeTheLow) { 
                behaviorParameters.Model = modelFaseOne;
            }
            else { 
                behaviorParameters.Model = modelFaseTwo;
            }
        }
        if (fase == 3) {
            Debug.Log("SWITCH"); 
            //behaviorParameters.BrainParameters.VectorObservationSize = 14; 
            behaviorParameters.Model = modelFaseThree; }

    }


}
