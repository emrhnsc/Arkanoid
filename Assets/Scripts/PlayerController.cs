using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float minX = -7.8f;
    [SerializeField] private float maxX = 7.8f;

    [Header("Mouse")]
    [SerializeField] private bool enableMouseControl = false;
    [SerializeField] private float mouseFollowSpeed = 10f;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
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
