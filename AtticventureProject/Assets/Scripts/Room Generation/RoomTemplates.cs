using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomTemplates", menuName = "ScriptableObjects/RoomTemplates", order = 2)]
public class RoomTemplates : ScriptableObject
{
    public GameObject[] upRooms;
    public GameObject[] rightRooms;
    public GameObject[] downRooms;
    public GameObject[] leftRooms;
    public GameObject[] closingRooms;
}
