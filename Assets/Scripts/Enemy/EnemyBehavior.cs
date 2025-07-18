using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // 플레이어가 detectDist보다 멀다면 돌상태, 가까워지면 활성상태로 에니메이션 재생 -> 플레이어 추적& detectDist 확장,
    // 만일 플레이어가 detectDist에서 멀어졌다면 일정 랜덤좌표로 이동을 랜덤 시간동안 실행후 비 활성화& detectDist 초기화,
    // 아니라면& 플레이어가 공격 범위내에 들어왔다면 공격
    // 만일 데미지를 입었다면 일정확률로 무시후 플레이어 공격방향의 반대로 이동
    void Start()
    {

    }

    void Update()
    {

    }
}
