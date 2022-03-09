using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public abstract class PlayerMove : MonoBehaviour
{
    public static PlayerData data;
    protected Vector2 movement;

    protected Rigidbody2D rb2D;
    protected Animator animator;
    protected CinemachineVirtualCamera camCinamachine;
    protected HealthManager health;

    private int xAnimation, yAnimation;

    abstract protected void OnEnable();
    abstract protected void OnDisable();
    abstract protected void Movement(Vector2 movement);

    protected void OnTriggerEnter2D(Collider2D collision) 
    {
        camCinamachine.LookAt = collision.transform.parent;
        camCinamachine.Follow = collision.transform.parent;

        AStarGridGraph.UpdateGraph(centre: collision.transform.position);
        PlayerShoot.currentRoom = collision.GetComponentInChildren<RoomManager>();

        if (!collision.GetComponent<RoomManager>().hasBeenActivated)
            collision.GetComponent<RoomManager>().InitiateRoom();
    }

    protected void HandleMovementAnimations() {
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0 || movement.y != 0)
        {
            xAnimation = Mathf.RoundToInt(movement.x);
            yAnimation = Mathf.RoundToInt(movement.y);
            animator.SetFloat("Horizontal", xAnimation);
            animator.SetFloat("Vertical", yAnimation);
        }
    }
    

}
