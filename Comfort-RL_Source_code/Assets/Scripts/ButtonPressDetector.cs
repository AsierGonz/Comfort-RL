using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonPressDetector : MonoBehaviour
{
    [SerializeField]
    private InputAction activateAction; // Asegúrate de referenciar tu InputAction aquí en el Inspector

    private void OnEnable()
    {
        activateAction.Enable();
    }

    private void OnDisable()
    {
        activateAction.Disable();
    }

    private void Update()
    {
        if (activateAction.triggered)
        {
            // Lógica para activar el modelo de robot con ML-Agents
            StartAgent();
        }
    }

    void StartAgent()
    {

        Debug.Log("PULSADO"); 
        // Aquí podrías habilitar el agente, iniciar una evaluación, etc.
    }
}
