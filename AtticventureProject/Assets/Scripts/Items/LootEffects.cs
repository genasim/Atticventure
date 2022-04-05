using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootEffects : MonoBehaviour
{
    private PlayerData playerData;
    private HealthManager playerHealth;

    private int effect;
    public Item item;

    private bool interactButton = false;
    private bool hasSpawned = false;
    private bool canBeOpen = false;
    public bool roomHasBeenCleared = false;

    [SerializeField] private Animator itemAnim;
    [SerializeField] private Animator crateAnim;
    [SerializeField] private Transform check;
    [SerializeField] private LayerMask player;

    private GameObject Player;

    private InputAction interactKeyboard;
    private InputAction interactGamepad;

    private void Awake()
    {
        AssignPlayerComponents();
        playerData = PlayerManager.Instance.data;
        interactKeyboard = PlayerManager.Instance.inputKeyboard.Player.Interact;
        interactGamepad = PlayerManager.Instance.inputGamepad.Player.Interact;
    }

    public void AssignPlayerComponents() {
        this.playerHealth = PlayerManager.Instance.Player.GetComponent<HealthManager>();
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
        canBeOpen = Physics2D.OverlapCircle(check.position, 1, player) && interactButton & roomHasBeenCleared;
        if (item && hasSpawned == false && canBeOpen)
        {
            effect = item.effect;
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
        playerData.AttackSpeed += .3f;
        Debug.Log("AttackSpeed+");
    }
    public void MinusAtkSpd()
    {
        playerData.AttackSpeed -= .3f;
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
        playerHealth.currentHealth += 10;
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
        playerHealth.currentHealth += 10;
        if (Health.numberOfHearths <= 7) Health.numberOfHearths++;
        Debug.Log("MaxHealth+");
    }
    public void MinusMaxHP()
    {
        if (playerHealth.maxHealth >= 60) playerHealth.maxHealth -= 20;
        if (Health.numberOfHearths >= 3) Health.numberOfHearths--;
        if (playerHealth.currentHealth >= 50) playerHealth.currentHealth -= 10;
        Debug.Log("MaxHealth-");
    }
}
