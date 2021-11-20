using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    public static int numberOfHearths = 5;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Update()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().currentHealth / 10;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numberOfHearths)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;


            if ((i + 1) * 2 <= health)
                hearts[i].sprite = fullHeart;
            else if ((i + 1) * 2 == health + 1)
                hearts[i].sprite = halfHeart;
            else if ((i + 1) * 2 > health)
                hearts[i].sprite = emptyHeart;
        }
    }
}
