using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    private Vector2 movement;

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;

    [SerializeField] private CinemachineVirtualCamera camCinamachine;

    public float speed = 2f;

    int xAnimation, yAnimation;

    private PlayerInput playerInput;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        movement = playerInput.actions["Movement"].ReadValue<Vector2>();
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0 || movement.y != 0)
        {
            xAnimation = Mathf.RoundToInt(movement.x);
            yAnimation = Mathf.RoundToInt(movement.y);
            animator.SetFloat("Horizontal", xAnimation);
            animator.SetFloat("Vertical", yAnimation);
        }
    }

    void FixedUpdate()
    {
        Movement(movement: playerInput.actions["Movement"].ReadValue<Vector2>());
    }

    void Movement(Vector2 movement)
    {
        rb2D.MovePosition(rb2D.position + speed * Time.fixedDeltaTime * movement);
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
