using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    [SerializeField] private Items[] loot;
    [SerializeField] private LootEffects effects;

    public int[] table =
    {
        60, //item A
        30, //item B
        10  //item C
    };

    public int total;
    public int randomNumber;

    void Start()
    {
        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                effects.item = loot[i];
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }

    }
}