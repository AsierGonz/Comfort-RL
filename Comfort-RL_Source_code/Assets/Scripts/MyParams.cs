using System;
using UnityEngine;

[Serializable]
public class MyParams
{
    public Goals goals;
    public Penalties penalties;
    public Phases phases;
    public HumanInteraction humanInteraction;
    public Environment environment;
    public ComfortParameters comfortParameters;
    public Miscellaneous miscellaneous;

 
    public MyParams(Goals goals, Penalties penalties, Phases phases, HumanInteraction humanInteraction,
                    Environment environment, ComfortParameters comfortParameters, Miscellaneous miscellaneous)
    {
        this.goals = goals;
        this.penalties = penalties;
        this.phases = phases;
        this.humanInteraction = humanInteraction;
        this.environment = environment;
        this.comfortParameters = comfortParameters;
        this.miscellaneous = miscellaneous;
    }

    public MyParams()
    {
        this.goals = new Goals();
        this.penalties = new Penalties();
        this.phases = new Phases();
        this.humanInteraction = new HumanInteraction();
        this.environment = new Environment();
        this.comfortParameters = new ComfortParameters();
        this.miscellaneous = new Miscellaneous();
    }
}

[Serializable]
public class Goals
{
    public float precisionGoal;


    public Goals()
    {
        this.precisionGoal = 3.0f;
    }


    public Goals(float precisionGoal)
    {
        this.precisionGoal = precisionGoal;
    }
}

[Serializable]
public class Penalties
{
    public bool activatePenalty;


    public Penalties()
    {
        this.activatePenalty = false;
    }

  


    public Penalties(bool activatePenalty)
    {
        this.activatePenalty = activatePenalty;
    }
}

[Serializable]
public class Phases
{
    public bool startInFirstPhaseOnly;
    public bool startInSecondPhaseOnly;
    public bool startInThirdPhaseOnly;
    public bool activateThirdPhase;


    public Phases()
    {
        this.startInFirstPhaseOnly = false;
        this.startInSecondPhaseOnly = false;
        this.startInThirdPhaseOnly = false;
        this.activateThirdPhase = true;
    }


    public Phases(bool startInFirstPhaseOnly, bool startInSecondPhaseOnly, bool startInThirdPhaseOnly, bool activateThirdPhase)
    {
        this.startInFirstPhaseOnly = startInFirstPhaseOnly;
        this.startInSecondPhaseOnly = startInSecondPhaseOnly;
        this.startInThirdPhaseOnly = startInThirdPhaseOnly;
        this.activateThirdPhase = activateThirdPhase;
    }
}

[Serializable]
public class HumanInteraction
{
    public bool humanMove;
    public bool personMovement;
    public float teleportationDistance;
    public float humanMovementMax;
    public int[] spawnHuman;

    
    public HumanInteraction()
    {
        this.humanMove = true;
        this.personMovement = false;
        this.teleportationDistance = 0.015f;
        this.humanMovementMax = 0.5f;
        this.spawnHuman = new int[] {6};
    }

    public HumanInteraction(bool humanMove, bool personMovement, float teleportationDistance, float humanMovementMax, int[] spawnHuman)
    {
        this.humanMove = humanMove;
        this.personMovement = personMovement;
        this.teleportationDistance = teleportationDistance;
        this.humanMovementMax = humanMovementMax;
        this.spawnHuman = spawnHuman;
    }
}

[Serializable]
public class Environment
{
    public Vector3 pinktablePosition;
    public Vector3 greytablePosition;
    public Vector3 orangetablePosition;
    public int colorBall;
    public string routeSaveFile;
    public int numberOfEnvs;


    public Environment()
    {
        this.pinktablePosition = new Vector3(0, 0, 0);
        this.greytablePosition = new Vector3(0, 0, 0);
        this.orangetablePosition = new Vector3(0, 0, 0);
        this.colorBall = 1;
        this.routeSaveFile = "save_file.txt";
        this.numberOfEnvs = 1; 
    }

    public Environment(Vector3 pinktablePosition, Vector3 greytablePosition, Vector3 orangetablePosition, int colorBall, string routeSaveFile, int numberOfEnvs)
    {
        this.pinktablePosition = pinktablePosition;
        this.greytablePosition = greytablePosition;
        this.orangetablePosition = orangetablePosition;
        this.colorBall = colorBall;
        this.routeSaveFile = routeSaveFile;
        this.numberOfEnvs = numberOfEnvs;
    }
}

[Serializable]
public class ComfortParameters
{
    public float temperature;
    public float numberOfComfortLevels;
    public float currentComfortLevel;
    public float distanceMaxThreshold;
    public int alpha;
    public int beta;

    public ComfortParameters()
    {
        this.temperature = 0.0f;
        this.numberOfComfortLevels = 10.0f;
        this.currentComfortLevel = 8.0f;
        this.distanceMaxThreshold = 1.5f;
        this.alpha = 1;
        this.beta = 0;
    }


    public ComfortParameters(float temperature, float numberOfComfortLevels, float currentComfortLevel, float distanceMaxThreshold, int alpha, int beta)
    {
        this.temperature = temperature;
        this.numberOfComfortLevels = numberOfComfortLevels;
        this.currentComfortLevel = currentComfortLevel;
        this.distanceMaxThreshold = distanceMaxThreshold;
        this.alpha = alpha;
        this.beta = beta;
    }
}

[Serializable]
public class Miscellaneous
{
    public bool randomStart;
    public bool clipPositionPerson;
    public bool singleTrainingOnly;
    public bool checkVR_socket;
    public bool saveResults;
    public int numberOfCameras;
    public string pathToSaveRealRobot;
    public bool switcher; 

    
    public Miscellaneous()
    {
        this.randomStart = false;
        this.clipPositionPerson = false;
        this.singleTrainingOnly = false;
        this.checkVR_socket = false;
        this.saveResults = false;
        this.numberOfCameras = 2;
        this.pathToSaveRealRobot = "Assets/Positions/trayectory1.txt";
        this.switcher = true; 
    }

    public Miscellaneous(bool randomStart, bool clipPositionPerson, bool singleTrainingOnly, bool checkVR_socket, bool saveResults, int numberOfCameras, string pathToSaveRealRobot, bool switcher)
    {
        this.randomStart = randomStart;
        this.clipPositionPerson = clipPositionPerson;
        this.singleTrainingOnly = singleTrainingOnly;
        this.checkVR_socket = checkVR_socket;
        this.saveResults = saveResults;
        this.numberOfCameras = numberOfCameras;
        this.pathToSaveRealRobot = pathToSaveRealRobot;
        this.switcher = switcher; 
    }
}
