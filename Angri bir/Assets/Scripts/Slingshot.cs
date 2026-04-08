using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private SpringJoint2D spring;
    private Rigidbody2D rb;

    private bool isDragging = false;
    private Vector2 startPoint;

    public float maxDragDistance = 4f;

    // Sistema de disparos
    public int maxShots = 3;
    private int shotsLeft;
    private bool canShoot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();

        startPoint = transform.position;

        spring.connectedAnchor = startPoint;
        spring.enabled = false;

        shotsLeft = maxShots;
    }

    void Update()
    {
        if (isDragging && canShoot)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = mousePos - startPoint;

            if (direction.magnitude > maxDragDistance)
            {
                direction = direction.normalized * maxDragDistance;
            }

            rb.position = startPoint + direction;
        }
    }

    void OnMouseDown()
    {
        if (!canShoot) return;

        isDragging = true;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        if (!canShoot) return;

        isDragging = false;
        rb.isKinematic = false;

        spring.enabled = true;

        shotsLeft--;
        Debug.Log("Disparos restantes: " + shotsLeft);

        if (shotsLeft <= 0)
        {
            canShoot = false;
            Debug.Log("Se acabaron los disparos");
        }

        Invoke("Release", 0.1f);
    }

    void Release()
    {
        spring.enabled = false;

        if (shotsLeft > 0)
        {
            Invoke("ResetBird", 1.5f);
        }
    }

    void ResetBird()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = startPoint;
    }
}