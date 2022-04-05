using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomTemplates", menuName = "ScriptableObjects/RoomTemplates", order = 2)]
public class RoomTemplates : ScriptableObject
{
    [Header("Floor Generation")]
    public GameObject[] upRooms;
    public GameObject[] rightRooms;
    public GameObject[] downRooms;
    public GameObject[] leftRooms;
    public GameObject[] closingRooms;

    [Header("Map tiles")]
    public GameObject mapTile;
    public Sprite currentRoom;
    public Sprite visitedRoom;
    public Sprite unvisitedRoom;
}
