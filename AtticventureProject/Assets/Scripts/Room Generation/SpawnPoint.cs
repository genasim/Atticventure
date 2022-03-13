using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int openingDiraction;
    // 1 --> need down  door
    // 2 --> need left  door
    // 3 --> need up    door
    // 4 --> need right door
    
    private RoomGenerator templates;
    public bool spawned {get; private set;} = false;

    void Start()
    {
        templates = GameObject.FindObjectOfType<RoomGenerator>();
        Invoke("Spawn", .1f);
    }

    public void ClosingRoomSpawn() {
        if (openingDiraction == 1)
        {
            Instantiate(templates.closingRooms[0], transform.position, Quaternion.identity);
        }
        else if (openingDiraction == 2)
        {
            Instantiate(templates.closingRooms[1], transform.position, Quaternion.identity);
        }
        else if (openingDiraction == 3)
        {
            Instantiate(templates.closingRooms[2], transform.position, Quaternion.identity);
        }
        else if (openingDiraction == 4)
        {
            Instantiate(templates.closingRooms[3], transform.position, Quaternion.identity);
        }

        spawned = true;
        Destroy(gameObject);
    }

    void Spawn()
    {
        if (spawned == false) {
            if (openingDiraction == 1)
            {
                int rand = UnityEngine.Random.Range(0, templates.downRooms.Length);
                Instantiate(templates.downRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 2)
            {
                int rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 3)
            {
                int rand = UnityEngine.Random.Range(0, templates.upRooms.Length);
                Instantiate(templates.upRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDiraction == 4)
            {
                int rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }

            spawned = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            try {
                if (other.GetComponent<SpawnPoint>().spawned == false && spawned == false) {
                    Destroy(gameObject);
                }
            } catch (NullReferenceException) {}
        }
    }
}
