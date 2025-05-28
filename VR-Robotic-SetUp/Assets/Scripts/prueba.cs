using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public ArticulationBody joint;
    public float targetAngle = 90f;  // In degrees, for instance.
    private bool isSleeping = false; // Flag to control sleep

    void Start()
    {
        ArticulationReducedSpace jointConfig = new ArticulationReducedSpace(targetAngle * Mathf.Deg2Rad);
        this.joint.jointPosition = jointConfig;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSleeping)
        {
            float currentPosition = this.joint.jointPosition[0]; // Assuming a single degree of freedom

            // Update the joint position by adding an angle in radians
            float angleToAdd = 1.0f * Mathf.Deg2Rad; // Change the value as needed
            currentPosition += angleToAdd;

            // Create a new ArticulationReducedSpace with the updated position and update the joint
            this.joint.jointPosition = new ArticulationReducedSpace(currentPosition);

            StartCoroutine(SleepForSeconds(1.0f)); // Start the sleep coroutine for 3 seconds
        }
    }

    IEnumerator SleepForSeconds(float seconds)
    {
        isSleeping = true;
        yield return new WaitForSeconds(seconds);
        isSleeping = false;
    }
}

