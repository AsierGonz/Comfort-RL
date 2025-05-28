using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnvs : MonoBehaviour
{
    // Serialized fields to assign cameras in the inspector
    [SerializeField]
    private Camera camera1;

    [SerializeField]
    private Camera camera2;

    [SerializeField]
    private Camera camera3;

    [SerializeField]
    private Camera camera4;
    private int numberOfEnvs;

    [SerializeField]
    private GameObject env1;
    [SerializeField]
    private GameObject env2;
    [SerializeField]
    private GameObject env3;

    void Awake()
    {
        MyParams myParams = Singleton.Instance.myObject;
        //numberOfEnvs = myParams.environment.numberOfEnvs;


        if (myParams.miscellaneous.numberOfCameras == 1){

            camera1.gameObject.SetActive(true);
            camera2.gameObject.SetActive(false);
            camera3.gameObject.SetActive(false);
            camera4.gameObject.SetActive(false);
        }
        else if (myParams.miscellaneous.numberOfCameras == 2)
        {

            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(true);
            camera3.gameObject.SetActive(false);
            camera4.gameObject.SetActive(false);
        }
        else if (myParams.miscellaneous.numberOfCameras == 3)
        {

            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(false);
            camera3.gameObject.SetActive(true);
            camera4.gameObject.SetActive(false);
        }
        else if (myParams.miscellaneous.numberOfCameras == 4)
        {

            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(false);
            camera3.gameObject.SetActive(false);
            camera4.gameObject.SetActive(true);
        }
        else
        {
            camera1.gameObject.SetActive(true);
            camera2.gameObject.SetActive(false);
            camera3.gameObject.SetActive(false);
            camera4.gameObject.SetActive(false);

        }


    }
}
