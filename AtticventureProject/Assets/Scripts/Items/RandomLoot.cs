using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    [SerializeField] private LootEffects effects;

    public void GenerateRandomItem(ItemPool pool)
    {
        int total = 0;

        foreach (var item in pool.table)
        {
            total += item;
        }

        int randomNumber = Random.Range(0, total);

        for (int i = 0; i < pool.table.Length; i++)
        {
            if (randomNumber <= pool.table[i])
            {
                effects.item = pool.loot[i];
                return;
            }
            else
            {
                randomNumber -= pool.table[i];
            }
        }
    }
}