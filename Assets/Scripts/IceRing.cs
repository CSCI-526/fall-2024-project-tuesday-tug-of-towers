using UnityEngine;

public class IceRing : MonoBehaviour
{
    public float fadeSpeed = 1f; // 控制透明度减淡的速度
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 获取当前颜色
        Color color = spriteRenderer.color;

        // 持续减小 alpha 值
        color.a -= fadeSpeed * Time.deltaTime;

        // 确保 alpha 值不会低于 0
        color.a = Mathf.Clamp01(color.a);

        // 应用颜色
        spriteRenderer.color = color;

        // 当完全透明时销毁对象
        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
