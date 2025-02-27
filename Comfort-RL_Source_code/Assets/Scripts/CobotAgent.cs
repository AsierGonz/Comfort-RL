using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.IO;

public class CobotAgent : Agent

{
    //WATCH OUT! LOOK FOR MORE RL REWARD BEHAVIOUR IN COLLISION SCRIPTS (GOAL AND NORMAL)
    [SerializeField] public ControladorCustom cobotControl;
    [SerializeField] private GameObject goalReachSphere;
    [SerializeField] public GameObject goalReachSphere2;
    [SerializeField] public GameObject person;
    [SerializeField] public GameObject robot;
    [SerializeField] public GameObject leaveOranje;
    [SerializeField] public GameObject leavePink;
    [SerializeField] public GameObject returnPositonTool;
    [SerializeField] public Transform givenPoint; // LO UTILIZAMOS PARA VER SI ESTA MAS ALEJADO DE UN PUNTO QUE SERIA UNA ESFERA (PARA EL CLIPING PARA COMPROBAR SI ESTAMOS MAS LEJOS!! ) 
    [SerializeField] public SpawnManager spawnManager;
    [SerializeField] public SpawnManager spawnManager2;
    [SerializeField] public moveManager humanMover;
    [SerializeField] private GameObject spawnPersona;
    [SerializeField] private CustomAgentControllerVR execControler; //CUIDADO AQUI QUITAR EL VR PARA EJECUCION NORMAL 
    [SerializeField] private CustomAgentControllerVR_key execControler_k; //CUIDADO AQUI QUITAR EL VR PARA EJECUCION NORMAL 

    [SerializeField] private Material orange;
    [SerializeField] private Material pink;



    [SerializeField] private MeshRenderer pinkBallLeaved; // PARA CUANDO SE DEJA EN LA MESA QUE SEA VISUAL
    [SerializeField] private MeshRenderer orangeBallLeaved;  // PARA CUANDO SE DEJA EN LA MESA QUE SEA VISUAL 

    public MeshRenderer meshRendererPinza; // Asigna el primer MeshRenderer en el Inspector
    public MeshRenderer meshRendererGoal;

    private float presitionGoal = 3.0f;
    public bool enEntranamiento = true;
    private bool randomStart = false;  //INICIA EL ROBOT EN UN PUNTO RANDOM
    //private bool activateCL = false; //YA NO SE UTILZAN. REVISAR SCRIPTS PARALELOS PARA BORRAR

    private bool activatePenalty = false;
    //private int goalReachedConsecutive = 0; //Se utilizo para los primeros entrenas de generalizacion 


    // OJO
    //SEGUNDA FASE!!!!!! - NEW TASK
    // OJO

    private bool inicioEnPrimeraFaseSolo = false;
    private bool inicioEnSegundaFaseSolo = false;

    private bool primerGoal = false; // IMPORTANTE PARA DIFERENCIAR LAS DOS FASES - PRIMERA Y SEGUNDA FASE. 
    private bool humanMove = true;  // Para activar el movimiento de la persona a un punto aleatorio !! NO ACTIVA EL MOVIMIENTO CONTINUO LINEAL  
    private int colorPelota = 0;
    private bool movimientoPersona = true; //ACTIVA EL MOVIMIENTO CONTINUO LINEAL  !!!! &&&&&&&&&& !!!!! &&&&&&&&&&& LO HE CAMBIADO, TENGO QUE HACERLO DE NUEVO ESTA EN BEGIN


    // OJO
    //TERCERA FASE!!!!!!
    // OJO

    private bool inicioEnTerceraFaseSolo = false; 
    
    private bool activarTerceraFase = false; 
    private bool enTerceraFase = false;  //NUNCA CAMBIAR ESTO A TRUE EN NINGUN MOMENTO!! 



    private bool clipPositionPerson = false;  //PARA GENERALIZACION DE LA POSICION DE LA PERSONA 
    public bool partialClipping = false;  //PARA GENERALIZACION DE LA POSICION DE LA PERSONA 



    public bool hacerSoloUnEntrena = true; // PARA REALIDAD VIRTUAL, PARA QUE HAGA UNA SOLA EJECUCION Y SE PARE.  
    // RECORDAR EL CAMBIO EN CUSTOM AGENT CONTROLLER SI SE ACTIVA EL SUPERIOR
    private bool finalizado = false;  //NO TOCAR 



    public bool checkVR_socket = false;  // TRAINING CHANGE TO FALSE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! OJO 


    // OJO
    // MODEL SWITCH !!!!!!
    // OJO

    private bool modelSwitch = true;
    [SerializeField] private ModelSwitch switcher;


