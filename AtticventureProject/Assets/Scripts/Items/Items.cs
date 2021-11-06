using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Items : ScriptableObject
{
    public GameObject item;
    public Sprite effectDescription;
    public int effect;
}