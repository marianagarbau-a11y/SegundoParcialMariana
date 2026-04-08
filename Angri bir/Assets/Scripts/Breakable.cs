using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float breakForce = 5f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica que el objeto sea el pajarito
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verifica la fuerza del impacto
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                Destroy(gameObject);
            }
        }
    }
}