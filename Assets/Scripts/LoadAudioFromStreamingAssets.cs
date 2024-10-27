using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking; // Required for UnityWebRequest

public class LoadAudioFromStreamingAsset : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component.
    private AudioClip horn;
    public string audioFileName; // Name of the audio file to load.
    private string folderPath; // Path to the StreamingAssets folder.
    private string fullPath; // Full path to the audio file.

    void Start()
    {
        // Set the folder path to the StreamingAssets folder (used for cross-platform file access).
        folderPath = Application.streamingAssetsPath;

        // Combine the folder path and file name to create the full path to the audio file.
        fullPath = Path.Combine(folderPath, audioFileName);

        LoadAudio();
        
    }

    // Coroutine to load the audio clip from the StreamingAssets folder.
    public void LoadAudio()
    {
        // Check if the file exists at the specified path.
        if (File.Exists(fullPath))
        {
            byte[] audioData = File.ReadAllBytes(fullPath);

            float[] floatArray = new float[audioData.Length / 2];

            for(int i = 0; i < floatArray.Length; i++)
            {
                short bitValue = System.BitConverter.ToInt16(audioData, i * 2);

                floatArray[i] = bitValue / 32786.0f;
            }

            horn = AudioClip.Create("Horn", floatArray.Length, 1, 44100, false);

            horn.SetData(floatArray, 0);

            audioSource.clip = horn;
        }
        else
        {
            Debug.LogError("Audio file not found: " + fullPath); // Log an error if the file isn't found.
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(audioSource.clip != null)
            {
                audioSource.PlayOneShot(horn);
            }
        }
    }
}