    private float temperatura = 0.0f; // LO VAMOS A HACER SIEMPRE DE 0 A 1 -- TEMPERATURA DE LA APROXIMACION!! 

    private Vector3 positionOrigen1 = new Vector3(0.41f, 0.61f, -0.09f);
    private Vector3 positionOrigenUp1 = new Vector3(0.13f, 0.89f, -0.09f);

    [SerializeField] private GameObject positionOrigen;
    [SerializeField] private GameObject positionOrigenUp;
    private List<float> jointDiff;


    private List<float> distances = new List<float>();
    private bool calDis = false; //PARA ACTIVAR LA GRABACION DE RESULTADOS EN EL TXT (DE CARA A DISTANCIA MEDIA) 

    private float thresholdTemp = 0.5f;

    private float thresholdAngle; 
    private float thresholdTempHelper;

    private string rutaArchivoGuardado;

    private int alpha; 
    private int beta;
    private float numberOfComfortLevels = 10.0f;
    private float CurrentComfortLevel = 3.0f;
    private float distanceMaxThreshold = 1.5f;

    private int steps = 0; 

    private string pathRealRobot; 


    int numeroDeVecesGuardado;


    // Start is called before the first frame update
    void Start()
    {
        numeroDeVecesGuardado = 0;
        Random.InitState(42);

        MyParams myParams = Singleton.Instance.myObject;

        // Acceso a variables de la subclase Goals
        presitionGoal = myParams.goals.precisionGoal;

        // Acceso a variables de la subclase Penalties
        activatePenalty = myParams.penalties.activatePenalty;

        // Acceso a variables de la subclase Phases
        inicioEnPrimeraFaseSolo = myParams.phases.startInFirstPhaseOnly;
        inicioEnSegundaFaseSolo = myParams.phases.startInSecondPhaseOnly;
        inicioEnTerceraFaseSolo = myParams.phases.startInThirdPhaseOnly;
        activarTerceraFase = myParams.phases.activateThirdPhase;

        // Acceso a variables de la subclase HumanInteraction
        humanMove = myParams.humanInteraction.humanMove;
        movimientoPersona = myParams.humanInteraction.personMovement;

        // Acceso a variables de la subclase Environment
        clipPositionPerson = myParams.miscellaneous.clipPositionPerson;

        // Acceso a variables de la subclase Miscellaneous
        hacerSoloUnEntrena = myParams.miscellaneous.singleTrainingOnly;
        checkVR_socket = myParams.miscellaneous.checkVR_socket;
        rutaArchivoGuardado = myParams.environment.routeSaveFile;

        // Acceso a variables de la subclase ComfortParameters
        temperatura = myParams.comfortParameters.temperature;
        alpha = myParams.comfortParameters.alpha;
        beta = myParams.comfortParameters.beta;
        numberOfComfortLevels = myParams.comfortParameters.numberOfComfortLevels;
        CurrentComfortLevel = myParams.comfortParameters.currentComfortLevel;
        distanceMaxThreshold = myParams.comfortParameters.distanceMaxThreshold;
        pathRealRobot = myParams.miscellaneous.pathToSaveRealRobot;

        modelSwitch = myParams.miscellaneous.switcher;

    }


    // Update is called once per frame
    void Update()  // USADO PARA DEBUGGEAR SOLO, CONSOLES LOG. 
    {


        //Debug.Log(goalReachedConsecutive);
        //AddReward(-0.5f);
        //Debug.Log(presitionGoal); 

        Debug.Log("Threshold is" + thresholdTemp);
        Debug.Log("Distance Threshold is" + distanceMaxThreshold);

    }

    public override void OnEpisodeBegin()
        
