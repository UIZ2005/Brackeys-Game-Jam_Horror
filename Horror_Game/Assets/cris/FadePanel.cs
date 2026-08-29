using System.Collections;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f;

    private CanvasGroup canvasGroup;
    private Coroutine currentFadeCoroutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);

        if (currentFadeCoroutine != null)
            StopCoroutine(currentFadeCoroutine);

        currentFadeCoroutine = StartCoroutine(Fade(canvasGroup.alpha, 1f, false));
    }

    public void FadeOut()
    {
        if (currentFadeCoroutine != null)
            StopCoroutine(currentFadeCoroutine);

        currentFadeCoroutine = StartCoroutine(Fade(canvasGroup.alpha, 0f, true));
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha, bool deactivateAtEnd)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (deactivateAtEnd)
            gameObject.SetActive(false);
    }
}
