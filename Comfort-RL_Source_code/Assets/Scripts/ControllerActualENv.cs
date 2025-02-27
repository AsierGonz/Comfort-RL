using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerActualENv : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject rosa;
    [SerializeField] private GameObject naraja;
    [SerializeField] private GameObject gris;

    void Start()
    {

        MyParams myParams = Singleton.Instance.myObject;
        //naraja.transform.position = myParams.environment.orangetablePosition;
        //rosa.transform.position = myParams.environment.pinktablePosition;
        //gris.transform.position = myParams.environment.greytablePosition;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
