using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject boss;
    public GameObject rocket;
    public GameObject canbus;
    private float targetAlpha = 1f; // 최종 alpha 값 (완전히 불투명)
    private float duration = 2f; // 천천히 증가할 시간 (초)
    private float startAlpha = 0f; // 시작 alpha 값
    private float timeElapsed = 0f; // 경과 시간
    private bool isAppearing = false; // 보스가 나타나고 있는지 여부
    private bool isAppearingC = false; // 보스가 나타나고 있는지 여부
    private bool isPlayed = false; // 보스가 나타나고 있는지 여부
    public GameObject[] squares; // 이미 생성된 3개의 사각형들
    public float patternChangeInterval = 3f; // 패턴 변경 시간 간격
    private bool isPatternActive = false; // 패턴 실행 상태
    private float patternTimer = 0f; // 패턴 타이머

    void Start()
    {
        canbus.gameObject.SetActive(false);
        startAlpha = boss.GetComponent<SpriteRenderer>().color.a;
        isAppearingC = false;
        foreach (GameObject square in squares)
        {
            square.SetActive(false);
        }
    }

    void Update()
    {
        if ((PlayerController.Local.transform.position - rocket.transform.position).magnitude <= 10f)
        {
            if (!isPlayed)
            {
                canbus.SetActive(true);
            }
            isAppearingC = true;
        }
        else
        {
            canbus.SetActive(false);
            isAppearingC = false;
        }
        if (Input.GetKeyDown(KeyCode.F) && !isAppearing && isAppearingC && !isPlayed)
        {
            isAppearing = true;
            canbus.gameObject.SetActive(false);
            isAppearingC = false;
            timeElapsed = 0f;
            BossFight();
        }
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
    void BossFight()
    {
        patternTimer += Time.deltaTime;

        if (patternTimer >= patternChangeInterval && !isPatternActive) // 일정 시간이 지나면
        {
            patternTimer = 0f; // 타이머 초기화
            StartRandomPattern(); // 랜덤 패턴 실행
        }
    }
    void StartRandomPattern()
    {
        int patternIndex = Random.Range(0, 3); // 0~2 사이의 랜덤 값
        GameObject selectedSquare = squares[patternIndex]; // 선택된 사각형

        selectedSquare.SetActive(true); // 사각형 활성화
        StartCoroutine(ApplyGradient(selectedSquare)); // 그라데이션 애니메이션 실행
        isPatternActive = true; // 패턴이 활성화됨
    }

    // 사각형에 그라데이션 효과를 적용하는 애니메이션
    IEnumerator ApplyGradient(GameObject square)
    {
        float duration = 1f; // 그라데이션 효과가 지속될 시간 (1초)
        SpriteRenderer renderer = square.GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color; // 원래 색
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f); // 목표 색 (불투명)

        // alpha 초기화
        renderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        float time = 0f;
        while (time < duration)
        {
            // 색상 보간: 원래 색에서 목표 색으로 alpha를 점차적으로 변경
            renderer.color = Color.Lerp(new Color(originalColor.r, originalColor.g, originalColor.b, 0f), targetColor, time / duration);
            time += Time.deltaTime;
            yield return null; // 한 프레임 대기
        }

        // 애니메이션 종료 후 최종 색상 적용
        renderer.color = targetColor;

        // 애니메이션 완료 후 사각형 비활성화
        yield return new WaitForSeconds(1f); // 애니메이션이 끝난 후 잠시 대기
        square.SetActive(false); // 사각형 비활성화
        isPatternActive = false; // 패턴 종료
    }
}
