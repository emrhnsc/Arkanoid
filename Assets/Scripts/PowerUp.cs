using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerupType { BigBall, MultiBall, Expand, Shrink, FastBall, SlowBall, Laser }
    public PowerupType type;
    public float fallSpeed = 3f;

    private Camera mainCamera;
    private float bottom;

    void Start()
    {
        mainCamera = Camera.main;
        Vector3 lowerleft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        bottom = lowerleft.y;
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < bottom)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController paddle = other.GetComponent<PlayerController>();
        if (paddle)
        {
            ApplyEffect(paddle);
            Destroy(gameObject);
        }
    }

    void ApplyEffect(PlayerController paddle)
    {
        switch (type)
        {
            case PowerupType.BigBall:
                break;
            case PowerupType.MultiBall:
                for (global::System.Int32 i = 0; i < 3; i++)
                {
                    Ball b = Instantiate(GameManager.I.ball, GameManager.I.ball.transform.position, Quaternion.identity);
                    b.SendMessage("LaunchBall", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case PowerupType.Expand:
                paddle.transform.localScale += new Vector3(0.5f, 0, 0);
                break;
            case PowerupType.Shrink:
                paddle.transform.localScale -= new Vector3(0.2f, 0, 0);
                break;
            case PowerupType.FastBall:
                foreach (var b in GameManager.I.GetActiveBalls())
                {
                    b.minSpeed = 12f;
                    b.maxSpeed = 17f;
                }
                break;
            case PowerupType.SlowBall:
                foreach (var b in GameManager.I.GetActiveBalls())
                {
                    b.minSpeed = 3f;
                    b.maxSpeed = 7f;
                }
                break;
            case PowerupType.Laser:
                break;
        }
    }
}
