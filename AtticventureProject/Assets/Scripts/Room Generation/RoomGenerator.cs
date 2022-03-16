using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : MonoBehaviour
{
    public GameObject[] upRooms;
    public GameObject[] rightRooms;
    public GameObject[] downRooms;
    public GameObject[] leftRooms;
    public GameObject[] closingRooms;

    public List<GameObject> rooms;

    private float waitTime = 3;
    private bool spawnedBoss = false;
    public GameObject boss;

    private int roomCount = 0;

    private void Update() {
        // if (rooms.Count >= 10) {
        //     foreach (var room in rooms) {
        //         foreach (var spawnPoint in room.gameObject.GetComponentsInChildren<SpawnPoint>())
        //         {
        //             if (!spawnPoint.spawned)
        //                 spawnPoint.ClosingRoomSpawn();
        //         }
        //     }
        // }

        if (waitTime <= 0 && !spawnedBoss) {
            Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
            rooms[rooms.Count - 1].GetComponentInChildren<RoomManager>().state = RoomState.Boss;

            //  Destroy remaining SpawnPoints ?

            spawnedBoss = true;
        } else {
            waitTime -= Time.deltaTime;
        }
    }
}
