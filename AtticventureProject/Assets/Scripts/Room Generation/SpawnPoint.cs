using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private BoxCollider2D doorColider;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] Destroyer destroyer;

    [Header("")]
    [SerializeField] private int openingDiraction;
    // 0 --> need down  door
    // 1 --> need left  door
    // 2 --> need up    door
    // 3 --> need right door
    
    private RoomGenerator generator;
    private RoomTemplates templates;
    public bool spawned = false;
    private bool closeDoor = false;

    // public delegate void spawnBoss();
    // public static event spawnBoss SpawnBoss;

    // public delegate void Room(Vector2 position);
    // public static event Room canSpawnRoom;

    void Start()
    {
        generator = RoomGenerator.Instance;
        templates = generator.templates;

        if (generator.rooms.Count < 6)
            Invoke("SpawnRoom", .05f);
        else if (!spawned) {
                Invoke("ClosingRoomSpawn", 1);
                // Invoke("DestroyDoor", 1f);
            }

    }

    // private void Start() {
    //     canSpawnRoom(transform.position);
    // }

    public void ClosingRoomSpawn() {
        if (!spawned) {
            if (openingDiraction == 0)
            {
                Instantiate(templates.closingRooms[0], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 1)
            {
                Instantiate(templates.closingRooms[1], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 2)
            {
                Instantiate(templates.closingRooms[2], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 3)
            {
                Instantiate(templates.closingRooms[3], transform.position, Quaternion.identity);
            }

            spawned = true;
            // SpawnBoss();
        }
    }

    public void SpawnRoom()
    {
        if (!spawned) {
            if (openingDiraction == 0)
            {
                int rand = UnityEngine.Random.Range(0, templates.downRooms.Length);
                Instantiate(templates.downRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 1)
            {
                int rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 2)
            {
                int rand = UnityEngine.Random.Range(0, templates.upRooms.Length);
                Instantiate(templates.upRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 3)
            {
                int rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }

            spawned = true;
            // this.enabled = false;
            // Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out SpawnPoint sp))
        {
            try {
                if (sp.spawned == false && spawned == false) {    
                    DestroyDoor();
                    CleanDestroy();
                }
            } catch (NullReferenceException) {}
        }

        if (other.gameObject.TryGetComponent(out Destroyer d)) {
            if (!checkCanGoToNextRoom(d.spawnPoints)) {
                DestroyDoor();
                CleanDestroy();
            }
        }
    }

    private bool checkCanGoToNextRoom(List<SpawnPoint> spawnPoints) {
        foreach (var point in spawnPoints)
        {
            if (MathF.Abs(this.openingDiraction - point.openingDiraction) == 2)
                return true;
        }
        return false;
    }

    private void CleanDestroy() {
        destroyer.spawnPoints.Remove(this);
        Destroy(gameObject);
    }

    private void DestroyDoor() {
        roomManager.doorColliders.Remove(this.doorColider);
        this.doorColider.enabled = true;
        roomManager.doorAnimators.Remove(this.doorAnimator);
        Destroy(this.door);
        // this.spawned = true;
    }
}
