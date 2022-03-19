using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : MonoBehaviour
{
    public RoomTemplates templates;
    public List<GameObject> rooms = new List<GameObject>();
    private int roomCount;
    [SerializeField] private bool mazeClosed;

    private float waitTime = 3;
    private bool spawnedBoss = false;
    public GameObject boss;
    

    private void Update() {
        if (waitTime <= 0 && !mazeClosed) {
            Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
            rooms[rooms.Count - 1].GetComponentInChildren<RoomManager>().state = RoomState.Boss;

            //  Destroy remaining SpawnPoints ?

            mazeClosed = true;
        } else {
            waitTime -= Time.deltaTime;
        }
    }
}

