using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    internal float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent(out HealthManager hm))
            hm.TakeDamage(damage);
        
        if (collisionObject.TryGetComponent(out Rigidbody2D rb2d))
            rb2d.velocity = Vector2.zero;

        Destroy(gameObject);
    }
}
