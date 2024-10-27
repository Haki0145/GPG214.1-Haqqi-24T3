using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    public ObjectPooler objectPooler;
    public float spawnInterval = 2f;
    public Vector2 spawnRangeX = new Vector2(-10f, 10f);
    public Vector2 spawnRangeY = new Vector2(-5f, 5f);
    public int coinsPerInterval = 3;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            for (int i = 0; i < coinsPerInterval; i++)
            {
                SpawnCoin();
            }
            timer = 0f;
        }
    }

    void SpawnCoin()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        GameObject coin = objectPooler.GetPooledObject();
        coin.transform.position = spawnPosition;
        coin.SetActive(true);
    }

    
}
