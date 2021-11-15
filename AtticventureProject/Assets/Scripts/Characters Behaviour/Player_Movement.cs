using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Pathfinding;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] internal Rigidbody2D rb2D;
    Vector2 movement;
    public Animator animator;

    public CinemachineVirtualCamera camCinamachine;

    public float speed = 2f;

    [SerializeField] private bool isMovingRight;
    [SerializeField] private bool isMovingLeft;
    [SerializeField] private bool isMovingUp;
    [SerializeField] private bool isMovingDown;
    [SerializeField] private bool isNotMoving;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x > 0)
        {
            isMovingRight = true;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
            isNotMoving = false;
        }
        else if (movement.x < 0)
        {
            isMovingLeft = true;
            isMovingRight = false;
            isMovingUp = false;
            isMovingDown = false;
            isNotMoving = false;
        }
        else if (movement.y > 0)
        {
            isMovingUp = true;
            isMovingRight = false;
            isMovingDown = false;
            isMovingLeft = false;
            isNotMoving = false;
        }
        else if(movement.y < 0)
        {
            isMovingDown = true;
            isMovingUp = false;
            isMovingRight = false;
            isMovingLeft = false;
            isNotMoving = false;
        }
        else
        {
            isNotMoving = true;
            isMovingUp = false;
            isMovingRight = false;
            isMovingDown = false;
            isMovingLeft = false;
        }

        animator.SetBool("isMovingUp", isMovingUp);
        animator.SetBool("isMovingRight", isMovingRight);
        animator.SetBool("isMovingDown", isMovingDown);
        animator.SetBool("isMovingLeft", isMovingLeft);
        animator.SetBool("isNotMoving", isNotMoving);

    }

    void FixedUpdate()
    {
        Movement(movement);
    }

    void Movement(Vector2 movement)
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        camCinamachine.LookAt = collision.transform.parent;
        camCinamachine.Follow = collision.transform.parent;

        AStarGridGraph.UpdateGraph(centre: collision.transform.position);

        if (!collision.GetComponent<RoomManager>().hasBeenActivated)
            collision.GetComponent<RoomManager>().InitiateRoom();
    }
}
