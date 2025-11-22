using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int hitPoints;
    public Slider healthSlider;
    public float smoothSpeed;
    public GameObject explosionPrefab;
    public GameObject gameOverScreen;
    public AudioSource collisionSource;

    public static int playerCount = 2;
    private float targetHealth;

    void Start()
    {
        targetHealth = hitPoints;
        healthSlider.maxValue = hitPoints;
        healthSlider.value = hitPoints;
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


    void decreaseHP()
    {
        if (LevelSpeedController.PlayersInvincible) return;

        hitPoints--;
        targetHealth = hitPoints;
        collisionSource.Play();

        if (hitPoints == 0)
        {
            if (GetComponent<Renderer>() != null)
                GetComponent<Renderer>().enabled = false;
            
            playerCount--;

            if (playerCount <= 0)
            {
                gameOverScreen.SetActive(true);
                ScrollingBackground.gameRunning = false;
            }

            healthSlider.value = 0;
            Instantiate(explosionPrefab, transform);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
