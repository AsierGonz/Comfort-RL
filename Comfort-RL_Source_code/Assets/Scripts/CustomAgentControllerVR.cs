using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine.XR; // Importa el namespace para XR
using System.Collections; // Necesario para usar corutinas
using System.Collections.Generic; // Para usar List

public class CustomAgentControllerVR : MonoBehaviour
{
    public Agent myAgent;

    public bool isAgentActive;
    private bool startWaiting = true;  // CUIDADO

    [SerializeField] public CobotAgent agenteCobot;
    [SerializeField] public ModelSwitch switcher;

    private InputDevice leftController;
    private InputDevice rightController;
    private InputFeatureUsage<bool> triggerButtonToCheck = CommonUsages.triggerButton;
    private InputFeatureUsage<bool> gripButtonToCheck = CommonUsages.gripButton;

    private InputFeatureUsage<bool> buttonBToCheck = CommonUsages.primaryButton;
    private InputFeatureUsage<bool> buttonAToCheck = CommonUsages.secondaryButton;
    private InputFeatureUsage<bool> buttonYToCheck = CommonUsages.primaryButton;
    private InputFeatureUsage<bool> buttonXToCheck = CommonUsages.secondaryButton;

    private bool canPressButton = true; // Agregar esta línea

    void Start()
    {
        isAgentActive = false;
        if (startWaiting == true)
        {
            myAgent.enabled = false;
        }

        var leftDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftDevices);
        if (leftDevices.Count > 0)
        {
            leftController = leftDevices[0];
        }

        var rightDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightDevices);
        if (rightDevices.Count > 0)
        {
            rightController = rightDevices[0];
        }
    }

    void Update()
    {
        bool buttonBPressed = false;
        bool buttonAPressed = false;
        bool gripButtonPressedLeft = false;
        bool gripButtonPressedRight = false;
        bool triggerButtonPressedLeft = false;
        bool triggerButtonPressedRight = false;

        if (canPressButton)
        {
            // Revisar el botón B (Establecer takeLow a true)
            if (rightController.TryGetFeatureValue(buttonBToCheck, out buttonBPressed) && buttonBPressed)
            {
                switcher.takeTheLow = true;
                StartCoroutine(DisableButtonPress());
                
            }

            // Revisar el botón A (Establecer takeLow a false)
            if (rightController.TryGetFeatureValue(buttonAToCheck, out buttonAPressed) && buttonAPressed)
            {
                switcher.takeTheLow = false;
                StartCoroutine(DisableButtonPress());
                
            }
        }

        // Revisar el botón Grip (Resetear el robot)
        if (leftController.TryGetFeatureValue(gripButtonToCheck, out gripButtonPressedLeft) && gripButtonPressedLeft)
        {
            ResetAllAgents();
        }

        if (rightController.TryGetFeatureValue(gripButtonToCheck, out gripButtonPressedRight) && gripButtonPressedRight)
        {
            ResetAllAgents();
        }
        // Revisar el botón de disparo (gatillo) del controlador izquierdo
        if (leftController.TryGetFeatureValue(triggerButtonToCheck, out triggerButtonPressedLeft) && triggerButtonPressedLeft)
        {
            ActivateAgent(myAgent, agenteCobot);
        }

        // Revisar el botón de disparo (gatillo) del controlador derecho
        if (rightController.TryGetFeatureValue(triggerButtonToCheck, out triggerButtonPressedRight) && triggerButtonPressedRight)
        {
            ActivateAgent(myAgent, agenteCobot);
        }

    }

    IEnumerator DisableButtonPress()
    {
        canPressButton = false;
        yield return new WaitForSeconds(1);
        canPressButton = true;
    }

    void ActivateAgent(Agent agent, CobotAgent cobotAgent)
    {
        agent.enabled = true;
        agent.RequestDecision();
        isAgentActive = true;
    }

    void ResetAllAgents()
    {
        agenteCobot.cobotControl.ResetCobot(1);
        myAgent.enabled = false;
        isAgentActive = false;
    }
}
