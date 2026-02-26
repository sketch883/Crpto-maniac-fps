using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 5f;
    public float health = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        MovePlayer();
        Jump();
        ApplyGravity();
    }

    void MovePlayer() {
        isGrounded = controller.isGrounded;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (Input.GetKey(KeyCode.LeftShift)) {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        } else {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }
    }

    void Jump() {
        if (isGrounded && Input.GetButtonDown("Jump")) {
            velocity.y = jumpForce;
        }
    }

    void ApplyGravity() {
        if (isGrounded && velocity.y < 0) {
            velocity.y = 0f;
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
