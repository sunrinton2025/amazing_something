using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    static string nextScene;
    public static bool loaded;

    [SerializeField]
    private Slider progress;

    private const float FakeLoadDuration = 1.5f;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");

        Time.timeScale = 1;
    }

    private void Start()
    {
        StartCoroutine(SceneProcess());
    }

    IEnumerator SceneProcess()
    {
        loaded = false;
        AsyncOperation load = SceneManager.LoadSceneAsync(nextScene);
        load.allowSceneActivation = false;

        float timer = 0f;

        while (!load.isDone)
        {
            yield return null;

            float targetProgress = Mathf.Clamp01(load.progress / 0.9f);
            timer += Time.unscaledDeltaTime;

            if (progress != null)
                progress.value = Mathf.Lerp(progress.value, targetProgress, 0.1f);

            // 로딩 완료되고 난 뒤의 연출 시간
            if (load.progress >= 0.9f && progress.value >= 0.99f)
            {
                yield return new WaitForSecondsRealtime(FakeLoadDuration);
                loaded = true;
                load.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