    {

        if (calDis == true)
        {
            Debug.Log(GetAverageDistance() +":" + steps);
            //GetAverageDistance();

        }

        steps = 0; 

        ClearDistances();



        //thresholdTemp = GetRandomThreshold();
        thresholdTemp = calculateThresholdBasedOnComfortLevelAndDistance();

        thresholdAngle = calculateIncrementAnglePerStepBasedOnComfortLevel();



        Debug.Log("Threshold is" + thresholdTemp); 
        Debug.Log("Distance Threshold is" + distanceMaxThreshold);




        //Debug.Log(checkVR_socket);

        if (hacerSoloUnEntrena == true)
        {
            if(finalizado == false)
            {

                finalizado = true; 

            }
            else
            {
                enabled = false;
                finalizado = false;

                execControler.isAgentActive = false;
                execControler_k.isAgentActive = false;
                //cobotControl.ResetCobot(1);

            }
        }

        if(finalizado == true || hacerSoloUnEntrena == false) { 

            meshRendererGoal.enabled = false;
            meshRendererPinza.enabled = false;
            pinkBallLeaved.enabled = false;
            orangeBallLeaved.enabled = false;
        
        


            if (randomStart) //AQUI GENERA UNA PELOTA ALEATORIA EN TODO EL ESPACIO !!!! PARA LAS PRIMERAS ITERACIONES DEL ELEMENTO 
            {
                cobotControl.ResetCobot(1);
                primerGoal = false; /// CUIDADO AQUI CAMBIAR PARA COGER DOS PELOTAS!!!!!!!!!!!!!!!!!!!!!
                spawnManager.GenerateRandomPoint();
                colorPelota = spawnManager.ChangeMaterial(checkVR_socket);
                SetMaterialBasedOnColor(colorPelota);
                meshRendererGoal.enabled = true;
            }
            else  //NORMALLY THE ONE USED
            {

                if(inicioEnTerceraFaseSolo == false) { 

                    if(inicioEnSegundaFaseSolo == true) // INICIO EN SEGUNDA FASE SOLO 
                    {
                        meshRendererGoal.enabled = false;
                        meshRendererPinza.enabled = true;
                        primerGoal = true; // ACTIVA QUE EL PRIMER GOAL YA ESTE CONSEGUIDO CON LO QUE UNICAMENTE PASARIA A LA SEGUNDA FASE 
                        cobotControl.ResetCobot(2);
                        colorPelota = spawnManager.ChangeMaterial(checkVR_socket);
                        SetMaterialBasedOnColor(colorPelota);
                        meshRendererGoal.enabled = true;

                    }
                    else {  // INICIO NORMAL; CON EL RESET EN EL PUNTO NORMAL 
                        cobotControl.ResetCobot(1);
                        primerGoal = false;  /// CUIDADO AQUI CAMBIAR PARA COGER DOS PELOTAS!!!!!!!!!!!!!!!!! <----------------------------------------------------------------------------
                        spawnManager.MoveRandom();
                        colorPelota = spawnManager.ChangeMaterial(checkVR_socket);
                        SetMaterialBasedOnColor(colorPelota);
                        meshRendererGoal.enabled = true;

                    }
                }
                else // INICIO EN TERCERA FASE SOLO 
                {
                    cobotControl.ResetCobot(3);
                    meshRendererGoal.enabled = false;
                    meshRendererPinza.enabled = false;
                    if (colorPelota == 1) { pinkBallLeaved.enabled = true; }
                    if (colorPelota == 0) { orangeBallLeaved.enabled = true; }
                }
            }

            if (humanMove == true)
            {
                humanMover.MoverHaciaEsferaAleatoria(); //Para activar el movimiento de la persona a un punto aleatorio !! NO ACTIVA EL MOVIMIENTO CONTINUO LINEAL, habria que poner el new para activar que se pueda poner
                //movimientoPersona = true; 

                if (movimientoPersona == true) { 
                    int indiceAleatorio = Random.Range(0, 100);

                    if (indiceAleatorio < 10)  // 20% DE LAS VECES DE SE QEDA QUIETO
                    {
                        //movimientoPersona = false;
                    }
                    else
                    {
                        movimientoPersona = true; 
                    }
                }
            }

            if (activarTerceraFase == true)
            {

                if(inicioEnTerceraFaseSolo == false) { 
                    enTerceraFase = false;  // SI TENEMOS ACTIVADA QUE EL ENTRENAMIENTO LLEGE A UNA TERCERA FASE TENEMOS QUE HACER ESTO
                }
                else
                {
                    enTerceraFase = true; 
                }
            }

            if (modelSwitch)
            {

                switcher.SwitchModel(1); 
            }

        }

    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        // Move the agent using the action.
        //MoveAgent(actionBuffers.DiscreteActions);

        steps = steps + 1; 

        MoveAgentContinious(actionBuffers.ContinuousActions);


         
        cobotControl.SaveJointPositions_deg(false, pathRealRobot); ////////////// SAVING JOINT POSITION FOR REAL ROBOTS 



        //Debug.Log(GetCumulativeReward());

        if ( movimientoPersona == true)
        {
            humanMover.MoveRandom();
            humanMover.MovePersonInRadius(); 
        }


        
        if (activatePenalty)
        {
            AddReward(-0.1f); //PARA PENALIZAR POR STEP!! PENSADO EN CONSEGUIR COMPORTAMIENTOS OPTIMIZADOS
        }


    }


