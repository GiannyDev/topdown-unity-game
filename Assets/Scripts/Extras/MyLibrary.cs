using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class MyLibrary
{
    public static bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }

    public static IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float duration, float desiredAlpha, UnityAction onComplete = null)
    {
        float currentAlpha = canvasGroup.alpha;
        float timer = 0;

        while (timer < 1f)
        {
            float targetAlpha = Mathf.SmoothStep(currentAlpha, desiredAlpha, timer);
            canvasGroup.alpha = targetAlpha;

            timer += duration * Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = desiredAlpha;
        onComplete?.Invoke();
    }
}