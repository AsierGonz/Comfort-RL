using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;


public class ControladorCustom : MonoBehaviour
{

    [SerializeField] public ArticulationBody joint1;
    [SerializeField] public ArticulationBody joint2;
    [SerializeField] public ArticulationBody joint3;
    [SerializeField] public ArticulationBody joint4;
    [SerializeField] public ArticulationBody joint5;
    [SerializeField] public ArticulationBody joint6;

    [SerializeField] public ArticulationBody pinzaFinal;
    [SerializeField] public GameObject pinzaFinalBola;
    public float angleToAdd = 1.0f;
    // Start is called before the first frame update
    public float targetAngle = 190f;  // In degrees, for instance.




    List<List<float>> jointPositions = new List<List<float>>()
{
        new List<float> {0.00f,-1.62f,-1.53f,-0.60f,1.39f,-0.30f},
        new List<float> { 0.48f, -2.78f, 0.12f, 0.97f, -0.18f, -0.25f },
        new List<float> { 0.41f, -2.81f, 0.91f, 1.33f, -0.48f, 0.35f },
        new List<float> { -0.03f, -1.61f, -1.58f, -0.55f, 1.41f, -0.24f },
        new List<float> { -1.35f, -2.54f, -0.38f, -0.66f, 1.32f, 0.06f },
        new List<float> { 0.00f, -1.59f, -1.56f, -0.57f, 1.41f, -0.28f },
        new List<float> { 0.49f, -2.78f, 0.08f, 1.00f, -0.17f, -0.26f },
        new List<float> { -3.47f, -1.55f, 1.17f, 0.56f, 1.71f, 1.57f },
        new List<float> { 0.51f, -2.64f, -0.13f, 0.83f, -0.31f, -0.40f },
        new List<float> { 0.00f, -1.64f, -1.52f, -0.62f, 1.38f, -0.33f },
        new List<float> { -1.37f, -2.52f, -0.39f, -0.67f, 1.39f, 0.10f },
        new List<float> { 0.02f, -1.63f, -1.43f, -0.80f, 1.32f, -0.50f },
        new List<float> { -1.20f, -2.48f, -0.34f, -0.80f, 0.94f, 0.86f },
        new List<float> { -0.02f, -1.59f, -1.57f, -0.53f, 1.43f, -0.22f },
        new List<float> { -1.23f, -2.57f, -0.20f, -0.88f, 1.07f, 0.20f },
        new List<float> { -1.31f, -2.56f, -0.28f, -0.75f, 1.24f, 0.05f },
        new List<float> { -1.23f, -2.50f, -0.37f, -0.77f, 1.05f, 0.79f },
        new List<float> { -1.25f, -2.55f, -0.24f, -0.85f, 1.10f, -0.58f },
        new List<float> { 0.51f, -2.65f, -0.10f, 0.82f, -0.32f, -0.42f },
        new List<float> { -1.33f, -2.55f, -0.27f, -0.79f, 1.23f, -0.47f },
        new List<float> { 0.50f, -2.68f, -0.07f, 0.80f, -0.27f, -0.30f },
        new List<float> { 0.65f, -0.87f, 0.63f, -0.99f, -0.17f, -0.91f },
        new List<float> { -1.26f, -2.54f, -0.17f, -0.93f, 1.12f, 0.88f },
        new List<float> { 0.66f, -0.87f, 0.69f, -1.15f, -0.12f, -1.21f },
        new List<float> { 0.02f, -1.61f, -1.51f, -0.65f, 1.34f, -0.46f },
        new List<float> { -1.28f, -2.53f, -0.33f, -0.72f, 1.10f, 0.73f },
        new List<float> { -3.45f, -1.55f, 1.19f, 0.61f, 1.76f, 1.53f },
        new List<float> { 0.63f, -0.85f, 0.80f, -1.27f, -0.19f, -1.13f },
        new List<float> { -1.30f, -2.56f, -0.24f, -0.83f, 1.17f, -0.50f },
        new List<float> { -1.41f, -2.52f, -0.21f, -0.88f, 1.34f, 1.14f },
        new List<float> { -1.32f, -2.53f, -0.34f, -0.75f, 1.26f, 0.19f },
        new List<float> { 0.50f, -2.70f, -0.06f, 0.81f, -0.25f, -0.32f },
        new List<float> { 0.48f, -2.78f, 0.08f, 1.04f, -0.17f, -0.26f },
        new List<float> { -1.29f, -2.58f, -0.21f, -0.84f, 1.14f, 0.12f },
        new List<float> { -1.33f, -2.52f, -0.33f, -0.74f, 1.25f, -0.48f },
        new List<float> { -0.02f, -1.62f, -1.46f, -0.76f, 1.38f, -0.56f },
        new List<float> { 0.50f, -2.83f, 0.15f, 0.98f, -0.15f, -0.30f },
        new List<float> { -3.50f, -1.55f, 1.15f, 0.59f, 1.70f, 1.67f },
        new List<float> { -1.22f, -2.50f, -0.34f, -0.77f, 0.99f, 0.81f },
        new List<float> { -1.33f, -2.48f, -0.39f, -0.73f, 1.23f, 0.85f },
        new List<float> { -1.31f, -2.55f, -0.26f, -0.77f, 1.23f, 0.12f },
        new List<float> { -3.50f, -1.54f, 1.13f, 0.64f, 1.70f, 1.78f },
        new List<float> { -0.01f, -1.64f, -1.42f, -0.79f, 1.34f, -0.59f },
        new List<float> { -1.20f, -2.54f, -0.18f, -0.99f, 0.99f, 0.77f },
        new List<float> { -0.04f, -1.61f, -1.46f, -0.77f, 1.40f, -0.53f },
        new List<float> { -1.26f, -2.55f, -0.18f, -0.93f, 1.10f, 0.87f },
        new List<float> { 0.49f, -2.77f, 0.17f, 0.99f, -0.20f, -0.24f },
        new List<float> { -3.47f, -1.55f, 1.17f, 0.62f, 1.74f, 1.62f },
        new List<float> { -1.34f, -2.52f, -0.35f, -0.74f, 1.26f, 0.78f },
        new List<float> { 0.40f, -2.82f, 0.92f, 1.33f, -0.49f, 0.41f },
        new List<float> { 0.65f, -0.86f, 0.73f, -1.21f, -0.13f, -1.19f },
        new List<float> { -1.36f, -2.54f, -0.38f, -0.66f, 1.34f, 0.05f },
        new List<float> { -1.30f, -2.60f, -0.19f, -0.84f, 1.17f, -0.58f },
        new List<float> { -1.29f, -2.50f, -0.36f, -0.75f, 1.12f, 0.78f },
        new List<float> { 0.50f, -2.64f, -0.10f, 0.82f, -0.32f, -0.40f },
        new List<float> { 0.66f, -0.85f, 0.69f, -1.14f, -0.11f, -1.09f },
        new List<float> { -1.21f, -2.46f, -0.41f, -0.78f, 0.94f, 0.98f },
        new List<float> { 0.49f, -2.77f, 0.09f, 0.99f, -0.18f, -0.26f },
        new List<float> { -1.27f, -2.54f, -0.18f, -0.92f, 1.13f, 0.85f },
        new List<float> { -1.28f, -2.56f, -0.17f, -0.93f, 1.16f, 0.90f },
        new List<float> { 0.50f, -2.78f, 0.04f, 1.01f, -0.17f, -0.30f },
        new List<float> { -1.25f, -2.57f, -0.20f, -0.86f, 1.09f, 0.15f },
        new List<float> { -1.20f, -2.55f, -0.18f, -0.95f, 1.00f, 0.82f },
        new List<float> { 0.65f, -0.88f, 0.77f, -1.25f, -0.14f, -1.15f },
        new List<float> { -1.23f, -2.50f, -0.36f, -0.76f, 1.06f, 0.76f },
        new List<float> { -1.30f, -2.59f, -0.23f, -0.82f, 1.14f, -0.48f },
        new List<float> { -3.50f, -1.56f, 1.19f, 0.57f, 1.72f, 1.51f },
        new List<float> { -1.32f, -2.55f, -0.27f, -0.79f, 1.10f, -0.56f } 
    }; //POSICIONES DE REINICIO PARA LA TERCERA FASE CON MOVIMIENTO DE PERSONA 


