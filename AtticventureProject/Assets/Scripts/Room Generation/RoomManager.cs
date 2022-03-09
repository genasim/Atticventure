using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> enemyList;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private BoxCollider2D[] doorColliders;
    [SerializeField] private Animator[] doorAnimators;
    [SerializeField] private LootEffects crate;

    private LootEffects effects;

    public bool hasBeenActivated = false;

    private void Awake() {
        effects = new LootEffects();
        effects.AssignPlayerComponents();
    }

    void Update()
    {
        foreach (var enemy in enemyList)
        {
            if (!enemy) enemyList.Remove(enemy);
        }

        if (hasBeenActivated && enemyList.Count == 0)
        {
            foreach (var border in doorColliders)
            {
                border.enabled = false;
            }

            foreach (var animator in doorAnimators)
            {
                animator.SetBool("closeDoor", false);
                animator.SetBool("openDoor", true);
            }

            crate.roomHasBeenCleared = true;
            // effects.PlusHP();
        
            this.enabled = false;
        }
    }

    public void InitiateRoom()
    {
        if (!hasBeenActivated)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                GameObject spawnedEnemy = Instantiate(spawnPoint.GetComponent<EnemySpawnPoints>().enemyToSpawn, spawnPoint.transform.position, transform.rotation, spawnPoint.transform);
                enemyList.Add(spawnedEnemy);
            }

            foreach (var border in doorColliders)
            {
                border.enabled = true;
            }

            foreach (var item in doorAnimators)
            {
                item.SetBool("closeDoor", true);
                item.SetBool("openDoor", false);
            }

            crate.roomHasBeenCleared = false;
            hasBeenActivated = true;
        }
    }
}
