using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    private InputSystem inputSystem;
    private InputAction movementReader;
    private Vector2 movement;

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;

    [SerializeField] private CinemachineVirtualCamera camCinamachine;

    public float speed = 2f;

    int xAnimation, yAnimation;


    private void Awake() {
        inputSystem = new InputSystem();
    }

    private void OnEnable() {
        movementReader = inputSystem.Player.Movement;
        movementReader.Enable();
    }

    private void OnDisable() {
        movementReader.Disable();
    }

    private void Update()
    {
        movement = movementReader.ReadValue<Vector2>();
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
        Movement(movement: movementReader.ReadValue<Vector2>());
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
