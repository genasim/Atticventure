using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private BoxCollider2D[] doorColliders;
    [SerializeField] private Animator[] doorAnimatiors;
    
    public bool canOpenChest = false;
    public bool hasBeenActivated = false;

    void Update()
    {
        foreach (var item in enemyList)
        {
            if (!item) enemyList.Remove(item);
        }

        if (enemyList.Count == 0)
        {
            foreach (var border in doorColliders)
            {
                border.enabled = false;
            }

            foreach (var item in doorAnimatiors)
            {
                item.SetBool("closeDoor", false);
                item.SetBool("openDoor", true);
            }

            canOpenChest = true;
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

            foreach (var item in doorAnimatiors)
            {
                item.SetBool("closeDoor", true);
                item.SetBool("openDoor", false);
            }

            hasBeenActivated = true;
        }
    }
}
