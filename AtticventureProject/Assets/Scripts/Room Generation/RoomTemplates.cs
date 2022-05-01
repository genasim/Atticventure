using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    [CreateAssetMenu(fileName = "RoomTemplates", menuName = "ScriptableObjects/RoomTemplates", order = 2)]
    public class RoomTemplates : ScriptableObject
    {
        [Header("Floor Generation")]
        public GameObject[] upRooms;
        public GameObject[] rightRooms;
        public GameObject[] downRooms;
        public GameObject[] leftRooms;
        public GameObject[] closingRooms;
        public GameObject ItemRoom;
        public GameObject BossLadderRoom;

        [Header("Map tiles")]
        public GameObject mapTile;
        public Sprite currentRoom;
        public Sprite visitedRoom;
        public Sprite unvisitedRoom;
    }
}