    public void MoveAgentContinious(ActionSegment<float> vectorAction)
    {


        float joint1 = Mathf.Clamp(vectorAction[0], -2f, 2f);
        float joint2 = Mathf.Clamp(vectorAction[1], -2f, 2f);
        float joint3 = Mathf.Clamp(vectorAction[2], -2f, 2f);
        float joint4 = Mathf.Clamp(vectorAction[3], -2f, 2f);
        float joint5 = Mathf.Clamp(vectorAction[4], -2f, 2f);
        float joint6 = Mathf.Clamp(vectorAction[5], -2f, 2f);





        cobotControl.UpdateJointPosition(cobotControl.joint1, angleStep: joint1);
        cobotControl.UpdateJointPosition(cobotControl.joint2, angleStep: joint2);
        cobotControl.UpdateJointPosition(cobotControl.joint3, angleStep: joint3);
        cobotControl.UpdateJointPosition(cobotControl.joint4, angleStep: joint4);
        cobotControl.UpdateJointPosition(cobotControl.joint5, angleStep: joint5);
        cobotControl.UpdateJointPosition(cobotControl.joint6, angleStep: joint6);



        if (enTerceraFase == false) { 

            RewardManagerLeaveBall();
        }
        else
        {
            RewardTerceraFase(); 
        }

        //float rewardLineal = -((1 / CalculateDistanceToPinzaBola()) / 20) * temperatura; 
        float rewardLog = -1 / Mathf.Log(CalculateDistanceToPinzaBola() + 1) * temperatura / 20; 
        //float rewardLog2 = Mathf.Log(CalculateDistanceToPinzaBola() + 1) * temperatura / 20; //Esto seria para darle en postivo, se puede probar
        //AddReward(rewardLog);

        //Debug.Log("lin" + rewardLineal);
        //Debug.Log("log" + rewardLog);
        //Debug.Log("log" + rewardLog2);


        float resultRewardDistance = GeometricDistanceReward(CalculateDistanceToPinzaBola());

        float resultRewardAnge = CalculateFloatRewardAngle(joint1, joint2, joint3, joint4, joint5, joint6);

        Debug.Log("reward Angle " + resultRewardAnge); 

        if (float.IsNaN(resultRewardDistance))
        {
            //Debug.Log("ES NAN Y LO EVITAMOS = -1");
            AddReward(-0.1f);
        }
        else
        {
            //Debug.Log(-0.1f + 0.1f * resultRewardDistance);
            AddReward(alpha / 10 * (-0.1f + 0.1f * resultRewardDistance));
        }

        if (float.IsNaN(resultRewardAnge))
        {
            //Debug.Log("ES NAN Y LO EVITAMOS = -1");
            AddReward(-0.1f);
        }
        else
        {
            //Debug.Log(-0.1f + 0.1f * resultRewardDistance);
            AddReward(beta * resultRewardAnge);
        }


        if (calDis) { 
            float distanciaActualStep = CalculateDistanceToPinzaBola();
            distances.Add(distanciaActualStep);
            //Debug.Log(distanciaActualStep); 
        }
    }
    public void RewardManager()
    {


        float distancia = CalculateDistance();
        float rewardLogNeg = -Mathf.Log(distancia + 1);  //PARA AÑADIR PENALTY POR DISTANCIA PEQUEÑA PARA QUE PUEDA APRENDER
        GiveReward(rewardLogNeg / 10);
        float distanciaAumentada = distancia * 10;

        if (distanciaAumentada < presitionGoal)
        {
            GoalReachedNewTask();
        }

    }

    public void RewardManagerLeaveBall()
    {
        if (primerGoal == false)
        {
            float distancia = CalculateDistanceGeneric(goalReachSphere2); 

            float rewardLogNeg = -Mathf.Log(distancia + 1);  //PARA AÑADIR PENALTY POR DISTANCIA PEQUEÑA PARA QUE PUEDA APRENDER
            float rewardLogPos = Mathf.Log(1 / (distancia + 0.1f));

            //GiveReward(rewardLogNeg / 10);
            //GiveReward(rewardLogPos);

            //Debug.Log(distancia);

            float distanciaAumentada = distancia * 10;

            if (distanciaAumentada < presitionGoal)
            {
                GoalReachedNewTask();
            }



        }
        else
        {


            if (colorPelota == 1) //PINK
            {
                float distancia = CalculateDistanceGeneric(leavePink);
                float distanciaAumentada = distancia * 10;
                if (distanciaAumentada < presitionGoal) { GoalReachedNewTask(); }
                float rewardLogNeg = -Mathf.Log(distancia + 1);  //PARA AÑADIR PENALTY POR DISTANCIA PEQUEÑA PARA QUE PUEDA APRENDER
                float rewardLogPos = Mathf.Log(1 / (distancia + 0.1f));
                //GiveReward(rewardLogNeg / 10);
                //GiveReward(rewardLogPos);

            }
            else  //ORANGE 
            {
                float distancia = CalculateDistanceGeneric(leaveOranje);
                float distanciaAumentada = distancia * 10;
                if (distanciaAumentada < presitionGoal) { GoalReachedNewTask(); }
                float rewardLogNeg = -Mathf.Log(distancia + 1);  //PARA AÑADIR PENALTY POR DISTANCIA PEQUEÑA PARA QUE PUEDA APRENDER
                float rewardLogPos = Mathf.Log(1 / (distancia + 0.1f));
                //GiveReward(rewardLogNeg / 10);
                //GiveReward(rewardLogPos);

            }


        }
    }

