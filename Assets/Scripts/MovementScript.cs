using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 50f;
    public float maxSpeed = 10f;
    public float damping = 5f;

    [Header("Input Keys")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    [Header("Screen Margins (World Units)")]
    public float marginTopWorld = 2.5f;
    public float marginBottomWorld = 2.5f;
    public float marginLeftWorld = 1.5f;
    public float marginRightWorld = 1.5f;

    private Rigidbody2D rb;
    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        Vector2 force = Vector2.zero;

        if (Input.GetKey(up)) force += (Vector2)transform.up;
        if (Input.GetKey(down)) force -= (Vector2)transform.up;
        if (Input.GetKey(left)) force -= (Vector2)transform.right;
        if (Input.GetKey(right)) force += (Vector2)transform.right;

        if (force != Vector2.zero)
        {
            rb.AddForce(force.normalized * acceleration);

            if (rb.linearVelocity.magnitude > maxSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.fixedDeltaTime * damping);
        }

        // --- Clamp inside camera view with separate margins ---
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        float marginTopViewport = marginTopWorld / worldHeight;
        float marginBottomViewport = marginBottomWorld / worldHeight;
        float marginLeftViewport = marginLeftWorld / worldWidth;
        float marginRightViewport = marginRightWorld / worldWidth;

        viewPos.y = Mathf.Clamp(viewPos.y, marginBottomViewport, 1f - marginTopViewport);
        viewPos.x = Mathf.Clamp(viewPos.x, marginLeftViewport, 1f - marginRightViewport);

        viewPos.z = Mathf.Abs(cam.transform.position.z - transform.position.z);

        Vector3 worldPos = cam.ViewportToWorldPoint(viewPos);
        rb.position = worldPos;
    }
}
