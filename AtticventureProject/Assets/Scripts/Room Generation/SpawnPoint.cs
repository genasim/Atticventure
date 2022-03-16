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
    public bool spawned = false;

    void Start()
    {
        generator = GameObject.FindObjectOfType<RoomGenerator>();
        Invoke("Spawn", .2f);
    }

    public void ClosingRoomSpawn() {
        if (openingDiraction == 0)
        {
            Instantiate(generator.closingRooms[0], transform.position, Quaternion.identity);
        }
        else if (openingDiraction == 1)
        {
            Instantiate(generator.closingRooms[1], transform.position, Quaternion.identity);
        }
        else if (openingDiraction == 2)
        {
            Instantiate(generator.closingRooms[2], transform.position, Quaternion.identity);
        }
        else if (openingDiraction == 3)
        {
            Instantiate(generator.closingRooms[3], transform.position, Quaternion.identity);
        }

        this.enabled = false;
        // spawned = true;
        // Destroy(gameObject);
    }

    void Spawn()
    {
        if (spawned == false) {
            if (openingDiraction == 0)
            {
                int rand = UnityEngine.Random.Range(0, generator.downRooms.Length);
                Instantiate(generator.downRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 1)
            {
                int rand = UnityEngine.Random.Range(0, generator.leftRooms.Length);
                Instantiate(generator.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 2)
            {
                int rand = UnityEngine.Random.Range(0, generator.upRooms.Length);
                Instantiate(generator.upRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 3)
            {
                int rand = UnityEngine.Random.Range(0, generator.rightRooms.Length);
                Instantiate(generator.rightRooms[rand], transform.position, Quaternion.identity);
            }

            this.enabled = false;
            // spawned = true;
            // Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            try {
                if (other.GetComponent<SpawnPoint>().spawned == false && spawned == false) {    
                    DestroyDoor();
                    CleanDestroy();
                }
            } catch (NullReferenceException) {}
        }

        if (other.gameObject.TryGetComponent(out Destroyer d)) {
            if (!checkCanGoToNextRoom(other.gameObject.GetComponent<Destroyer>().spawnPoints)) {
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

    public void CleanDestroy() {
        destroyer.spawnPoints.Remove(this);
        Destroy(gameObject);
    }

    public void DestroyDoor() {
        roomManager.doorColliders.Remove(doorColider);
        doorColider.enabled = true;
        roomManager.doorAnimators.Remove(doorAnimator);
        Destroy(door);
        this.spawned = true;
    }
}
