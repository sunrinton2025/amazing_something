using System.Collections.Generic;
using minyee2913.Utils;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    public EnemySpawner enemySpawner;
    public int MaxEnemyCount;
    public int MinEnemyCount;

    public List<GameObject> EnemyList = new List<GameObject>();
    public List<GameObject> SpawnedEnemyList = new List<GameObject>();

    void Awake()
    {
        enemySpawner = this.GetComponent<EnemySpawner>();
    }

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if (SpawnedEnemyList.Count <= MinEnemyCount)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Debug.Log("spawn");

        for (int i = SpawnedEnemyList.Count; i < MaxEnemyCount; i++)
        {
            (float minX, float maxX) = MapManager.Instance.GetXBoundsOfMaps();
            (float mapMinX, float mapMaxX) = MapManager.Instance.GetXBoundsInCamera();

            float randSpawnX = Random.Range(minX, maxX);
            int safetyCount = 0;
            while ((mapMinX <= randSpawnX && randSpawnX <= mapMaxX) && safetyCount < 100)
            {
                Debug.Log("Looping...");
                randSpawnX = Random.Range(minX, maxX);
                safetyCount++;
            }

            if (safetyCount >= 100)
            {
                Debug.LogWarning("Spawn position generation failed: too close to camera.");
                continue;
            }
            enemySpawner.Spawn(randSpawnX);
        }
    }
}
