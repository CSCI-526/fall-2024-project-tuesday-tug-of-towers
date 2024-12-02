using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private TMP_Text popupText2;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 2f;

    private Dictionary<TMP_Text, CanvasGroup> canvasGroups = new Dictionary<TMP_Text, CanvasGroup>();
    private Dictionary<TMP_Text, Coroutine> fadeOutCoroutines = new Dictionary<TMP_Text, Coroutine>();

    private void Awake()
    {
        InitializeCanvasGroup(popupText);
        InitializeCanvasGroup(popupText2);
    }

    private void InitializeCanvasGroup(TMP_Text text)
    {
        if (text == null) return;
        var canvasGroup = text.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = text.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroups[text] = canvasGroup;
        canvasGroup.alpha = 0;
    }

    public void ShowMessage(string message, TMP_Text targetPopupText = null)
    {
        TMP_Text popupTextToUse = targetPopupText ?? popupText;

        if (canvasGroups.ContainsKey(popupTextToUse))
        {
            popupTextToUse.text = message;
            if (fadeOutCoroutines.ContainsKey(popupTextToUse))
            {
                StopCoroutine(fadeOutCoroutines[popupTextToUse]);
            }
            canvasGroups[popupTextToUse].alpha = 1;
            fadeOutCoroutines[popupTextToUse] = StartCoroutine(FadeOut(popupTextToUse));
        }
    }

    // Coroutine to fade the message out after displayTime
    private IEnumerator FadeOut(TMP_Text targetPopupText)
    {
        yield return new WaitForSeconds(displayTime);

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroups[targetPopupText].alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            yield return null;
        }
        canvasGroups[targetPopupText].alpha = 0;
    }
}