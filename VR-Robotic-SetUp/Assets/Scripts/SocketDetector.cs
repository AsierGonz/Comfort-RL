using UnityEngine;

public class SocketDetector : MonoBehaviour
{
    [SerializeField] private GameObject originalPositionPink;
    [SerializeField] private GameObject originalPositionOrange;
    [SerializeField] private GameObject Orange;
    [SerializeField] private GameObject Pink;

    private GameObject currentObjectInSocket;

    public int checkColor = 2;

    public void Start()
    {
        checkColor = 2;
        currentObjectInSocket = null; 
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check the tag of the GameObject and perform actions accordingly
        if (other.tag == "Pink")
        {
            checkColor = 1;
            currentObjectInSocket = other.gameObject;
        }
        else if (other.tag == "Orange")
        {
            checkColor = 0;
            currentObjectInSocket = other.gameObject;
        }



        Debug.Log(other.gameObject.name); 
    }

    public void ReturnBack()
    {
        Debug.Log("CALLED");
        if(currentObjectInSocket != null) { 

            if (currentObjectInSocket.tag == "Pink")
            {
                // Perform action for Pink GameObject and teleport
                Debug.Log("Returning Pink GameObject.");
                if (originalPositionPink != null)
                {

                    Pink.SetActive(false); 



                    Debug.Log("positionREturned");
                }
            }
            else if (currentObjectInSocket.tag == "Orange")
            {
                // Perform action for Orange GameObject and teleport
                Debug.Log("Returning Orange GameObject.");
                if (originalPositionOrange != null)
                {
                    Orange.SetActive(false);


                }
            }

            // Reset checkColor to 0 after teleporting
            checkColor = 2;
            currentObjectInSocket = null; 
            Debug.Log($"GameObject {gameObject.name} has been returned to its original position. checkColor reset to {checkColor}.");
        }
    }




}
