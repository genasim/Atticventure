using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent_DemHand : MonoBehaviour
{
    private Enemy_DeamonHand demonHand;
    private bool isActive;

    private void Awake() {
        demonHand = transform.parent.GetComponent<Enemy_DeamonHand>();
        isActive = false;
    }

    void TeleportToPlayer() {
        demonHand.TeleportToPlayer();
    }

    void GrabPlayer() {
        demonHand.Grab();
    }

    void ActivateHitBox() {
        isActive = !isActive;
        demonHand.ActivateHitBox(isActive);
    }
}