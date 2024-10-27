using System.Xml; // Required for XML documents.
using System.Xml.Serialization; // Required for serializing and deserializing objects to/from XML.
using System.Collections;
using System.Collections.Generic;
using System.IO; // Required for reading/writing files.
using UnityEngine;

public class SaveAndLoadXMLFiles : MonoBehaviour
{
    [SerializeField] private GameObject player; // Reference to the player GameObject.

    private PlayerStats P_stats = new PlayerStats(); // Holds player's data like name, health, and position.

    public string playerSaveFile; // The name of the file where player data will be saved.
    public string folderPath; // Path to the folder where the file will be saved.
    public string combinedFilePath; // The full path (folder + file) where data is saved.

    private void Start()
    {
        // Set the folder path to the StreamingAssets folder (used for cross-platform file access).
        folderPath = Application.streamingAssetsPath;

        // Combine the folder path and filename to create the full path for the player's save file.
        combinedFilePath = Path.Combine(folderPath, playerSaveFile);
    }

    private void Update()
    {
        // If the L key is pressed, load the player data from the XML file and update the player's position.
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadPlayerXml(); // Load player data (name, health, position) from XML file.
            loadPlayerPosition(); // Load and apply the player's position from the XML data.
        }

        // If the S key is pressed, save the player data to the XML file.
        if (Input.GetKeyDown(KeyCode.S))
        {
            savePlayerData(); // Gather current player data (name, health, position).
            savePlayerXml(); // Save that data to the XML file.
        }
    }

    // Gathers player data (name, health, position) into the P_stats object.
    private void savePlayerData()
    {
        P_stats.playerName = player.transform.name; // Get the player's name.
        P_stats.playerHealth = 5f; // Hardcoded player health value for this example.
        P_stats.playerPosition = player.transform.position; // Get the player's current position.
    }

    // Saves the player data (stored in P_stats) to an XML file.
    private void savePlayerXml()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerStats)); // Create a serializer for the PlayerStats class.

        // Write the serialized data to the file.
        using (StreamWriter sw = new StreamWriter(combinedFilePath))
        {
            serializer.Serialize(sw, P_stats); // Serialize and save the player stats to the XML file.
            Debug.Log("Saving player data"); // Output a debug message to indicate the data was saved.
        }
    }

    // Loads player data from the XML file into the P_stats object.
    private void loadPlayerXml()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerStats)); // Create a serializer for PlayerStats.

        // Read the serialized data from the file.
        using (StreamReader reader = new StreamReader(combinedFilePath))
        {
            P_stats = (PlayerStats)serializer.Deserialize(reader); // Deserialize and load the player stats from the XML file.
            Debug.Log("Loading Player Data"); // Output a debug message to indicate the data was loaded.
        }
    }

    // Loads the player's position from the XML file and sets it in the game.
    private void loadPlayerPosition()
    {
        XmlDocument document = new XmlDocument(); // Create an XML document to load the file.

        document.Load(combinedFilePath); // Load the XML file.

        // Navigate to the playerPosition node in the XML file.
        XmlNode position = document.SelectSingleNode("/PlayerStats/playerPosition");

        // If the position node is found, extract its x, y, and z values and set the player's position.
        if (position != null)
        {
            string xNode = position.SelectSingleNode("x")?.InnerText; // Get the x-coordinate from XML.
            string yNode = position.SelectSingleNode("y")?.InnerText; // Get the y-coordinate from XML.
            string zNode = position.SelectSingleNode("z")?.InnerText; // Get the z-coordinate from XML.

            // Set the player's position based on the loaded coordinates.
            setPlayerPosition(xNode, yNode, zNode);
        }
        else
        {
            Debug.Log("error"); // Log an error message if the position node isn't found.
        }
    }

    // Parses the x, y, z coordinates and applies them to the player's position.
    private void setPlayerPosition(string x, string y, string z)
    {
        // Convert string values to floats and create a Vector3 for the new position.
        Vector3 savedPosition = new Vector3(float.Parse(x), float.Parse(y), float.Parse(z));

        player.transform.position = savedPosition; // Set the player's position in the game.

        P_stats.playerPosition = savedPosition; // Update the player's position in the stats object.
    }
}