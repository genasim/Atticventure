using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthManager), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player_Movement : MonoBehaviour
{
#region Variables
    private Vector2 movement;

    private Rigidbody2D rb2D;
    private Animator animator;

    [SerializeField] private CinemachineVirtualCamera camCinamachine;

    public float speed = 2f;

    int xAnimation, yAnimation;

    private InputSystem playerInput;
    // private PlayerInput playerInput;
    private HealthManager health;
#endregion

    void Awake() {
        // TODO: Reverted becuase of incompatability between On-Screen Controls and PlayerInput component
        // playerInput = GetComponent<PlayerInput>();
        // playerInput.actions["Movement"].performed += ctx => movement = ctx.ReadValue<Vector2>();
        // playerInput.actions["Movement"].canceled += ctx => movement = Vector2.zero;

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
        playerInput = _InitialiseInput.playerInput;
    }

    private void Update()
    {
        movement = playerInput.Player.Movement.ReadValue<Vector2>();
        HandleMovementAnimations();

        if (health.currentHealth > health.maxHealth) health.currentHealth = health.maxHealth;
    }

    void FixedUpdate()
    {
        Movement(movement);
    }

    private void HandleMovementAnimations() {
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0 || movement.y != 0)
        {
            xAnimation = Mathf.RoundToInt(movement.x);
            yAnimation = Mathf.RoundToInt(movement.y);
            animator.SetFloat("Horizontal", xAnimation);
            animator.SetFloat("Vertical", yAnimation);
        }
    }

    private void Movement(Vector2 movement)
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
