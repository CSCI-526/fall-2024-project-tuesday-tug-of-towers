using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] protected float bulletSpeed = 5f;
    [SerializeField] protected float bulletDamage = 0.25f;


    protected Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<EnemyStats>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}