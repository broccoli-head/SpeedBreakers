using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float speed = 1.5f;
    public float despawnY = -6f;

    public static float GlobalSpeedMultiplier = 1f;

    private float rotationSpeed;

    void Start()
    {
        bool negativeRotation = Random.value < 0.5f;

        if (negativeRotation)
            rotationSpeed = Random.Range(-40f, -15f);
        else
            rotationSpeed = Random.Range(15f, 40f);
    }

    void Update()
    {
        float finalSpeed = speed * GlobalSpeedMultiplier;

        transform.position += Vector3.down * finalSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (transform.position.y < despawnY)
            Destroy(gameObject);
    }
}
