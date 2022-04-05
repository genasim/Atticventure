using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : Singleton<RoomGenerator>
{
    public RoomTemplates templates;
    public List<GameObject> rooms = new List<GameObject>();
    private int roomCount;
    [SerializeField] private bool mazeClosed;

    private float waitTime = 3;
    private bool spawnedBoss = false;
    [SerializeField] private GameObject boss;
    
    // private void Start() {
    //     // SpawnRooms(roomCount);
    //     StartCoroutine("SpawnRooms");
    // }

    // private IEnumerator SpawnRooms() {
    //     // roomCount = rooms.Count;
    //     // Debug.Log(rooms.Count);
    //     var roomsArray = rooms.ToArray(); 
    //     while (roomsArray.Length < 7) {
    //         foreach (var room in roomsArray)
    //         {
    //             foreach (var spawnPoint in room.GetComponentsInChildren<SpawnPoint>())
    //             {
    //                 spawnPoint.SpawnRoom();
    //             }
    //         }

    //         // yield return new WaitForEndOfFrame();
    //         IEnumerable co = SpawnRooms();
    //         StartCoroutine(co.GetEnumerator());
    //     }
    // }
    

    private void Update() {
        if (waitTime <= 0 && !mazeClosed) {
            var bossRoom = rooms[rooms.Count - 1];
            Instantiate(boss, bossRoom.transform.position, Quaternion.identity);
            bossRoom.GetComponentInChildren<RoomManager>().state = RoomState.Boss;


            mazeClosed = true;
        } else {
            waitTime -= Time.deltaTime;
        }
    }
}

