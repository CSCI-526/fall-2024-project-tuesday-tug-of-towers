using System.Collections;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeDuration = 1f;

    private TMP_Text textComponent;

    public void SetWorldPosition(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    public void SetText(string text)
    {
        textComponent = GetComponent<TMP_Text>();

        if (textComponent != null)
        {
            textComponent.text = text;
        }
        else
        {
            Debug.LogError("TMP_Text component not found on FloatingText prefab.");
        }
    }

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();

        if (textComponent == null)
        {
            Debug.LogError("TMP_Text component not found on FloatingText prefab.");
            return;
        }

        Color originalColor = textComponent.color;
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        StartCoroutine(FadeAndMove());  
    }

    IEnumerator FadeAndMove()
    {
        float elapsedTime = 0f;
        Color originalColor = textComponent.color;

        while (elapsedTime < fadeDuration)
        {
            
            transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);

            
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
