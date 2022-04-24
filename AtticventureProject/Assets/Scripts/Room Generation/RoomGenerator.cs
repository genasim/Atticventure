using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : Singleton<RoomGenerator>
{
    [SerializeField] private RoomTemplates templates;
    public RoomTemplates Templates { get => templates; }

    [SerializeField] private ItemPools pools;
    public ItemPools Pools { get => pools; }

    public List<GameObject> rooms = new List<GameObject>();

    [SerializeField] private GameObject boss;
    
    void Start()
    {
        StartCoroutine(GenerateMaze());
    }

    private IEnumerator GenerateMaze()
    {
        int round = 0;
        while (round < 1) {
            for (int i = rooms.Count - 1; i >= 0 ; i--)
            {
                var spawnPoints = rooms[i].GetComponentInChildren<RoomManager>().roomSpawnPoints;
                for (int j = spawnPoints.Count - 1; j >= 0 ; j--)
                    spawnPoints[j].SpawnRoom();
            }
            yield return new WaitForSeconds(.5f);
            round++;
        }
        for (int i = rooms.Count - 1; i >= 0 ; i--)
        {
            var spawnPoints = rooms[i].GetComponentInChildren<RoomManager>().roomSpawnPoints;
            for (int j = spawnPoints.Count - 1; j >= 0 ; j--)
                spawnPoints[j].ClosingRoomSpawn();
        }

        //  ASSIGN ITEM ROOM
        var itemRoom = rooms[UnityEngine.Random.Range(1, rooms.Count - 2)];
        itemRoom.GetComponentInChildren<RoomManager>().State = RoomState.ItemRoom;

        //  ASSIGN BOSS ROOM
        var bossRoom = rooms[rooms.Count - 1];
        Instantiate(boss, bossRoom.transform.position, Quaternion.identity);
        bossRoom.GetComponentInChildren<RoomManager>().State = RoomState.Boss;
    }
}

