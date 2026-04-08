using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private SpringJoint2D spring;
    private Rigidbody2D rb;

    private bool isDragging = false;
    private Vector2 startPoint;

    public float maxDragDistance = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();

        startPoint = transform.position;

        spring.connectedAnchor = startPoint;
        spring.enabled = false; // importante
    }

    void Update()
    {
        if (isDragging)
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
        isDragging = true;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;

        spring.enabled = true;

        Invoke("Release", 0.1f);
    }

    void Release()
    {
        spring.enabled = false;
    }
}
