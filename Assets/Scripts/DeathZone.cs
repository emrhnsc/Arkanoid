using NUnit.Framework;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ball")
        {
            
            StartCoroutine(GameManager.I.ResetRound());
        }
    }
}
