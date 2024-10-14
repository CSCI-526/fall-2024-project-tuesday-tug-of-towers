using UnityEngine;

public class TailFlame: MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private SpriteRenderer spriteRenderer; // SpriteRenderer 组件
    [SerializeField] private Sprite[] flameSprites; // 存放火焰图片的数组

    [Header("Attribute")]
    [SerializeField] private float frameDuration = 0.1f; // 每帧持续时间

    private int currentFrame = 0; // 当前帧索引
    private float timer = 0f; // 计时器

    private void Update()
    {
        // 更新计时器
        timer += Time.deltaTime;

        // 如果计时器超过了当前帧持续时间
        if (timer >= frameDuration)
        {
            // 重置计时器
            timer = 0f;

            // 切换到下一个火焰图片
            currentFrame = (currentFrame + 1) % flameSprites.Length;
            spriteRenderer.sprite = flameSprites[currentFrame];
        }
    }
}
