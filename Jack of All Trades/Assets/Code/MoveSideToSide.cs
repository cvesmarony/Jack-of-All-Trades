using UnityEngine;

public class MoveSideToSide : MonoBehaviour
{
    public float moveSpeed = 0.01f; // Speed of movement
    public float changeDirectionTime = 0.1f; // Time interval before changing direction
    public float moveDistance = 5f; // Maximum distance NPC can move left or right from the starting position

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;
    private float initialX; // Store initial X position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialX = transform.position.x; // Store the initial position of the NPC
        ChooseNewDirection();
        timer = changeDirectionTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChooseNewDirection();
            timer = changeDirectionTime;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void ChooseNewDirection()
    {
        // Only allow movement along the X-axis (left or right)
        float direction = Random.Range(0f, 1f) > 0.5f ? 1f : -1f; // Randomly pick left or right
        moveDirection = new Vector2(direction, 0f); // Y-axis movement is fixed to 0
    }
}
