using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int hitPoints;
    public Slider healthSlider;
    public float smoothSpeed;

    private float targetHealth;
    private Renderer renderer;

    void Start()
    {
        targetHealth = hitPoints;
        healthSlider.maxValue = hitPoints;
        healthSlider.value = hitPoints;
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, Time.deltaTime * smoothSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            decreaseHP();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            decreaseHP();
    }

    void Explode()
    {
        Destroy(gameObject);
    }

    void decreaseHP()
    {
        hitPoints--;
        targetHealth = hitPoints;
        if (hitPoints <= 0)
        {
            if (renderer != null)
                renderer.enabled = false;
            Invoke("Explode", 2f);
        }
    }
}
