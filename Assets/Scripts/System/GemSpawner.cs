using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public int MaxGemCount;
    public int MinGemCount;

    public List<GameObject> GemList = new List<GameObject>();
    public List<GameObject> SpawnedGemList = new List<GameObject>();

    void Start()
    {
        IniSpawn();
    }

    void Update()
    {
        if (SpawnedGemList.Count <= MinGemCount)
        {
            AvoidSpawn();
        }
    }

    void AvoidSpawn()
    {
        for (int i = SpawnedGemList.Count; i < MaxGemCount; i++)
        {
            (float minX, float maxX) = MapManager.Instance.GetXBoundsOfMaps();
            (float mapMinX, float mapMaxX) = MapManager.Instance.GetXBoundsInCamera();

            float randSpawnX = Random.Range(minX, maxX);
            int safetyCount = 0;
            while ((mapMinX <= randSpawnX && randSpawnX <= mapMaxX) && safetyCount < 100)
            {
                randSpawnX = Random.Range(minX, maxX);
                safetyCount++;
            }
            Spawn(randSpawnX);
        }
    }
    void IniSpawn()
    {
        for (int i = SpawnedGemList.Count; i < MaxGemCount; i++)
        {
            (float minX, float maxX) = MapManager.Instance.GetXBoundsOfMaps();
            float randSpawnX = Random.Range(minX, maxX);
            Spawn(randSpawnX);
        }
    }
    public void Spawn(float XPos)
    {
        int randSpawn = Random.Range(0, GemList.Count);
        Vector2 randPos = MapManager.Instance.GetPosInMap(XPos);
        GameObject spawnedGem = Instantiate(GemList[randSpawn], randPos, Quaternion.identity);
        SpawnedGemList.Add(spawnedGem);
    }
}
