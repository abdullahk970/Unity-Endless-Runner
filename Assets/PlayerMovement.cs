using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement")]
    public float jumpForce = 5f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 12f;

    [Header("Magnet System")]
    public float magnetRange = 7f;   // ✅ Updated as per your requirement
    public bool magnetActive = false;

    [Header("References")]
    public AudioSource jumpSound;
    public AudioSource hitSound;
    public Animator animator;

    private int currentLane = 0;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isGameOver = false;

    private float targetX;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetX = 0;
    }

    void Update()
    {
        if (GameManager.instance.isGameOver || isGameOver) return;

        animator.SetBool("isRunning", true);

        HandleLaneInput();
        HandleJump();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isGameOver || isGameOver) return;

        MovePlayer();
    }

    // -------------------------
    // INPUT HANDLING
    // -------------------------
    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > -1)
            currentLane--;

        if (Input.GetKeyDown(KeyCode.D) && currentLane < 1)
            currentLane++;

        targetX = currentLane * laneDistance;
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");

            if (jumpSound != null)
                jumpSound.Play();

            isGrounded = false;
        }
    }

    // -------------------------
    // MOVEMENT
    // -------------------------
    void MovePlayer()
    {
        float speed = GameManager.instance.gameSpeed;

        Vector3 currentPos = rb.position;

        float smoothX = Mathf.Lerp(currentPos.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);
        float newZ = currentPos.z + speed * Time.fixedDeltaTime;

        rb.MovePosition(new Vector3(smoothX, currentPos.y, newZ));
    }

    // -------------------------
    // COLLISION
    // -------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit Obstacle");

            if (hitSound != null)
                hitSound.Play();

            GameManager.instance.GameOver();
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // -------------------------
    // MAGNET SYSTEM
    // -------------------------
    public void ActivateMagnet(float duration)
    {
        magnetActive = true;

        // ✅ Prevent stacking invokes
        CancelInvoke(nameof(DisableMagnet));
        Invoke(nameof(DisableMagnet), duration);
    }

    void DisableMagnet()
    {
        magnetActive = false;
    }

    // Optional: visualize magnet range in Scene view
    private void OnDrawGizmosSelected()
    {
        if (!magnetActive) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, magnetRange);
    }
}