    List<List<float>> jointPositionFase2_Person = new List<List<float>>();  //POSICIONES DE REINICIO PARA LA SEGUNDA FASE CON MOVIMIENTO DE PERSONA 

    List<List<float>> jointPositionFase2_NoPerson = new List<List<float>>() {

        new List<float> {1.46f,-2.78f,-0.27f,-0.29f,2.37f,0.01f},
        new List<float> { 1.37f, -2.94f, 0.02f, -0.23f, 2.66f, 0.17f },
        new List<float> { 1.47f, -2.79f, -0.26f, -0.22f, 2.36f, -0.06f },
        new List<float> { 1.46f, -2.78f, -0.25f, -0.30f, 2.37f, -0.07f },
        new List<float> { 1.47f, -2.77f, -0.25f, -0.34f, 2.35f, -0.01f },
        new List<float> { 1.39f, -2.94f, 0.03f, -0.26f, 2.65f, 0.16f },
        new List<float> { 1.49f, -2.80f, -0.26f, -0.24f, 2.32f, -0.17f },
        new List<float> { 1.38f, -2.93f, 0.02f, -0.26f, 2.67f, 0.13f },
        new List<float> { 1.37f, -2.94f, 0.04f, -0.27f, 2.66f, 0.15f },
        new List<float> { 1.38f, -2.95f, 0.05f, -0.26f, 2.64f, 0.16f },
        new List<float> { 1.38f, -2.95f, 0.03f, -0.23f, 2.68f, 0.10f },
        new List<float> { 1.38f, -2.94f, 0.02f, -0.23f, 2.67f, 0.16f }

    };  //POSICIONES DE REINICIO PARA LA SEGUNDA FASE SIN SIN SIN MOVIMIENTO DE PERSONA 

