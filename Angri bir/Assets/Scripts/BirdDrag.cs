using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]
public class BirdDrag : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpringJoint2D spring;

    private bool isDragging = false;
    private Camera cam;

    public float maxDragDistance = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
        cam = Camera.main;

        rb.isKinematic = true;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 anchor = spring.connectedAnchor;

            Vector2 direction = mousePos - anchor;

            if (direction.magnitude > maxDragDistance)
            {
                direction = direction.normalized * maxDragDistance;
            }

            rb.position = anchor + direction;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        rb.isKinematic = false;
        spring.enabled = true;

        Invoke("ReleaseSpring", 0.1f);
    }

    void ReleaseSpring()
    {
        spring.enabled = false;
    }
}