    public void RewardTerceraFase()
    {

        List < float> jointDiff = cobotControl.CalculateAllJointAngleDifferences();
        float totalDifference = 0.0f;
        foreach (float diff in jointDiff)
        {
            totalDifference += Mathf.Abs(diff); // Usa Mathf.Abs para obtener el valor absoluto
        }
        if (cobotControl.JointsInPosition(jointDiff))
        {
            GoalReachedNewTask();
        }
        else
        {

            float distancia = Vector3.Distance(positionOrigenUp.transform.position, cobotControl.pinzaFinalBola.transform.position);
            float rewardLogNeg = -Mathf.Log(distancia + 1);  //PARA AÑADIR PENALTY POR DISTANCIA PEQUEÑA PARA QUE PUEDA APRENDER

            GiveReward(rewardLogNeg / 10);


            if (totalDifference != 0) {
                //float rewardToGive =  totalDifference / 2000;
                //float penalty = -totalDifference / 5000; //needed for compensation penaly 

                //float rewardToGive = (1 / totalDifference) * 10 ; 
                //Debug.Log(penalty); 
                //GiveReward(penalty);
            }
        }



    }


    public override void CollectObservations(VectorSensor sensor)
    {



        if (partialClipping == true) {


        
            float distanceToGivenPoint = Vector3.Distance(new Vector3(givenPoint.position.x, 0, givenPoint.position.z), new Vector3(transform.position.x, 0, transform.position.z));
            float distanceFromPerson = Vector3.Distance(new Vector3(person.transform.position.x, 0, person.transform.position.z), new Vector3(transform.position.x , 0 , transform.position.z));

            if(distanceToGivenPoint < distanceFromPerson)
            {

                clipPositionPerson = true; 
            }
            else
            {
                clipPositionPerson = false; 
            }

            //Debug.Log(clipPositionPerson.ToString() + ':' + transform.parent.gameObject.name); 

        }



        if (enTerceraFase)
        {

            sensor.AddObservation(cobotControl.joint1.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint2.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint3.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint4.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint5.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint6.jointPosition[0]);

            if (clipPositionPerson == false)
            {
                sensor.AddObservation(person.transform.position.x - transform.position.x);
                sensor.AddObservation(person.transform.position.z - transform.position.z);
            }
            else
            {
                sensor.AddObservation(calculateClippingPoint(person.transform.position)[0] - transform.position.x);
                sensor.AddObservation(calculateClippingPoint(person.transform.position)[1] - transform.position.z);
            }


            jointDiff = cobotControl.CalculateAllJointAngleDifferences();
  
            foreach (float diff in jointDiff)
            {
                sensor.AddObservation(diff);
            }


            //sensor.AddObservation(Vector3.Distance(   cobotControl.pinzaFinalBola.transform.position  ,    positionOrigenUp)); 
            sensor.AddObservation(cobotControl.pinzaFinalBola.transform.position.x - positionOrigenUp.transform.position.x);  
            sensor.AddObservation(cobotControl.pinzaFinalBola.transform.position.y - positionOrigenUp.transform.position.y);
            sensor.AddObservation(cobotControl.pinzaFinalBola.transform.position.z - positionOrigenUp.transform.position.z);
            sensor.AddObservation(0);



        }
        else
        {

            sensor.AddObservation(cobotControl.joint1.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint2.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint3.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint4.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint5.jointPosition[0]);
            sensor.AddObservation(cobotControl.joint6.jointPosition[0]);


            if (clipPositionPerson == false)
            {
                sensor.AddObservation(person.transform.position.x - transform.position.x);
                sensor.AddObservation(person.transform.position.z - transform.position.z);
            }
            else
            {
                sensor.AddObservation(calculateClippingPoint(person.transform.position)[0]  -transform.position.x);
                sensor.AddObservation(calculateClippingPoint(person.transform.position)[1] - transform.position.z);
            }
            sensor.AddObservation(CalculateDistanceVectorTake());
            sensor.AddObservation(CalculateDistanceVectorGeneric(leaveOranje));
            sensor.AddObservation(CalculateDistanceVectorGeneric(leavePink));
            sensor.AddObservation(colorPelota);
            //sensor.AddObservation(thresholdTemp);  //Esto sirve par intentar generalizar la threshold en vase al stress


        }




    }

