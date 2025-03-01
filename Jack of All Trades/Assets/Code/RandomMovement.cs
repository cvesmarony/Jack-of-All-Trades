using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 0.01f; // Speed of movement
    public float changeDirectionTime = 0.1f; // Time interval before changing direction

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }
}
