using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitPoints;
    public Sprite[] damageSprites;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();   
    }

    void Start()
    {
        switch (gameObject.tag)
        {
            case "brick1":
                hitPoints = 2;
                break;
            case "brick2":
                hitPoints = 3;
                break;
            case "brick3":
                hitPoints = 4;
                break;
            case "brick4":
                hitPoints = 5;
                break;
            default:
                hitPoints = 1;
                break;
        }
    }

    void Hit()
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            Break();
        }
        else UpdateDamageSprite();
    }

    void Break()
    {
        Destroy(gameObject);
    }

    void UpdateDamageSprite()
    {
        int idx = Mathf.Clamp(hitPoints - 1, 0, damageSprites.Length);
        sr.sprite = damageSprites[idx];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ball")
        {
            Hit();
        }
    }
}