    List<float> jointPositionFase3_NoPerson_pink = new List<float>() { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}; //TO RESET AFTER TAKING THE PINK BALL 
    List<float> jointPositionFase3_NoPerson_orange = new List<float>() { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}; // TO RESET AFTER TAKING ORANGE BALL  

    //private void Update()
    //{
    //Debug.Log(pinzaFinalBola.transform.position);
    //}

    private void Start()
    {
        ResetCobot(1); 
    }

    public void UpdateJointPosition(ArticulationBody joint, float minAngleRad = (-2 * Mathf.PI), float maxAngleRad = (2 * Mathf.PI), float angleStep = 1.0f, bool direction = true)
    {
        float currentPosition = joint.jointPosition[0];

        angleStep = angleStep * Mathf.Deg2Rad; //por si lo paso en grados

        if (direction)
        {
            // Comprueba si la posición actual más el ángulo a agregar es menor o igual a 2 * pi radianes
            if (currentPosition + angleStep <= maxAngleRad)
            {
                currentPosition += angleStep;
            }
        }

        if (!direction)
        {
            // Comprueba si la posición actual más el ángulo a agregar es mayor o igual a 0 radianes
            if (currentPosition - angleStep >= minAngleRad)
            {
                currentPosition -= angleStep;
            }
        }

        // Crea una nueva ArticulationReducedSpace con la posición actualizada y actualiza la articulación
        joint.jointPosition = new ArticulationReducedSpace(currentPosition);

        //Debug.Log(currentPosition);
    }

