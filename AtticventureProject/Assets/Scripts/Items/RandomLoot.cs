using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    public Items[] loot;
    LootEffects crate;

    public int[] table =
    {
        60, //item A
        30, //item B
        10  //item C
    };

    public int total;
    public int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        crate = GameObject.FindGameObjectWithTag("Crate").GetComponent<LootEffects>();
        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                crate.item = loot[i];
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }

    }
}