using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadTexture : MonoBehaviour
{
    public string fileName = "Horse.jpg";
    public string folderPath = Application.streamingAssetsPath;
    private string combinedFilePathLocation;
    public Image Horse;


    // Start is called before the first frame update
    void Start()
    {
        //combined the path
        combinedFilePathLocation = Path.Combine(folderPath, fileName);
        Debug.Log(combinedFilePathLocation);
        LoadTheTexture();
    }

   private void LoadTheTexture()
    {
        if (File.Exists(combinedFilePathLocation))
        {
            byte[] imageBytes = File.ReadAllBytes(combinedFilePathLocation);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            Horse.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero); 
        }
        else
        {
            Debug.Log("Image  File not found at path");
        }

        
    }
}
