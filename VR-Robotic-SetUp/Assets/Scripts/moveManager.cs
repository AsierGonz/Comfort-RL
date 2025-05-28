using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveManager : MonoBehaviour
{
    public Transform objetoAMover;


    [SerializeField]
    private GameObject spawnPersona;

    public float radio = 0.50f;
    private float totalAngle = (2 * Mathf.PI) / 12; // 1/12 de un círculo
    private int totalSteps = 500;
    private int currentStep;
    private float anglePerStep;
    private float radioMove;
    private float initialAngle; // Ángulo inicial basado en la posición de spawn



    // for the keyboard movement 

    public float speed = 1f;
    public float rotationSpeed = 50.0f;


    //PARA LA QUE LA PERSONA SE MUEVA SOLA
    public float minDistanceFromLast = 0.0f;
    public float teleportDistance = 0.015f;
    private Vector3 lastPosition;

    private int numHijosbis;

    private int[] spawnHuman;

    private int valorAleatorio; 

    MyParams myParams;

    void Start()
    {
        Random.InitState(42);

        lastPosition = transform.position; // Inicializa la última posición

        myParams = Singleton.Instance.myObject;

        teleportDistance = myParams.humanInteraction.teleportationDistance;

        radio = myParams.humanInteraction.humanMovementMax;

        numHijosbis = myParams.humanInteraction.spawnHuman.Length;

        spawnHuman = myParams.humanInteraction.spawnHuman;

        valorAleatorio = spawnHuman[0]; 
    }


    private void Update()
    {
        KeyboardManagement(); 
    }


    private void KeyboardManagement()
    {
        // Movimiento hacia adelante y hacia atrás
        if (Input.GetKey(KeyCode.W))
        {
            MoveDirectionPerson(true, 0.0003f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveDirectionPerson(false, 0.0003f);
        }

        // Rotación
        if (Input.GetKey(KeyCode.A))
        {
            RotatePerson(true, 0.1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotatePerson(false, 0.1f);
        }

    }


    public void MoveRandom()
    {
        Vector3 randomDirection;
        Vector3 targetPosition;
        do
        {
            // Calcula una dirección aleatoria
            randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0; // Movimiento solo en el plano horizontal

            // Calcula el nuevo punto objetivo
            targetPosition = transform.position + randomDirection * teleportDistance;
        }
        while (Vector3.Distance(targetPosition, lastPosition) < minDistanceFromLast);

        // Decide aleatoriamente si llama a MoveDirectionPerson
        if (Random.Range(0, 50) < 1) // Hay un 2% de probabilidad de que esto ocurra
        {
            MoveDirectionPerson(true, 0.008f);
        }
        else
        {
            // Teletransporta el objeto a la nueva posición
            transform.position = targetPosition;

            // Actualiza la última posición
            lastPosition = transform.position;
        }

        // Hace que el objeto mire hacia el punto (0, 0, 0)
        transform.LookAt(Vector3.zero);
    }

    void MoveRandom2()
    {
        // Calcula una dirección aleatoria
        Vector3 randomDirection = Random.insideUnitSphere;

        // Asegúrate de que el movimiento sea solo en el plano horizontal (x, z)
        randomDirection.y = 0;

        // Calcula el nuevo punto objetivo
        Vector3 targetPosition = transform.position + randomDirection * 0.005f;

        // Teletransporta el objeto a la nueva posición
        transform.position = targetPosition;
    }

    void RandomMoveAndRotate()
    {
        float actionProbability = Random.Range(0f, 1f);
        float teleportDistance = 0.002f; // Ajusta según sea necesario
        float rotationAmount = 0.8f; // Ajusta según sea necesario, por ejemplo, 45 grados

        if (actionProbability < 1f / 6f) // 16.67% de probabilidad de moverse hacia adelante
        {
            MoveDirectionPerson(true, teleportDistance);
        }
        else if (actionProbability < 2f / 6f) // 16.67% de probabilidad de moverse hacia atrás
        {
            MoveDirectionPerson( false, teleportDistance);
        }
        else if (actionProbability < 3f / 6f) // 16.67% de probabilidad de girar a la izquierda
        {
            RotatePerson(true, rotationAmount);
            MoveDirectionPerson(true, teleportDistance);
        }
        else if (actionProbability < 4f / 6f) // 16.67% de probabilidad de girar a la derecha
        {
            RotatePerson(false, rotationAmount);
            MoveDirectionPerson(true, teleportDistance);
        }
        else if (actionProbability < 5f / 6f) // 16.67% de probabilidad de quedarse quieto
        {
            RotatePerson(true, rotationAmount);
        }
        // Si no, 16.67% de probabilidad de no girar (mantener la misma dirección)
    }


    public void MoverHaciaEsferaAleatoria()
    {
        if (spawnPersona == null)
        {
            Debug.LogError("El GameObject spawnPersona es nulo.");
            return;
        }

        int numHijos = spawnPersona.transform.childCount;

        if (numHijos > 0)
        {

            int indiceAleatorio = Random.Range(0, numHijosbis);

            valorAleatorio = spawnHuman[indiceAleatorio];

            Transform esferaAleatoria = spawnPersona.transform.GetChild(valorAleatorio);

            Vector3 positionNew = new Vector3(esferaAleatoria.position.x, objetoAMover.position.y, esferaAleatoria.position.z);
            objetoAMover.position = positionNew;

            Vector3 lookAtPoint = new Vector3(0, objetoAMover.position.y, 0);
            objetoAMover.LookAt(lookAtPoint);
        }

        ResetMovement();
    }
    public void ResetMovement()
    {
        currentStep = 0;
        radioMove = Vector3.Distance(new Vector3(0, objetoAMover.position.y, 0), objetoAMover.position);
        initialAngle = Mathf.Atan2(objetoAMover.position.z, objetoAMover.position.x);
        if (initialAngle < 0) initialAngle += 2 * Mathf.PI;

        anglePerStep = totalAngle / totalSteps;
    }

    public void MovePersonInRadius()
    {
        if (currentStep < totalSteps)
        {
            //float currentAngle = currentStep * anglePerStep; PARA AUMENTAR EN CADA PASO 
            // Iniciar desde el ángulo total y disminuir en cada paso
            // Calcular el ángulo actual considerando el ángulo inicial
            float currentAngle = initialAngle - (currentStep * anglePerStep);


            float radioMove = CalculateDistanceToOrigin();  // ELIMINAR ESTA LINEA SI SE QUIERE TODO NORMAL
            float x = Mathf.Cos(currentAngle) * radioMove;
            float z = Mathf.Sin(currentAngle) * radioMove;
            objetoAMover.position = new Vector3(x, objetoAMover.position.y, z);
            objetoAMover.LookAt(new Vector3(0, objetoAMover.position.y, 0));
            currentStep++;
        }
    }




    public void MoverObjetoAlCirculo()
    {

        float anguloAleatorio = Random.Range(0f, 2f * Mathf.PI);
        float posX = radio * Mathf.Cos(anguloAleatorio);
        float posZ = radio * Mathf.Sin(anguloAleatorio);
        Vector3 nuevaPosicion = new Vector3(posX, objetoAMover.position.y, posZ);
        objetoAMover.position = nuevaPosicion;
        Vector3 lookAtPoint = new Vector3(0, objetoAMover.position.y, 0);
        objetoAMover.LookAt(lookAtPoint);

        ResetMovement();
    }



    // Función para mover el personaje
    void MoveDirectionPersonKey(bool movingForward , float speed)
    {
        if (movingForward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    // Función para rotar el personaje
    void RotatePersonKey(bool rotateLeft, float rotSpeed)
    {
        if (rotateLeft)
        {
            transform.Rotate(Vector3.up, -rotSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        }
    }

    void MoveDirectionPerson(bool movingForward, float teleportDistance)
    {
        Vector3 direction = movingForward ? transform.forward : -transform.forward;
        Vector3 movement = direction * teleportDistance;
        transform.position += movement;
    }


    // Función para rotar el personaje
    void RotatePerson(bool rotateLeft, float rotationAmount)
    {
        float rotationDirection = rotateLeft ? -1 : 1;
        transform.Rotate(Vector3.up, rotationAmount * rotationDirection);
    }


    float CalculateDistanceToOrigin()
    {
        // La posición actual del objeto
        Vector3 currentPosition = transform.position;

        // La posición del origen (0,0,0)
        Vector3 origin = Vector3.zero;

        // Calcula y devuelve la distancia
        return Vector3.Distance(currentPosition, origin);
    }


}
