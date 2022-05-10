using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemPools", menuName = "ScriptableObjects/ItemPools", order = 0)]
public class ItemPools : ScriptableObject {
    public ItemPool RegularRoom;
    public ItemPool ItemRoom;
}

[System.Serializable] public struct ItemPool
{
    [SerializeField] private string Name;
    public Item[] loot;
    public int[] table;
}
