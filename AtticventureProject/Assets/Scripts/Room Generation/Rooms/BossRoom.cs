using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    public class BossRoom : RoomManager
    {
        [SerializeField] private GameObject boss;
        [HideInInspector] public List<GameObject> enemyList;
        private int enemiesCount = 0;
        private int EnemiesCount {
            set {
                enemiesCount = value;
                if (hasBeenActivated && enemiesCount == 0)
                {
                    // foreach (var border in doorColliders)
                    //     border.enabled = false;

                    foreach (var animator in doorAnimators) {
                        animator.SetBool("closeDoor", false);
                        animator.SetBool("openDoor", true);
                    }
                }
            }
        }

        private void Awake() {
            SetUpRoom();
        }

        protected override void InitiateRoom()
        {
            Instantiate(boss, transform.position, Quaternion.identity, transform);

            hasBeenActivated = true;
        }
        
        override protected void OnTriggerEnter2D(Collider2D other) {
            base.OnTriggerEnter2D(other);
            
        }
    }
}
