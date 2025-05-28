using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonPressDetector : MonoBehaviour
{
    [SerializeField]
    private InputAction activateAction; // Aseg�rate de referenciar tu InputAction aqu� en el Inspector

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
            // L�gica para activar el modelo de robot con ML-Agents
            StartAgent();
        }
    }

    void StartAgent()
    {

        Debug.Log("PULSADO"); 
        // Aqu� podr�as habilitar el agente, iniciar una evaluaci�n, etc.
    }
}
