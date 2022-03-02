using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimEvent_HolyHunter : MonoBehaviour
{
    Enemy_Ranged enemy_Ranged;

    private void Awake()
    {
        enemy_Ranged = transform.parent.gameObject.GetComponent<Enemy_Ranged>();
    }

    void Shoot()    // Being used by AnimationEvent in the Animation
    {
        enemy_Ranged.Shoot();
    }
}

