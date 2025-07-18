using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void Spawn(float XPos)
    {
        int randSpawn = Random.Range(0, EnemySystem.Instance.EnemyList.Count);
        Vector3 randSpawnPos = new Vector3(
            XPos, 1//힘수가 들어갈 예정
        );

        GameObject spawnedEnemy = Instantiate(EnemySystem.Instance.EnemyList[randSpawn], randSpawnPos, Quaternion.identity);
        EnemySystem.Instance.SpawnedEnemyList.Add(spawnedEnemy);
    }
}
