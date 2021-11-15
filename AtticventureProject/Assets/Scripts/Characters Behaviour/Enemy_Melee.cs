using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform attackPoint;
    public LayerMask playerMask;

    private float nextTimetoAttack = 2f;
    public float attackSpeed = 0.4f;

    public float damage = 10f;
    [SerializeField] private float attackRange = 1f;


    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = player;

        if (Time.time >= nextTimetoAttack)
        {
            Attack();
            nextTimetoAttack = Time.time + 1 / attackSpeed;
        }
    }

    void Attack()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= attackRange)  // Is in Attack Range
            Physics2D.OverlapCircle(gameObject.transform.position, attackRange, playerMask).gameObject.GetComponent<HealthManager>().TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }

}
