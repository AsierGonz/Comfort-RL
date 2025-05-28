using System.Collections;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    private Light myLight; // Variable para almacenar el componente de luz

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Light del objeto al que está adjunto este script
        myLight = GetComponent<Light>();
    }

    // Función pública para iniciar el parpadeo de la luz
    public void StartBlinking()
    {
        // Inicia la corutina
        StartCoroutine(BlinkLight());
    }

    // Corutina que maneja el encendido y apagado de la luz
    IEnumerator BlinkLight()
    {
        // Repetir el proceso 5 veces
        for (int i = 0; i < 5; i++)
        {
            // Encender la luz
            myLight.enabled = true;
            // Esperar 2 segundos
            yield return new WaitForSeconds(2);
            // Apagar la luz
            myLight.enabled = false;
            // Esperar 2 segundos antes de continuar el ciclo
            yield return new WaitForSeconds(2);
        }
        // Asegurar que la luz quede desactivada al finalizar
        myLight.enabled = false;
    }
}
