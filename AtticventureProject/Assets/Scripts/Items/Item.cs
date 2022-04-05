using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public GameObject item;
    public Sprite effectDescription;
    public int effect;
}