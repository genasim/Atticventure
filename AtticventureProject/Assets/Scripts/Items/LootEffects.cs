using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEffects : MonoBehaviour
{
    Player_Movement playerMovement;
    Shooting playerWeapon;
    HealthManager playerHealth;

    private int effect;
    public Items item;
    private bool hasSpawned = false;
    private bool canBeOpen = false;
    public Animator itemAnim;
    public Animator crateAnim;
    public Transform check;
    public LayerMask player;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
    }

    void Update()
    {
        canBeOpen = Physics2D.OverlapCircle(check.position, 1, player) && Input.GetKeyDown(KeyCode.E);
        if (item && hasSpawned == false && canBeOpen)
        {
            effect = item.effect;
            Debug.Log(effect);
            SetEffect();
            Instantiate(item.item, transform.position, Quaternion.identity, transform.GetChild(0));
            itemAnim.SetBool("HasSpawned", true);
            crateAnim.SetBool("ItemSpawned", true);
            hasSpawned = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(check.position, 1);
    }

    public void SetEffect()
    {
        switch (effect)
        {
            case 1:
                PlusAtkSpd();
                break;
            case 2:
                MinusAtkSpd();
                break;
            case 3:
                PlusMvtSpd();
                break;
            case 4:
                MinusMvtSpd();
                break;
            case 5:
                PlusAtkDmg();
                break;
            case 6:
                MinusAtkDmg();
                break;
            case 7:
                PlusBulletSpeed();
                break;
            case 8:
                MinusBulletSpeed();
                break;
            case 9:
                PlusHP();
                break;
            case 10:
                MinusHP();
                break;
            case 11:
                PlusCritDamage();
                break;
            case 12:
                MinusCritDamage();
                break;
            case 13:
                PlusCritRate();
                break;
            case 14:
                MinusCritRate();
                break;
            case 15:
                PlusMaxHP();
                break;
            case 16:
                MinusMaxHP();
                break;
            default:
                Debug.Log("Invalid Effect");
                break;

        }
    }
    public void PlusAtkSpd()
    {
        playerWeapon.attackSpeed += 1;
        Debug.Log("It worked");
    }
    public void MinusAtkSpd()
    {
        if(playerWeapon.currentAttackSpeed > playerWeapon.attackSpeed)
        {
            playerWeapon.attackSpeed -= 1;
            Debug.Log("It worked");
        }
    }
    public void PlusMvtSpd()
    {
        playerMovement.speed += 1;
        Debug.Log("It worked");
    }
    public void MinusMvtSpd()
    {
        playerMovement.speed -= 1;
        Debug.Log("It worked");
    }
    public void PlusAtkDmg()
    {
        playerWeapon.damage += 1;
        Debug.Log("It worked");
    }
    public void MinusAtkDmg()
    {
        playerWeapon.damage -= 1;
        Debug.Log("It worked");
    }
    public void PlusBulletSpeed()
    {
        playerWeapon.bulletSpeed += 1;
        Debug.Log("It worked");
    }
    public void MinusBulletSpeed()
    {
        playerWeapon.bulletSpeed -= 1;
        Debug.Log("It worked");
    }
    public void PlusHP()
    {
        playerHealth.currentHealth += 5;
        Debug.Log("It worked");
    }
    public void MinusHP()
    {
        playerHealth.currentHealth -= 5;
        Debug.Log("It worked");
    }
    public void PlusCritRate()
    {
        playerWeapon.critRate += 5;
        Debug.Log("It worked");
    }
    public void MinusCritRate()
    {
        playerWeapon.critRate -= 5;
        Debug.Log("It worked");
    }
    public void PlusCritDamage()
    {
        playerWeapon.critDamage += 5;
        Debug.Log("It worked");
    }
    public void MinusCritDamage()
    {
        playerWeapon.critDamage -= 5;
        Debug.Log("It worked");
    }
    public void PlusMaxHP()
    {
        playerHealth.maxHealth += 10;
        playerHealth.currentHealth += 10;
        Debug.Log("It worked");
    }
    public void MinusMaxHP()
    {
        playerHealth.maxHealth -= 10;
        playerHealth.currentHealth -= 10;
        Debug.Log("It worked");
    }
}
