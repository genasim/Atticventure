using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Transform player {get;}
    void AttackPlayer();
}

public interface IDamagable
{
    void TakeDamage();
}

public interface IShooter
{
    void Shoot();
}

public interface IInteractable {
    void Interact();
}