    public void GiveReward(float rewardGiven)
    {
        CalculateDistanceGeneric(returnPositonTool); 
        AddReward(rewardGiven);
    }



    public void CollisionRobot(bool persona = false)
    {
        //GiveReward(-10.0f);
        //Debug.Log("Colision");

        if (persona == true)
        {

            GiveReward(-10.0f);
        }

        if (enTerceraFase) // Le doy esto por la collision para que no se pegue un golpetazo si es muy negativo, como ya ha pasado. 
        {
            //GiveReward(-100f); 
        }

        //goalReachedConsecutive = 0;
        //Debug.Log(StepCount);
        EndEpisode();


    }
    public void GoalReached()
    {
        if (primerGoal == false)
        {

            //Debug.Log("REWARD1");
            GiveReward(50.0f);
            primerGoal = true;

        }
        else
        {
            //Debug.Log("REWARD2");
            GiveReward(100.0f);
            EndEpisode();

        }

    }

    public void GoalReachedNewTask()
    {

        if (primerGoal == false)
        {
            meshRendererGoal.enabled = false;
            meshRendererPinza.enabled = true;
            Debug.Log("REWARD1");
            Debug.Log(cobotControl.CheckAnglePosition()); 
            GiveReward(50.0f);
            

            if( inicioEnPrimeraFaseSolo == true)
            {

                //cobotControl.SaveJointPositions();    //---- DESCOMENTAR ESTO AQUI

                //Debug.Log(GetCumulativeReward()); 
                EndEpisode(); 

            }
            else
            {
                SetMaterialBasedOnColor(colorPelota);
                primerGoal = true;
            }
            
            //EndEpisode();
        }

        else if (primerGoal == true)
        {
            if (activarTerceraFase == false)
            {
                Debug.Log("REWARD2");
                GiveReward(100.0f);
                Debug.Log(cobotControl.CheckAnglePosition());

                //cobotControl.SaveJointPositions();  //guardamos posicion de las joints  

                EndEpisode();
            }
            else if (enTerceraFase == false)
            {
                //Debug.Log("REWARD2");
                GiveReward(100.0f);
                enTerceraFase = true;

                if (modelSwitch)
                {

                    switcher.SwitchModel(3);
                }

                meshRendererPinza.enabled = false;

                if (colorPelota == 1)
                {
                    pinkBallLeaved.enabled = true;
                }
                else
                {
                    orangeBallLeaved.enabled = true;
                }

                meshRendererGoal.enabled = false;
            }
            else if(enTerceraFase == true)
            {
                Debug.Log("REWARD3");
                //cobotControl.SaveJointPositions_deg(true, pathRealRobot);
                GiveReward(150.0f);
                EndEpisode();
            }
        }          
    }

    public void SetMaterialBasedOnColor(int colorPelota)
    {
        // Asegúrate de que el MeshRenderer esté asignado
        if (meshRendererPinza != null)
        {
            // Establece el material en función del valor de colorPelota
            if (colorPelota == 0)
            {
                meshRendererPinza.material = orange;
            }else if(colorPelota == -1)
            {
                meshRendererPinza.material = null;
            }
            else
            {
                meshRendererPinza.material = pink;
            }
        }
        else
        {
            Debug.LogError("MeshRenderer no asignado. Asigna el MeshRenderer en el Inspector.");
        }
    }

    private float CalculateDistance()
    {

        float distancia = 0.0f;
        if (primerGoal == false)
        {
            distancia = Vector3.Distance(cobotControl.pinzaFinalBola.transform.position, goalReachSphere.transform.position);
        }
        else
        {
            distancia = Vector3.Distance(cobotControl.pinzaFinalBola.transform.position, goalReachSphere2.transform.position);
        }

        return distancia;
    }

    private float CalculateDistanceGeneric(GameObject goalObject)
    {
        float distancia = 0.0f;

        if (goalObject != null)
        {
            distancia = Vector3.Distance(cobotControl.pinzaFinalBola.transform.position, goalObject.transform.position);
        }
        else
        {
            Debug.LogError("Error: El objeto de destino es nulo.");
        }

        return distancia;
    }

