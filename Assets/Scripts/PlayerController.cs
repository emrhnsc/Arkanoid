using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float minX = -7.8f;
    [SerializeField] private float maxX = 7.8f;

    [Header("Dash")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private float startDashTime = 0.2f;
    [SerializeField] private float dashSpeed = 15f;
    private float currentDashTime;

    [Header("Mouse")]
    [SerializeField] private bool enableMouseControl = false;
    [SerializeField] private float mouseFollowSpeed = 10f;

    Rigidbody2D rb;

    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Dash();
    }

    void Dash()
    {
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(StartDash(Vector2.left));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(StartDash(Vector2.right));
            }
        }
    }

    IEnumerator StartDash(Vector2 direction)
    {
        canDash = false;
        currentDashTime = startDashTime;

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime;

            rb.linearVelocity = direction * dashSpeed;

            yield return null;
        }

        rb.linearVelocity = new Vector2(0f,0f);

        canDash = true;
    }

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enableMouseControl = true;
        }
        else if (Input.anyKeyDown)
        {
            enableMouseControl = false;
        }

        if (enableMouseControl)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 targetPos = transform.position;

            targetPos.x = Mathf.Clamp(mouseWorldPos.x, minX, maxX);

            transform.position = Vector3.Lerp(transform.position,targetPos,mouseFollowSpeed * Time.deltaTime);

        }
        else
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            Vector3 position = transform.position;
            position.x += horizontal * speed * Time.deltaTime;

            position.x = Mathf.Clamp(position.x, minX, maxX);

            transform.position = position;
        }

    }
}
