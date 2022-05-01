using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration 
{
    public class RegularRoom : RoomManager
    {
        [SerializeField] private List<GameObject> spawnPoints;
        [SerializeField] private LootEffects crate;
        [HideInInspector] public List<GameObject> enemyList;
        private int enemiesCount = 0;
        private int EnemiesCount {
            set {
                enemiesCount = value;
                if (hasBeenActivated && enemiesCount == 0)
                {
                    foreach (var border in doorColliders)
                        border.enabled = false;

                    foreach (var animator in doorAnimators) {
                        animator.SetBool("closeDoor", false);
                        animator.SetBool("openDoor", true);
                    }

                    crate.roomHasBeenCleared = true;
                    crate.PlusHP();
                }
            }
        }

        private void Awake() {
            SetUpRoom();
        }

        override protected void InitiateRoom() {
            if (spawnPoints.Count == 0) {
                EnemiesCount = 0;
                return;
            }

            foreach (var spawnPoint in spawnPoints) {
                GameObject spawnedEnemy = Instantiate(spawnPoint.GetComponent<EnemySpawnPoints>().enemyToSpawn, 
                                                      spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
                enemyList.Add(spawnedEnemy);
            }

            foreach (var border in doorColliders) {
                border.enabled = true;
            }

            foreach (var item in doorAnimators) {
                item.SetBool("closeDoor", true);
                item.SetBool("openDoor", false);
            }

            crate.roomHasBeenCleared = false;
            hasBeenActivated = true;
        }

       void Update()
        {
            if (enemyList.Count == 0) return;
            foreach (var enemy in enemyList)
            {
                if (!enemy) enemyList.Remove(enemy);
                EnemiesCount = enemyList.Count;
            }
        }

        override protected void OnTriggerEnter2D(Collider2D other) {
            base.OnTriggerEnter2D(other);
            if (other.TryGetComponent<PlayerShoot>(out PlayerShoot player)) {
                PlayerShoot.currentRoom = this;
            }
        }
    }
}
