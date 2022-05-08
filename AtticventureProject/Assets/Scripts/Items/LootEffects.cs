using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MazeGeneration;

public class LootEffects : MonoBehaviour, IInteractable
{
    private PlayerData playerData;
    internal HealthManager playerHealth;

    public Item item;

    private bool interactButton = false;
    private bool hasSpawned = false;
    private bool canBeOpen = false;
    public bool roomHasBeenCleared = false;

    [SerializeField] private Animator itemAnim;
    [SerializeField] private Animator crateAnim;
    [SerializeField] private Transform check;
    [SerializeField] private LayerMask player;

    private InputAction interactKeyboard;
    private InputAction interactGamepad;

    private void Awake()
    {
        playerHealth = PlayerManager.Instance.Player.GetComponent<HealthManager>();
        playerData = PlayerManager.Instance.data;
        interactKeyboard = PlayerManager.Instance.inputKeyboard.Player.Interact;
        interactGamepad = PlayerManager.Instance.inputGamepad.Player.Interact;
        GenerateItem(RoomGenerator.Instance.Pools.RegularRoom);
    }

    public void GenerateItem(ItemPool newPool)
    {
        var loot = GetComponent<RandomLoot>();
        loot.GenerateRandomItem(newPool);
    }

    private void OnEnable() {
        interactKeyboard.performed += _ => interactButton = true;
        interactGamepad.performed += _ => interactButton = true;
        interactKeyboard.canceled += _ => interactButton = false;
        interactGamepad.canceled += _ => interactButton = false;
    }

    private void OnDisable() {
        interactKeyboard.performed -= _ => interactButton = true;
        interactGamepad.performed -= _ => interactButton = true;
        interactKeyboard.canceled -= _ => interactButton = false;
        interactGamepad.canceled -= _ => interactButton = false;
    }

    void Update()
    {
        canBeOpen = Physics2D.OverlapCircle(check.position, 2, player) && interactButton && roomHasBeenCleared;
        if (!(item && hasSpawned == false && canBeOpen)) return;
        Interact();
    }

    public void Interact()
    {
        SetEffect();
        Instantiate(item.item, transform.position, Quaternion.identity, transform.GetChild(0));
        itemAnim.SetBool("HasSpawned", true);
        crateAnim.SetBool("ItemSpawned", true);
        hasSpawned = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(check.position, 2);
    }

    private void SetEffect()
    {
        switch (item.effect)
        {
            case 1:
                PlusAtkDmg();
                break;
            case -1:
                MinusAtkDmg();
                break;
            case 2:
                PlusAtkSpd();
                break;
            case -2:
                MinusAtkSpd();
                break;
            case 3:
                PlusBulletSpeed();
                break;
            case -3:
                MinusBulletSpeed();
                break;
            case 4:
                PlusCritDamage();
                break;
            case -4:
                MinusCritDamage();
                break;
            case 5:
                PlusCritRate();
                break;
            case -5:
                MinusCritRate();
                break;
            case 6:
                PlusHP();
                break;
            case -6:
                MinusHP();
                break;
            case 7:
                PlusMaxHP();
                break;
            case -7:
                MinusMaxHP();
                break;
            case 8:
                PlusMvtSpd();
                break;
            case -8:
                MinusMvtSpd();
                break;
            default:
                Debug.Log("Invalid Effect");
                break;

        }
    }
    public void PlusAtkSpd()
    {
        playerData.AttackSpeed += .3f;
        Debug.Log("AttackSpeed+");
    }
    public void MinusAtkSpd()
    {
        playerData.AttackSpeed -= .2f;
        Debug.Log("AttackSpeed-");
    }
    public void PlusMvtSpd()
    {
        playerData.Speed += 1;
        Debug.Log("MovSpeed+");
    }
    public void MinusMvtSpd()
    {
        playerData.Speed -= 1;
        Debug.Log("MovSpeed-");
    }
    public void PlusAtkDmg()
    {
        playerData.Damage += 5;
        Debug.Log("Damage+");
    }
    public void MinusAtkDmg()
    {
        playerData.Damage -= 5;
        Debug.Log("Damage-");
    }
    public void PlusBulletSpeed()
    {
        playerData.BulletSpeed += 1;
        Debug.Log("BulletSpeed+");
    }
    public void MinusBulletSpeed()
    {
        playerData.BulletSpeed -= 1;
        Debug.Log("BulletSpeed-");
    }
    public void PlusHP()
    {
        if (playerHealth.currentHealth < playerHealth.maxHealth) playerHealth.currentHealth += 10;
        Debug.Log("Health+");
    }
    public void MinusHP()
    {
        if (playerHealth.currentHealth >= 50) playerHealth.currentHealth -= 10;
        Debug.Log("Health-");
    }
    public void PlusCritRate()
    {
        playerData.CritRate += 10;
        Debug.Log("CritRate+");
    }
    public void MinusCritRate()
    {
        playerData.CritRate -= 10;
        Debug.Log("CritRate-");
    }
    public void PlusCritDamage()
    {
        playerData.CritDamage += 5;
        Debug.Log("CritDmg+");
    }
    public void MinusCritDamage()
    {
        playerData.CritDamage -= 5;
        Debug.Log("CritDmg-");
    }
    public void PlusMaxHP()
    {
        if (playerHealth.maxHealth <= 140) playerHealth.maxHealth += 20;
        playerHealth.currentHealth += 20;
        if (Health.numberOfHearths <= 7) Health.numberOfHearths++;
        Debug.Log("MaxHealth+");
    }
    public void MinusMaxHP()
    {
        if (playerHealth.maxHealth >= 60) playerHealth.maxHealth -= 20;
        if (Health.numberOfHearths >= 3) Health.numberOfHearths--;
        if (playerHealth.currentHealth >= 50) playerHealth.currentHealth -= 20;
        Debug.Log("MaxHealth-");
    }
}
