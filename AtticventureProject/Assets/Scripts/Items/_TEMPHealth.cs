using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TEMPHealth : MonoBehaviour
{
    [SerializeField] private LootEffects effects;

    public void Health()
    {
        print("Accessing test object");
        if (gameObject.CompareTag("PlusMaxHP")) effects.PlusMaxHP();
        else if (gameObject.CompareTag("MinusMaxHP")) effects.MinusMaxHP();

        if (gameObject.CompareTag("PlusHP")) effects.PlusHP();
        else if (gameObject.CompareTag("MinusHP")) effects.MinusHP();
    }
}
