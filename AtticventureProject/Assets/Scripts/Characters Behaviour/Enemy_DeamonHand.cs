using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager), typeof(BoxCollider2D))]
public class Enemy_DeamonHand : MonoBehaviour
{
    private new Collider2D collider;
    private Transform player;
    private Transform thisEnemy;

    [SerializeField] private Transform attackPoint;
    private LayerMask playerMask;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackRange = .8f;


    private void Awake() {
        collider = GetComponent<BoxCollider2D>();
        thisEnemy = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerMask = LayerMask.GetMask("Player");
    }

    internal void Grab()    // Triggered by AnimEvent_DemHand.cs
    {
        try {
        Physics2D.OverlapCircle(gameObject.transform.position, attackRange, playerMask)
            .gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        } catch {}
    }

    internal void TeleportToPlayer()    // Triggered by AnimEvent_DemHand.cs
    {
        if (player) {
            thisEnemy.position = player.position;
        }
    }

    internal void ActivateHitBox(bool isActive)    // Triggered by AnimEvent_DemHand.cs
    {
        collider.enabled = isActive;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
