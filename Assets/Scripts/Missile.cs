using UnityEngine;

public class Missile : Bullet
{
    [Header("Missile Attributes")]
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float acceleration = 1f;  // 加速度

    private float currentSpeed;

    private void Start()
    {
        // 初始化当前速度为导弹的初始速度
        currentSpeed = bulletSpeed;

        // 如果目标存在，初始化时就朝向目标
        if (target != null)
        {
            SetInitialRotation();
        }
    }

    private void FixedUpdate()
    {
        if (!target) return;

        // 计算朝向目标的方向
        Vector2 direction = (target.position - transform.position).normalized;

        // 计算当前角度和目标方向之间的角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = rb.rotation;

        // 逐渐旋转导弹，达到平滑转向效果
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, angle, rotationSpeed * Time.fixedDeltaTime);
        rb.rotation = newAngle;

        // 增加速度
        currentSpeed += acceleration * Time.fixedDeltaTime;

        // 根据新的方向设置速度
        rb.velocity = rb.transform.right * currentSpeed;
    }

    private void SetInitialRotation()
    {
        // 计算导弹初始位置和目标之间的方向
        Vector2 direction = (target.position - transform.position).normalized;

        // 计算朝向目标的初始角度
        float initialAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 设置 Rigidbody2D 和 Transform 的初始旋转角度
        rb.rotation = initialAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, initialAngle);
    }
}
