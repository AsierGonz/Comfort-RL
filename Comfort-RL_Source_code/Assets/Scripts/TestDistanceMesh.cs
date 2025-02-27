using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistanceMesh : MonoBehaviour
{
    [SerializeField] public Transform object1;
    [SerializeField] public Transform object2;

    float minDistance; 

    void Start()
    {
        minDistance = CalculateMinDistance(object1, object2);
        Debug.Log("La distancia mínima entre las mallas es: " + minDistance);
    }

    private void Update()
    {

        minDistance = CalculateMinDistance(object1, object2);
        Debug.Log("La distancia mínima entre las mallas es: " + minDistance);
    }

    float CalculateMinDistance(Transform obj1, Transform obj2)
    {
        Mesh mesh1 = obj1.GetComponent<MeshFilter>().mesh;
        Mesh mesh2 = obj2.GetComponent<MeshFilter>().mesh;

        float minDistance = float.MaxValue;

        foreach (Vector3 vertex1 in mesh1.vertices)
        {
            Vector3 worldVertex1 = obj1.TransformPoint(vertex1);
            foreach (Vector3 vertex2 in mesh2.vertices)
            {
                Vector3 worldVertex2 = obj2.TransformPoint(vertex2);
                float distance = Vector3.Distance(worldVertex1, worldVertex2);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }

        return minDistance;
    }
}
