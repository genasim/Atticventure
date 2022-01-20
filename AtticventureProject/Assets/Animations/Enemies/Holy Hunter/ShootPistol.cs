using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootPistol : MonoBehaviour
{
    Enemy_Ranged enemy_Ranged;

    private void Awake()
    {
        enemy_Ranged = transform.parent.gameObject.GetComponent<Enemy_Ranged>();
    }

    void Shoot()    // Being used by AnimationEvent in each of the Animations
    {
        enemy_Ranged.Shoot();
    }
}