    private Vector3 CalculateDistanceVectorTake()
    {
        Vector3 cobotPosition = cobotControl.pinzaFinalBola.transform.position;

        // Si primerGoal es true, devuelve un vector con distancias en X y Z igual a 0
        if (primerGoal)
        {
            return new Vector3(0f, 0f, 0f);
        }
        else
        {

            if (enTerceraFase) //////////////////////////////////////////// CREO QUE AQUI NO SE ENTRA NUNCA TENGO QUE CHECKEARLO SHIT! 
            {

                float distanceX = positionOrigen.transform.position.x - cobotPosition.x;
                float distanceY = positionOrigen.transform.position.y - cobotPosition.y;
                float distanceZ = positionOrigen.transform.position.z - cobotPosition.z;

                return new Vector3(distanceX, distanceY, distanceZ);


            }
            else
            {         
                // Si primerGoal es false, devuelve la distancia con goalReachSphere2
                Vector3 goalPosition = goalReachSphere2.transform.position;

                float distanceX = goalPosition.x - cobotPosition.x;
                float distanceY = goalPosition.y - cobotPosition.y;
                float distanceZ = goalPosition.z - cobotPosition.z;

                return new Vector3(distanceX, distanceY, distanceZ);

            }
        }
    }


    private Vector3 CalculateDistanceVector()
    {
        Vector3 cobotPosition = cobotControl.pinzaFinalBola.transform.position;
        Vector3 goalPosition = primerGoal ? goalReachSphere2.transform.position : goalReachSphere.transform.position;

        float distanceX = goalPosition.x - cobotPosition.x;
        float distanceY = goalPosition.y - cobotPosition.y;
        float distanceZ = goalPosition.z - cobotPosition.z;

        return new Vector3(distanceX, distanceY, distanceZ);
    }

    Vector3 CalculateDistanceVectorGeneric(GameObject otherObject)
    {

        if (gameObject != null && otherObject != null)
        {
            Vector3 thisPosition = transform.position;
            Vector3 otherPosition = otherObject.transform.position;
            float distanceX = otherPosition.x - thisPosition.x;
            float distanceZ = otherPosition.z - thisPosition.z;
            Vector3 distanceVector = new Vector3(distanceX, 0f, distanceZ);
            return distanceVector;
        }
        else
        {
            Debug.LogError("Error: Uno o ambos objetos son nulos.");
            return Vector3.zero;
        }
    }


    public Vector2 calculateClippingPoint(Vector3 sensorPosition)
    {
        float minDistance = float.MaxValue; // Inicializa con el máximo valor posible
        Vector2 closestPoint = new Vector2(); // Punto más cercano inicializado en (0,0)

        foreach (Transform child in spawnPersona.transform)
        {
            Vector3 childPosition = child.position;
            // Calcula la distancia en el plano xz
            float distance = Vector2.Distance(new Vector2(sensorPosition.x, sensorPosition.z), new Vector2(childPosition.x, childPosition.z));

            // Si la distancia es menor que la distancia mínima encontrada hasta ahora, actualiza
            if (distance < minDistance)
            {
                minDistance = distance; // Nueva distancia mínima
                closestPoint = new Vector2(childPosition.x, childPosition.z); // Nuevo punto más cercano
            }
        }

        return closestPoint; // Devuelve el punto más cercano encontrado
    }



    public float CalculateDistances_person_joints()
    {
        // Inicializa un acumulador para la suma de distancias
        float totalDistance = 0;

        // Obtiene la posición x y z del humano
        float humanPosX = person.transform.position.x;
        float humanPosZ = person.transform.position.z;

        // Crea un array con todas las articulaciones
        ArticulationBody[] joints = new ArticulationBody[] {
            cobotControl.joint4,
            cobotControl.joint5,
            cobotControl.joint6
        };

        // Calcula y suma la distancia para cada articulación
        for (int i = 0; i < joints.Length; i++)
        {
            ArticulationBody joint = joints[i];
            float jointPosX = joint.transform.position.x;
            float jointPosZ = joint.transform.position.z;

            // Calcula la distancia en el plano x, z
            float distance = Mathf.Sqrt((humanPosX - jointPosX) * (humanPosX - jointPosX) +
                                        (humanPosZ - jointPosZ) * (humanPosZ - jointPosZ));

            // Suma la distancia al total
            totalDistance += distance;
        }

        return totalDistance;  // Devuelve la suma de las distancias
    }


