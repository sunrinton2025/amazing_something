using System.Collections;
using System.Collections.Generic;
using minyee2913.Utils;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    public float PlayerDist;
    public EnemySpawner enemySpawner;
    public int MaxEnemyCount;
    public int MinEnemyCount;
    public Vector3 MinSpawnPos;
    public Vector3 MaxSpawnPos;

    public List<GameObject> EnemyList;
    public List<GameObject> SpawnedEnemyList;
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
        PlayerDist = Vector2.Distance(PlayerController.Local.transform.position, transform.position);
    }
    void FixedUpdate()
    {
        if (SpawnedEnemyList.Count <= MinEnemyCount)
        {
            Spawn();
        }
    }
    void Spawn()
    {
        for (int i = SpawnedEnemyList.Count; i > MaxEnemyCount; i++)
        {
            float randSpawnX = 1; //함수
            enemySpawner.Spawn(randSpawnX);
        }
    }
}