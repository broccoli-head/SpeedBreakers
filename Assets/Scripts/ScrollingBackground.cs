using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 2f;
    public float speedIncrease = 0.2f;

    public static System.Action OnBackgroundScrolled;
    public static float TotalScrolledDistance { get; private set; }
    public static float BackgroundHeight { get; private set; }
    public static int TotalScrolls { get; private set; }

    private float backgroundHeight;
    private Transform bg1;
    private Transform bg2;

    void Start()
    {
        bg1 = transform.GetChild(0);
        bg2 = transform.GetChild(1);

        backgroundHeight = bg1.GetComponent<SpriteRenderer>().bounds.size.y;
        BackgroundHeight = backgroundHeight;

        TotalScrolledDistance = 0f;
        TotalScrolls = 0;
    }

    void Update()
    {
        speed += speedIncrease * Time.deltaTime;

        float delta = speed * Time.deltaTime;

        TotalScrolledDistance += delta;

        bg1.position += Vector3.down * delta;
        bg2.position += Vector3.down * delta;

        if (bg1.position.y < -backgroundHeight)
        {
            bg1.position = new Vector3(
                bg1.position.x,
                bg2.position.y + backgroundHeight,
                bg1.position.z
            );

            OnBackgroundScrolled?.Invoke();
            TotalScrolls++;
        }


        if (bg2.position.y < -backgroundHeight)
        {
            bg2.position = new Vector3(
                bg2.position.x,
                bg1.position.y + backgroundHeight,
                bg2.position.z
            );

            OnBackgroundScrolled?.Invoke();
            TotalScrolls++;
        }
    }
}
