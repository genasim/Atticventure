using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    internal float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.GetComponent<HealthManager>())
            collisionObject.GetComponent<HealthManager>().TakeDamage(damage);

        if (collisionObject.GetComponent<Rigidbody2D>())
            collisionObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (collisionObject.GetComponent<_TEMPHealth>())
            collisionObject.GetComponent<_TEMPHealth>().Health();

        Destroy(gameObject);
    }
}
