using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource coinSound;

    private void Start()
    {
        // Get the AudioSource component attached to the coin
        coinSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided is the player
        if (other.CompareTag("Player"))
        {
            // Play the collection sound
            coinSound.Play();

            // Disable the coin's collider and sprite renderer to prevent further interaction but keep it in the scene
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            // Destroy the coin after the sound has finished playing
            Destroy(gameObject, coinSound.clip.length);
        }
    }
}

