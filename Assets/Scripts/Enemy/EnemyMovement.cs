using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float DetectDist;
    public float AddDist;
    public float AttackDist;

    void Start()
    {

    }
    void Update()
    {

    }
    private void EnemyCycle()
    {
        if (EnemySystem.Instance.PlayerDist >= DetectDist)
        {

        }
        else
        {
            DetectDist += AddDist;

        }
    }
}
