using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Gameplay")]
    public int lives = 3;

    [Header("Refs")]
    public Ball ball;
    public PlayerController paddle;

    private List<Ball> activeBalls = new List<Ball>();

    void Awake()
    {
        I = this;  
    }

    public void RegisterBall(Ball b)
    {
        if (!activeBalls.Contains(b))
        {
            activeBalls.Add(b);
        }
    }

    public List<Ball> GetActiveBalls()
    {
        return activeBalls;
    }

    public void LoseLife(Ball b)
    {
        if (activeBalls.Contains(b))
        {
            activeBalls.Remove(b);
            Destroy(b.gameObject);
        }
        if (activeBalls.Count == 0)
        {
            lives--;
            if (lives <= 0) GameOver();
            else StartCoroutine(ResetRound());
        }
    }

    public void GameOver()
    {

    }

    public IEnumerator ResetRound(float time = 1f)
    {
        yield return new WaitForSeconds(time);
        if (ball)
        {
            ball.ResetBall(paddle);
        }
    }
}