    private void ResetCobotJoint(ArticulationBody joint, float resetAngle = 0.0f)
    {

        resetAngle = resetAngle * Mathf.Deg2Rad;
        joint.jointPosition = new ArticulationReducedSpace(resetAngle);

    }
    private void ResetCobotJointRad(ArticulationBody joint, float resetAngle = 0.0f)
    {
        // Se asume que resetAngle ya está en radianes
        joint.jointPosition = new ArticulationReducedSpace(resetAngle);
    }



    public void ResetCobot(int fase)
    {
        if(fase == 1) {  //EMPEZANDO EN EL INICIO 
            ResetCobotJoint(joint1, 0.0f);
            ResetCobotJoint(joint2, -90.0f);
            ResetCobotJoint(joint3, 0.0f);
            ResetCobotJoint(joint4, 0.0f);
            ResetCobotJoint(joint5, 90.0f);
            ResetCobotJoint(joint6, 0.0f);
            //Debug.Log(pinzaFinalBola.transform.position); 
        }
        if (fase == 2) //EMPEZANDO UNA VEZ QUE COJA LA PELOTA // ARRAY DE POSICIONES 
        {
            int index = Random.Range(0, jointPositionFase2_NoPerson.Count);
            List<float> selectedJointValues = jointPositionFase2_NoPerson[index];

            ResetCobotJointRad(joint1, selectedJointValues[0]);
            ResetCobotJointRad(joint2, selectedJointValues[1]);
            ResetCobotJointRad(joint3, selectedJointValues[2]);
            ResetCobotJointRad(joint4, selectedJointValues[3]);
            ResetCobotJointRad(joint5, selectedJointValues[4]);
            ResetCobotJointRad(joint6, selectedJointValues[5]);
        }
        if (fase == 3) //EMPEZANDO UNA VEZ QUE COJA LA PELOTA // ARRAY DE POSICIONES 
        {
            
            int index = Random.Range(0, jointPositions.Count);
            List<float> selectedJointValues = jointPositions[index];

            ResetCobotJointRad(joint1, selectedJointValues[0]);
            ResetCobotJointRad(joint2, selectedJointValues[1]);
            ResetCobotJointRad(joint3, selectedJointValues[2]);
            ResetCobotJointRad(joint4, selectedJointValues[3]);
            ResetCobotJointRad(joint5, selectedJointValues[4]);
            ResetCobotJointRad(joint6, selectedJointValues[5]);
        }

    }

    public List<float> CalculateAllJointAngleDifferences()
    {
        // Lista para almacenar las diferencias de ángulo de cada articulación
        ArticulationBody[] joints = { joint1, joint2, joint3, joint4, joint5, joint6 };
        List<float> angleDifferences = new List<float>();

        // Ángulos de reseteo por defecto para cada articulación
        float[] defaultResetAngles = new float[] { 0.0f, -90.0f, 0.0f, 0.0f, 90.0f, 0.0f };

        for (int i = 0; i < joints.Length; i++)
        {
            // Convertir el ángulo de reseteo a radianes
            float resetAngleRadians = defaultResetAngles[i] * Mathf.Deg2Rad;

            // Obtener la posición actual de la articulación en radianes
            float currentAngleRadians = joints[i].jointPosition[0];

            // Calcular la diferencia en radianes
            float differenceRadians = Mathf.Abs(currentAngleRadians - resetAngleRadians);

            // Convertir la diferencia de radianes a grados y agregarla a la lista
            angleDifferences.Add(differenceRadians * Mathf.Rad2Deg);
        }

        return angleDifferences;
    }

