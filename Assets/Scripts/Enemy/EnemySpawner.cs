using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void Spawn(float XPos)
    {
        int randSpawn = Random.Range(0, EnemySystem.Instance.EnemyList.Count);
        Vector2 randPos = MapManager.Instance.GetPosInMap(XPos);
        GameObject spawnedEnemy = Instantiate(EnemySystem.Instance.EnemyList[randSpawn], randPos, Quaternion.identity);
        EnemySystem.Instance.SpawnedEnemyList.Add(spawnedEnemy);
    }
}
