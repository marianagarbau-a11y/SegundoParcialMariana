using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float breakForce = 5f;
    public int points = 100;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                ScoreManager.instance.AddPoints(points);
                Destroy(gameObject);
            }
        }
    }
}