    public float CalculateDistanceToPinzaBola()
    {
        // Inicializa un acumulador para la suma de distancias
        float totalDistance = 0;

        // Obtiene la posición x y z del humano
        float humanPosX = person.transform.position.x;
        float humanPosZ = person.transform.position.z;

        // Obtiene la posición x y z de pinzaBola
        float pinzaBolaPosX = cobotControl.pinzaFinalBola.transform.position.x;
        float pinzaBolaPosZ = cobotControl.pinzaFinalBola.transform.position.z;

        // Calcula la distancia en el plano x, z
        float distance = Mathf.Sqrt((humanPosX - pinzaBolaPosX) * (humanPosX - pinzaBolaPosX) +
                                    (humanPosZ - pinzaBolaPosZ) * (humanPosZ - pinzaBolaPosZ));

        // Suma la distancia al total
        totalDistance += distance;

        return totalDistance;  // Devuelve la distancia total
    }

    public void ClearDistances()
    {
        distances.Clear();
    }


    public float GetAverageDistance()
    {
        if (distances.Count == 0)
            return 0.0f;

        float sum = 0.0f;
        foreach (float dist in distances)
        {
            sum += dist;
        }

        float average = sum / distances.Count; 

        //SaveResultToFile(average.ToString("F2"), colorPelota.ToString("F2"));

        numeroDeVecesGuardado = numeroDeVecesGuardado + 1;

        CheckChangeFileAndThreshold(numeroDeVecesGuardado); 

        

        return average;
    }

    void SaveResultToFile(string number, string colorPelotaS)
    {
        // Camino al archivo donde se guardarán los resultado13
        string filePath = Path.Combine(Application.persistentDataPath, rutaArchivoGuardado);

        // Añade el número al archivo, seguido de un salto de línea

        if (colorPelota == 0 || colorPelota == 1){  //NARANJA 

            File.AppendAllText(filePath, number + ":" + steps +  "\n");

        }
    }

    public float GeometricDistanceReward(float value)  // PENSAR AQUI EN METER LA FUNCION DE TEMPERATURA, AHORA MISMO NO TENEMOS FUNCION DE TEMPERATURA, SOBRETODO PARA LA THRESHOLDSIGN
    {
        // Define thresholds within the function
        float thresholdSign = thresholdTemp;  // Example threshold for switching from penalty to reward
        float thresholdMax = distanceMaxThreshold;   // Example maximum distance for full positive reward

        // Ensure value is not zero to avoid division by zero
        if (value == 0) return -1.0f;

        float factor1 = (value - thresholdSign) / Mathf.Max(value, 1e-10f);  // Safeguard division by valueget
        float factor2 = Mathf.Max(thresholdMax - value, 1e-10f);  // Safeguard to ensure factor2 is never zero

        // Use a custom implementation of tanh if Mathf.Tanh is not available
        float exponent = 2 * (factor1 / factor2);
        float eToTheX = Mathf.Exp(exponent);  // Use Mathf.Exp to calculate e^x
        float tanhValue = (eToTheX - 1) / (eToTheX + 1);  // tanh formula

        // Invert the tanh output to penalize closeness and reward distance
        return tanhValue;
    }


    float GetRandomThreshold()
    {
        // Lista de valores posibles
        float[] possibleValues = { 0.2f, 0.1f, 0.4f, 0.5f, 0.6f, 0.3f, 0.7f, 0.8f, 0.9f, 1.0f, 1.1f };

        // Generar un índice aleatorio
        int randomIndex = Random.Range(0, possibleValues.Length);

        // Devolver el valor correspondiente al índice aleatorio

        //Debug.Log(randomIndex); 

        if (thresholdTempHelper == -1.0f)
        {


            return possibleValues[randomIndex];
            
        }
        else
        {


            return thresholdTempHelper; 
        }
        
    }

    float CalculateFloatRewardAngle(float joint1, float joint2, float joint3, float joint4, float joint5, float joint6)
    {
        // Comprueba si alguna de las articulaciones supera el umbral
        if (joint1 > thresholdAngle || joint2 > thresholdAngle || joint3 > thresholdAngle ||
            joint4 > thresholdAngle || joint5 > thresholdAngle || joint6 > thresholdAngle)
        {
            return -0.1f;
        }

        // Si ninguna supera el umbral, retorna 0.0f
        return 0.0f;
    }


    float calculateThresholdBasedOnComfortLevelAndDistance()
    {


        float increment = distanceMaxThreshold / numberOfComfortLevels; 


        return distanceMaxThreshold - increment * CurrentComfortLevel;

        //si es 10 vamos a hacer esta segunda funcion 

        //Esto deberia funcionar mejor yo creo 

        // 1.5 - 0.15 * 1
        // 1.5 - 0.15 * 10
    }


    float calculateIncrementAnglePerStepBasedOnComfortLevel()
    {

        return 2.0f / numberOfComfortLevels * CurrentComfortLevel; 

    }




    void CheckChangeFileAndThreshold(int veces)
    {


        Debug.Log(veces); 

    }

}
