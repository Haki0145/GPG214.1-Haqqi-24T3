using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public CoinController coinPrefab;

    private void Awake()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        //ObjectPooler.SetupPool(coinPrefab, 10, "Coin");
    }
}
