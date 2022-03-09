using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Movement Stats")]
    public float speed = 9f;

    [Header("Attack Stats")]
    public GameObject bullet;
    public float bulletSpeed = 35f;
    public float damage = 10f;
    public float attackSpeed = 1f;
    public float critRate = 10;
    public float critDamage = 50;
}
