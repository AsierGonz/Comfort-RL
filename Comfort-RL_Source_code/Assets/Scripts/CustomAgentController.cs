


using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class CustomAgentController : MonoBehaviour
{
    public Agent myAgent; // Asegúrate de asignar esto en el Inspector de Unity
    public bool isAgentActive;
    private bool startWaiting = false; // ASEGURARSE QUE SI SE QUIERE HACER CON PARADA DESPUES DE CADA ENTRENAMIENTO SE HAGA ; 
    [SerializeField] public CobotAgent agenteCobot;


    void Start()
    {
        // Opcionalmente, desactiva el agente al inicio si necesitas más control

        isAgentActive = false; 
        if(startWaiting == true)
        {
            myAgent.enabled = false;
        }
      
    }

    void Update()
    {
        // Ejemplo: Activa el agente con la tecla "K"
        if (Input.GetKeyDown(KeyCode.K) && !isAgentActive)
        {
            ActivateAgent();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isAgentActive)
        {
            agenteCobot.cobotControl.ResetCobot(1); 
        }

    }

    void ActivateAgent()
    {
        myAgent.enabled = true; // Activa el agente
        myAgent.RequestDecision(); // Solicita la primera decisión
        isAgentActive = true; // Asegura que se active solo una vez o según lo diseñes
    }
}





// <-------------------------------------- DIVISION HERE ------------------------------------------------> 



/* 


using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine.XR; // Importa el namespace para XR
using System.Collections.Generic; // Para usar List


public class CustomAgentController : MonoBehaviour
{
    public Agent myAgent; // Asegúrate de asignar esto en el Inspector de Unity
    public bool isAgentActive;
    private bool startWaiting = true;

    // Definir el botón del mando que quieres comprobar
    private InputDevice controller;
    private InputFeatureUsage<bool> buttonToCheck = CommonUsages.triggerButton; // Ejemplo: botón del gatillo

    void Start()
    {
        // Opcionalmente, desactiva el agente al inicio si necesitas más control
        isAgentActive = false;
        if (startWaiting == true)
        {
            myAgent.enabled = false;
        }

        // Obtener el dispositivo (mando) de Oculus Quest 2
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0)
        {
            controller = devices[0]; // Asume que usas el mando derecho, ajusta según sea necesario
        }
    }

    void Update()
    {
        // Comprobar si el botón específico del mando se ha presionado
        bool buttonPressed = false;
        if (controller.TryGetFeatureValue(buttonToCheck, out buttonPressed) && buttonPressed && !isAgentActive)
        {
            ActivateAgent();
        }
    }

    void ActivateAgent()
    {
        myAgent.enabled = true; // Activa el agente
        myAgent.RequestDecision(); // Solicita la primera decisión
        isAgentActive = true; // Asegura que se active solo una vez o según lo diseñes
    }
}


*/