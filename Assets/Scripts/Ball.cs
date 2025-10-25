using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float launchSpeed = 10f;
    [SerializeField] private float paddleBounceAngle = 60f;
    public float minSpeed = 8f;
    public float maxSpeed = 12f;

    PlayerController currentPaddle;
    private Rigidbody2D rb;
    private bool isLaunched = false;
    Vector3 startPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameManager.I != null)
        {
            GameManager.I.RegisterBall(this);
        }
    }

    void Start()
    {
        currentPaddle = FindFirstObjectByType<PlayerController>();
        startPoint = currentPaddle.transform.position;
    }

    void Update()
    {
        if (!isLaunched && currentPaddle )
        {
            transform.position = currentPaddle.transform.position + Vector3.up * 0.4f;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                LaunchBall();
            }
        }

        if (isLaunched)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * Mathf.Clamp(rb.linearVelocity.magnitude, minSpeed, maxSpeed);
            if (Mathf.Abs(Vector2.Dot(rb.linearVelocity.normalized, Vector2.up)) < 0.05f || Mathf.Abs(Vector2.Dot(rb.linearVelocity.normalized, Vector2.right)) < 0.05f)
                rb.linearVelocity = (rb.linearVelocity + Random.insideUnitCircle * 0.05f).normalized * rb.linearVelocity.magnitude;
        }
    }

    void LaunchBall()
    {
        isLaunched = true;
        Vector2 dir = new Vector2(Random.Range(-0.25f, 0.25f), 1f).normalized;
        rb.linearVelocity = dir * launchSpeed;
    }

    public void ResetBall(PlayerController paddle)
    {
        isLaunched = false;
        transform.position = currentPaddle.transform.position + Vector3.up * 0.4f;
        currentPaddle.transform.position = startPoint;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController paddle = other.collider.GetComponent<PlayerController>();
        if (paddle)
        {
            float halfWidth = paddle.GetComponent<CapsuleCollider2D>().size.x * paddle.transform.localScale.x * 0.5f;
            float hitX = transform.position.x - paddle.transform.position.x;
            float t = Mathf.Clamp(hitX / halfWidth, -1f, 1f);
            float angle = t * paddleBounceAngle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
            rb.linearVelocity = dir.normalized * Mathf.Max(minSpeed, rb.linearVelocity.magnitude);
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("deathzone"))
        {
            if (GameManager.I != null)
            {
                GameManager.I.LoseLife(this);
            }
        }
    }
}
