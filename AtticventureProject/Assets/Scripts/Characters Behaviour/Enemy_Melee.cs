using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Enemy_Melee : MonoBehaviour , IEnemy
{
    public Transform player {get; private set;}
    [SerializeField] private Transform attackPoint;
    [SerializeField] LayerMask playerMask;

    private float nextTimetoAttack = 2f;
    public float attackSpeed = 0.4f;

    public float damage = 10f;
    [SerializeField] private float attackRange = 1f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = player;
    }

    private void Update()
    {
        TurnToPlayer();

        if (Time.time >= nextTimetoAttack)
        {
            AttackPlayer();
            nextTimetoAttack = Time.time + 1 / attackSpeed;
        }
    }

    private void TurnToPlayer()
    {
        Vector2 temp = transform.localScale;
        temp.x = (transform.position.x > player.position.x) ? -1 : 1;
        transform.localScale = temp;
    }

    public void AttackPlayer()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= attackRange)  // Is in Attack Range
            Physics2D.OverlapCircle(attackPoint.position, attackRange, playerMask).gameObject.GetComponent<HealthManager>().TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
