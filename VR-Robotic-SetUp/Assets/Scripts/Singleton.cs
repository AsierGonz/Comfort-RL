using System.IO;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public MyParams myObject;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Ensure the singleton persists across scenes
        }

        string fileName = "jsonfile.json";
        string filePath = GetConfigFilePath(fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            myObject = JsonUtility.FromJson<MyParams>(json);
            Debug.Log("la precision es " + myObject.goals.precisionGoal); 
        }
        else
        {
            myObject = new MyParams(); // Initialize with default parameters if file does not exist
        }
    }

    private string GetConfigFilePath(string fileName)
    {
        // Use Application.dataPath to navigate to the Config folder in the root directory
        string folderPath = Path.Combine(Application.dataPath, "../Config");
        return Path.Combine(folderPath, fileName);
    }
}
