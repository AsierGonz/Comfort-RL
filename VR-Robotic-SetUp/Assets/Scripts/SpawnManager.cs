using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject posicionesContainer; //GameObject que contiene las posiciones
    [SerializeField] private GameObject Goal; //GameObject que contiene las posiciones
    [SerializeField] private GameObject robot; //GameObject que contiene las posiciones
    //[SerializeField] private GameObject Goal2; //GameObject que contiene las posiciones
    [SerializeField] private GameObject sphereVisual;


    [SerializeField] private SocketDetector socketDetector;




    [SerializeField] private GameObject primerPuntoSpawn;  // PUNTO PARA SABER A DONDE TIENE QUE COGER EL PUNTO 
    [SerializeField] private GameObject segunndoPuntoSpawnPosible1;  // PUNTO PARA SABER A DONDE TIENE QUE COGER EL PUNTO 
    [SerializeField] private GameObject segunndoPuntoSpawnPosible2;  // PUNTO PARA SABER A DONDE TIENE QUE COGER EL PUNTO 

    //[SerializeField] private GameObject primerPuntoSpawn;  // PUNTO PARA SABER A DONDE TIENE QUE COGER EL PUNTO 


    [SerializeField]
    private Material material1; //ORANGE

    [SerializeField]
    private Material material2; //PINK

    private List<Transform> posiciones = new List<Transform>();

    public float minY = 0.20f;
    public float maxY = 0.60f;
    public float minX = -0.50f;
    public float maxX = 0.50f;
    public float minZ = -0.50f;
    public float maxZ = 0.50f;
    public float cylinderRadius = 1f;
    public float cylinderCenterY = 0.7f; // Altura del centro del cilindro en el eje Y
    public float cylinderHeight = 0.5f;

    private int setColorBall;

    [SerializeField]
    private GameObject spawnPointTemplate;

    private void Start()
    {

        Random.InitState(42);

        MyParams myParams = Singleton.Instance.myObject;
        setColorBall = myParams.environment.colorBall;

        if (posicionesContainer != null)
        {
            // Todas las posiciones dentro del contenedor
            foreach (Transform child in posicionesContainer.transform)
            {
                posiciones.Add(child);
            }
        }
        else
        {
            Debug.LogError("El GameObject de posiciones no está asignado en el Inspector.");
        }
    }

    // Función para mover aleatoriamente a una posición
    public void MoveRandom()
    {
        if (posiciones.Count == 0)
        {
            return;
        }

        // Selecciona una posición aleatoria
        int indiceAleatorio = Random.Range(0, posiciones.Count);
        Vector3 destino = posiciones[indiceAleatorio].position;

        transform.position = destino; 
        // Mueve el objeto hacia la posición aleatoria
        
    }

    public void MovePosition(Vector3 positionValid)
    {
        transform.position = positionValid; 
    }

    public void GenerateRandomPoint()
    {
        Vector3 randomPoint;

        float x;
        float y;
        float z;
        do
        {
            // Genera ángulos aleatorios en coordenadas esféricas
            float theta = Random.Range(0f, Mathf.PI);
            float phi = Random.Range(0f, 2f * Mathf.PI);
            float r = Random.Range(0f, 0.55f); // Radio de la esfera

            // Convierte coordenadas esféricas a cartesianas
            x = r * Mathf.Sin(theta) * Mathf.Cos(phi);
            y = r * Mathf.Cos(theta);
            z = r * Mathf.Sin(theta) * Mathf.Sin(phi);

            randomPoint = new Vector3(x, y, z);
        } while ( y <= 0.2f || IsInsideCylinder(randomPoint, robot.transform.position, 0.4f));


        CalcDistancia(randomPoint, robot.transform.position); 
        Goal.transform.position = randomPoint;
    }



    bool IsInsideCylinder(Vector3 point, Vector3 pointRobot, float radius)
    {
        float dx = point.x - pointRobot.x;
        float dz = point.z - pointRobot.z;
        float distance = Mathf.Sqrt(dx * dx + dz * dz);
        return distance < radius;
    }


    public void CalcDistancia(Vector3 point, Vector3 pointRobot)
    {

        float dx = point.x - pointRobot.x;
        float dz = point.z - pointRobot.z;
        float distance = Mathf.Sqrt(dx * dx + dz * dz);
        Debug.Log(distance);


    }



    bool IsInsideCylinderOLD(Vector3 point, float radius, float centerY, float height)
    {
        float distanceToAxis = Mathf.Sqrt(point.x * point.x + point.z * point.z);
        return (distanceToAxis <= radius) && (point.y >= centerY - height / 2 && point.y <= centerY + height / 2);
    }



    public void GenerateRandomPointOLD()
    {
        Vector3 randomPoint;

        do
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);

            randomPoint = new Vector3(x, y, z);
        } while (IsInsideCylinderOLD(randomPoint, cylinderRadius, cylinderCenterY, cylinderHeight));


        Goal.transform.position = randomPoint;

        //Debug.Log("Punto generado"); 
        //Debug.Log(randomPoint);
    }


    public int ChangeMaterial( bool checkForVR )
    {


        if (checkForVR == false) {
            // Generar un número aleatorio entre 0 y 1

            return ChangeMaterialHelper(); 
            
        }
        else
        {

            Debug.Log(socketDetector.checkColor);

            if (socketDetector.checkColor == 2) //no hay nada 
            {

                return ChangeMaterialHelper();

            }
            else
            {
                int color = socketDetector.checkColor;
                Renderer rend = sphereVisual.GetComponent<Renderer>();
                if (color == 0)
                {
                    rend.material = material1; //ORANGE
                }
                else
                {
                    rend.material = material2; //PINK
                }
                socketDetector.ReturnBack();
                Debug.Log("COLOR SET BASED ON SOCKET"); 
                return color; 
            }
        }

        
    }

    private int ChangeMaterialHelper()  //PODEMOS AQUI CAMBIAR PARA VER EL COLOR DE RANDOM VALUE 
    {

        int randomValue; 


        if (setColorBall == -1)
        {
            randomValue  = Random.Range(0, 2);
        }
        else if(setColorBall == 1)
        {
            randomValue = 1;

        }
        else
        {
            randomValue = 0;

        }
      
        // Obtener el componente Renderer del objeto
        Renderer rend = sphereVisual.GetComponent<Renderer>();

        // Cambiar el material según el valor aleatorio
        if (randomValue == 0)
        {
            rend.material = material1; //ORANGE
        }
        else
        {
            rend.material = material2; //PINK
        }

        return randomValue;
    }


    public GameObject returnFirstPoint()  //devuelve el spawn del primer punto, a donde tiene que ir a buscar la pelota 
    {

        return primerPuntoSpawn; 
    }

    public GameObject returnSecondPoint(int colorSpawnPrimero) //devuelve al punto al que tiene que ir 
    {

        if( colorSpawnPrimero == 0)
        {
            return segunndoPuntoSpawnPosible1;

        }
        else
        {

            return segunndoPuntoSpawnPosible2;
        }

        
    }
}







