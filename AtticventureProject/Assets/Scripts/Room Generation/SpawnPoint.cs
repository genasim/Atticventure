using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject door;
        [SerializeField] private BoxCollider2D doorColider;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private RoomManager roomManager;
        public MapTile mapTile;


        [Header("")]
        [SerializeField] private int openingDiraction;
        // 0 --> need down  door
        // 1 --> need left  door
        // 2 --> need up    door
        // 3 --> need right door
        
        private RoomTemplates templates;
        [HideInInspector] public bool spawned = false;  // Don't remove

        void Awake()
        {
            templates = RoomGenerator.Instance.Templates;
        }

        public void ClosingRoomSpawn() {
            if (spawned) return;

            switch (openingDiraction)
            {
                case 0:
                    Instantiate(templates.closingRooms[0], transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(templates.closingRooms[1], transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(templates.closingRooms[2], transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(templates.closingRooms[3], transform.position, Quaternion.identity);
                    break;
            }

            spawned = true;
        }

        public void SpawnRoom()
        {
            if (spawned) return;

            switch (openingDiraction)
            {
                case 0: {
                        int rand = UnityEngine.Random.Range(0, templates.downRooms.Length);
                        Instantiate(templates.downRooms[rand], transform.position, Quaternion.identity);
                        break;
                    }
                case 1: {
                        int rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                        Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
                        break;
                    }
                case 2: {
                        int rand = UnityEngine.Random.Range(0, templates.upRooms.Length);
                        Instantiate(templates.upRooms[rand], transform.position, Quaternion.identity);
                        break;
                    }
                case 3: {
                        int rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                        Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
                        break;
                    }
            }

            spawned = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out SpawnPoint sp))
            {
                if (sp.spawned == false && spawned == false) {    
                    CleanDestroy();
                }
            }

            if (other.gameObject.TryGetComponent(out RoomManager rm)) {
                if (!CheckCanGoToNextRoom(rm.roomSpawnPoints))
                    CleanDestroy();

                switch ((int)rm.state) {
                    case 2:
                        door.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                        break;
                    case 4:
                        door.GetComponentInChildren<SpriteRenderer>().color = new Color(1, .47f, .47f, 1);
                        
                        roomManager.doorColliders.Remove(this.doorColider);
                        roomManager.doorAnimators.Remove(this.doorAnimator);
                        
                        var bosLadderRoom = (BossLadderRoom)rm;
                        doorColider.enabled = true;
                        doorAnimator.SetBool("closeDoor", true);
                        doorAnimator.SetBool("openDoor", false);

                        bosLadderRoom.entranceCol = this.doorColider;
                        bosLadderRoom.entranceAnim = this.doorAnimator;
                        break;
                }
            }
        }

        private bool CheckCanGoToNextRoom(List<SpawnPoint> spawnPoints) {
            foreach (var point in spawnPoints)
            {
                if (MathF.Abs(this.openingDiraction - point.openingDiraction) == 2)
                    return true;
            }
            return false;
        }

        public void CleanDestroy() {
            roomManager.roomSpawnPoints.Remove(this);
                //  Destroy Door
            roomManager.doorColliders.Remove(this.doorColider);
            this.doorColider.enabled = true;
            roomManager.doorAnimators.Remove(this.doorAnimator);
            Destroy(this.door);
                //  Destroy Spawn Point
            Destroy(gameObject);
        }
    }
}