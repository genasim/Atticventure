using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    public RoomState state = RoomState.Regular;
    [HideInInspector] public List<GameObject> enemyList;
    [SerializeField] private List<GameObject> spawnPoints;
    public List<BoxCollider2D> doorColliders;
    public List<Animator> doorAnimators;
    [SerializeField] private LootEffects crate;

    private LootEffects effects;

    public bool hasBeenActivated = false;

    private CinemachineVirtualCamera camCinamachine;

    private void Awake() {
        this.effects = new LootEffects();
        this.effects.AssignPlayerComponents();
        
        RoomGenerator generator = GameObject.FindObjectOfType<RoomGenerator>();
        generator.rooms.Add(transform.parent.gameObject);

        this.state = RoomState.Regular;

        camCinamachine = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
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
            effects.PlusHP();
            
            this.enabled = false;
        }
    }


    public void InitiateRoom()
    {
        if (!hasBeenActivated)
        {
            switch (this.state) {
                case RoomState.Regular:
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
                    break;

                case RoomState.Boss:

                    //  Remove obstacles

                    //  Spawn Boss
                
                    break;

                case RoomState.ItemRoom:

                    //  Chest with better loot

                    //  No Enemies

                    //  Remove Obstacles

                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out PlayerMove player)) {
            camCinamachine.LookAt = transform.parent;
            camCinamachine.Follow = transform.parent;

            AStarGridGraph.UpdateGraph(centre: transform.position);
            PlayerShoot.currentRoom = this;

            if (!hasBeenActivated)
                InitiateRoom();
        }
    }
}

public enum RoomState {
    Regular = 1,
    Boss = 2,
    Start = 3,
    ItemRoom = 4
};
