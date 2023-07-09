using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;

    protected override void InitializeBehaviour()
    {
        FadeOut(() => FadeIn(() => Instance.StartCoroutine(LoadSceneAsync("Menu"))));
    }

    protected override void FinishBehaviour()
    {

    }

    public static void FadeOut(Action onFadeComplete = null)
    {
        Instance.StartCoroutine(Fade(false, onFadeComplete));
    }

    public static void FadeIn(Action onFadeComplete = null)
    {
        Instance.StartCoroutine(Fade(true, onFadeComplete));
    }

    private static IEnumerator Fade(bool _in, Action onFadeComplete = null)
    {
        Instance.fadeCanvasGroup.alpha = _in ? 0 : 1;

        float t;

        for (t = 0; t < 1; t += Time.deltaTime)
        {
            Instance.fadeCanvasGroup.alpha = _in ? (t / 1) : (1 - (t / 1));
            yield return null;
        }

        Instance.fadeCanvasGroup.alpha = _in ? 1 : 0;

        yield return new WaitForSeconds(1f);

        onFadeComplete?.Invoke();
    }

    public static void LoadScene(string scene)
    {
        FadeIn(() => Instance.StartCoroutine(LoadSceneAsync(scene)));
    }

    private static IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        yield return new WaitUntil(() => operation.isDone);

        yield return Fade(false);
    }
}
