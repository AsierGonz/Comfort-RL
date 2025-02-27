


using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class CustomAgentController : MonoBehaviour
{
    public Agent myAgent; // Aseg�rate de asignar esto en el Inspector de Unity
    public bool isAgentActive;
    private bool startWaiting = false; // ASEGURARSE QUE SI SE QUIERE HACER CON PARADA DESPUES DE CADA ENTRENAMIENTO SE HAGA ; 
    [SerializeField] public CobotAgent agenteCobot;


    void Start()
    {
        // Opcionalmente, desactiva el agente al inicio si necesitas m�s control

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
        myAgent.RequestDecision(); // Solicita la primera decisi�n
        isAgentActive = true; // Asegura que se active solo una vez o seg�n lo dise�es
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
    public Agent myAgent; // Aseg�rate de asignar esto en el Inspector de Unity
    public bool isAgentActive;
    private bool startWaiting = true;

    // Definir el bot�n del mando que quieres comprobar
    private InputDevice controller;
    private InputFeatureUsage<bool> buttonToCheck = CommonUsages.triggerButton; // Ejemplo: bot�n del gatillo

    void Start()
    {
        // Opcionalmente, desactiva el agente al inicio si necesitas m�s control
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
            controller = devices[0]; // Asume que usas el mando derecho, ajusta seg�n sea necesario
        }
    }

    void Update()
    {
        // Comprobar si el bot�n espec�fico del mando se ha presionado
        bool buttonPressed = false;
        if (controller.TryGetFeatureValue(buttonToCheck, out buttonPressed) && buttonPressed && !isAgentActive)
        {
            ActivateAgent();
        }
    }

    void ActivateAgent()
    {
        myAgent.enabled = true; // Activa el agente
        myAgent.RequestDecision(); // Solicita la primera decisi�n
        isAgentActive = true; // Asegura que se active solo una vez o seg�n lo dise�es
    }
}


*/