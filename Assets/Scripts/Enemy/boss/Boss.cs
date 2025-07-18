using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject boss;
    private float targetAlpha = 1f; // 최종 alpha 값 (완전히 불투명)
    private float duration = 2f; // 천천히 증가할 시간 (초)
    private float startAlpha = 0f; // 시작 alpha 값
    private float timeElapsed = 0f; // 경과 시간
    private bool isAppearing = false; // 보스가 나타나고 있는지 여부

    void Start()
    {
        startAlpha = boss.GetComponent<SpriteRenderer>().color.a;
    }

    void Update()
    {
        // 마우스 클릭시 AppearBoss 호출
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAppearing)
        {
            isAppearing = true;
            timeElapsed = 0f; // 새로운 애니메이션 시작 시 timeElapsed를 초기화
        }

        // 보스가 나타날 때 alpha 값을 변경
        if (isAppearing)
        {
            AppearBoss();
        }
    }

    void AppearBoss()
    {
        if (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime; // 경과 시간 증가
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / duration);
            Color newColor = new Color(boss.GetComponent<SpriteRenderer>().color.r, boss.GetComponent<SpriteRenderer>().color.g, boss.GetComponent<SpriteRenderer>().color.b, alpha);
            boss.GetComponent<SpriteRenderer>().color = newColor;
        }
        else
        {
            isAppearing = false; // 애니메이션 종료 후 더 이상 변하지 않도록 설정
        }
    }
}
