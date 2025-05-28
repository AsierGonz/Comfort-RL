using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class CustomAgentControllerVR_key : MonoBehaviour
{
    public Agent myAgent; // Asignar en el Inspector

    public bool isAgentActive;
    private bool startWaiting = true; // Control de inicio de activación
    [SerializeField] public CobotAgent agenteCobot; // Asignar en el Inspector


    [SerializeField] public ModelSwitch switcher; // Asignar en el Inspector para controlar takeLow

    void Start()
    {
        // Desactivar agentes al inicio si es necesario
        isAgentActive = false;
        if (startWaiting)
        {
            myAgent.enabled = false;

        }
    }

    void Update()
    {
        // Resetear todos los agentes con la tecla "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAllAgents();
            Debug.Log("key R - Reset All Agents");
        }

        // Establecer takeLow a true con la tecla "L"
        if (Input.GetKeyDown(KeyCode.L))
        {
            switcher.takeTheLow = true;
            Debug.Log("key L - takeLow set to true");
            
        }

        // Establecer takeLow a false con la tecla "K"
        if (Input.GetKeyDown(KeyCode.K))
        {
            switcher.takeTheLow = false;
            Debug.Log("key K - takeLow set to false");
            
        }

        // Resetear el robot con la tecla "P" (función de ResetAllAgents)
        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateAgent(myAgent, agenteCobot);
            Debug.Log("key P - Robot Reset");
        }
    }

    void ActivateAgent(Agent agent, CobotAgent cobotAgent)
    {
        agent.enabled = true; // Activar el agente
        agent.RequestDecision(); // Solicitar decisión inicial
        isAgentActive = true; // Indicar que un agente está activo
    }

    void ResetAllAgents()
    {
        // Resetear todos los cobots
        agenteCobot.cobotControl.ResetCobot(1);


        // Desactivar todos los agentes
        myAgent.enabled = false;

        isAgentActive = false; // Restablecer estado de activación
    }
}
