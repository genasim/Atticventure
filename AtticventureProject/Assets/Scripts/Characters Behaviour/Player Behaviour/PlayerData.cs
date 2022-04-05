using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private float speed = 9f;

    [Header("Attack Stats")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 35f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float critRate = 15;
    [SerializeField] private float critDamage = 50;

    public float Speed { get => speed;
        set { if (speed + value >= 6) speed += value; }
    }

    public GameObject Bullet { get => bullet; }

    public float BulletSpeed { get => bulletSpeed;
        set { if (bulletSpeed + value >= 25) bulletSpeed += value; }
    }

    public float Damage { get => damage;
        set { if (damage + value >= 5) damage += value; }
    }

    public float AttackSpeed { get => attackSpeed; 
        set { if (attackSpeed + value >= .4f) attackSpeed += value; }
    }

    public float CritRate { get => critRate; 
        set { if (critRate + value >= 0 && critRate + value <= 100) critRate += value; }
    }

    public float CritDamage { get => critDamage; 
        set { if (critDamage + value >= 0) critDamage += value; }
    }

    public void Reset() {
        speed = 9f;
        bulletSpeed = 35f;
        damage = 10f;
        attackSpeed = 1f;
        critRate = 15;
        critDamage = 50;
    }
}
