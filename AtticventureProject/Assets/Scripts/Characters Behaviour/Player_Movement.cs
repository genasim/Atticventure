using UnityEngine;
using Cinemachine;

public class Player_Movement : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    private PlayerInput playerInput;

>>>>>>> Stashed changes
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    private Vector2 movement;

    [SerializeField] private CinemachineVirtualCamera camCinamachine;

    public float speed = 2f;

    int xAnimation, yAnimation;

<<<<<<< Updated upstream
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

=======
    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Vector2 movement = playerInput.actions["Movement"].ReadValue<Vector2>();
>>>>>>> Stashed changes
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0 || movement.y != 0)
        {
            xAnimation = (int)movement.x;
            yAnimation = (int)movement.y;
            animator.SetFloat("Horizontal", xAnimation);
            animator.SetFloat("Vertical", yAnimation);
        }
    }

    void FixedUpdate()
    {
<<<<<<< Updated upstream
        Movement(movement);
=======
        Movement(movement: playerInput.actions["Movement"].ReadValue<Vector2>());
>>>>>>> Stashed changes
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