    public bool JointsInPosition(List<float> angleDifferences)
    {
        ArticulationBody[] joints = { joint1, joint2, joint3, joint4, joint5, joint6 };
        //List<float> angleDifferences = CalculateAllJointAngleDifferences(joints);

        // Tolerancia en grados
        const float toleranceDegrees = 10.0f;

        foreach (float angleDifference in angleDifferences)
        {
            // Si alguna articulación está fuera de la tolerancia, devuelve false
            if (angleDifference > toleranceDegrees)
            {
                return false;
            }
        }
        // Todas las articulaciones están dentro de la tolerancia, devuelve true
        return true;
    }


    public void SaveJointPositions()
    {
        string path = "Assets/Positions/trayectory1.txt"; // Cambia esto por la ruta donde quieres guardar el archivo

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            string line = "new List<float> {" + joint1.jointPosition[0].ToString("0.00f", CultureInfo.InvariantCulture) + "," + 
                          joint2.jointPosition[0].ToString("0.00f", CultureInfo.InvariantCulture) + "," +
                          joint3.jointPosition[0].ToString("0.00f", CultureInfo.InvariantCulture) + "," +
                          joint4.jointPosition[0].ToString("0.00f", CultureInfo.InvariantCulture) + "," +
                          joint5.jointPosition[0].ToString("0.00f", CultureInfo.InvariantCulture) + "," +
                          joint6.jointPosition[0].ToString("0.00f", CultureInfo.InvariantCulture) + "},"; 

            writer.WriteLine(line);
        }
    }

    public void SaveJointPositions_deg(bool final, string pathReal)
    {
        string path = "Assets/Positions/trayectory1.txt"; // Cambia esto por la ruta donde quieres guardar el archivo

        path = pathReal;  // Para hacerlo por el JSON

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            if (final)
            {
                // Si el parámetro final es true, escribe "END OF TRAYECTORY"
                writer.WriteLine("END OF TRAYECTORY");
            }
            else
            {
                // Convertir las posiciones de las articulaciones de radianes a grados
                float joint1Degrees = joint1.jointPosition[0] * Mathf.Rad2Deg;
                float joint2Degrees = joint2.jointPosition[0] * Mathf.Rad2Deg;
                float joint3Degrees = joint3.jointPosition[0] * Mathf.Rad2Deg;
                float joint4Degrees = joint4.jointPosition[0] * Mathf.Rad2Deg;
                float joint5Degrees = joint5.jointPosition[0] * Mathf.Rad2Deg;
                float joint6Degrees = joint6.jointPosition[0] * Mathf.Rad2Deg;

                // Crear una línea con los valores separados por comas
                string line = joint1Degrees.ToString("0.00", CultureInfo.InvariantCulture) + "," +
                              joint2Degrees.ToString("0.00", CultureInfo.InvariantCulture) + "," +
                              joint3Degrees.ToString("0.00", CultureInfo.InvariantCulture) + "," +
                              joint4Degrees.ToString("0.00", CultureInfo.InvariantCulture) + "," +
                              joint5Degrees.ToString("0.00", CultureInfo.InvariantCulture) + "," +
                              joint6Degrees.ToString("0.00", CultureInfo.InvariantCulture);

                // Escribir la línea en el archivo
                writer.WriteLine(line);
            }
        }
    }

    public string CheckAnglePosition()
    {
        float joint1Degrees = joint1.jointPosition[0] * Mathf.Rad2Deg;
        float joint2Degrees = joint2.jointPosition[0] * Mathf.Rad2Deg;
        float joint3Degrees = joint3.jointPosition[0] * Mathf.Rad2Deg;
        float joint4Degrees = joint4.jointPosition[0] * Mathf.Rad2Deg;
        float joint5Degrees = joint5.jointPosition[0] * Mathf.Rad2Deg;
        float joint6Degrees = joint6.jointPosition[0] * Mathf.Rad2Deg;

        return "pizaBola " + joint1Degrees + "|" + joint2Degrees + "|" + joint3Degrees + "|" + joint4Degrees + "|" + joint5Degrees + "|" + joint6Degrees + "";


    }


}
