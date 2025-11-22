using UnityEngine;

public class LevelSpeedController : MonoBehaviour
{
    public ScrollingBackground background;
    public ObstacleSpawner spawner;

    public float startSpeed = 2f;
    public float maxSpeed = 8f;
    public float acceleration = 0.2f;

    public float asteroidSpeedMultiplier = 1.2f;

    public float finalBoostDuration = 10f;
    public float finalBoostMultiplier = 3f;

    [Header("Spawn Settings")]
    public float normalSpawnInterval = 1.2f;
    public float finalSpawnInterval = 0.25f;

    private bool finalBoostActive = false;
    private float finalBoostTimer = 0f;

    void Start()
    {
        background.speed = startSpeed;
        background.speedIncrease = acceleration;

        spawner.objectSpeed = startSpeed * asteroidSpeedMultiplier;
        spawner.spawnInterval = normalSpawnInterval;
    }

    void Update()
    {
        if (!finalBoostActive)
        {
            // Sync asteroid speed with background speed
            spawner.objectSpeed = background.speed * asteroidSpeedMultiplier;
        }
        else
        {
            // Increase boost timer
            finalBoostTimer += Time.deltaTime;

            // SUPER FAST background
            background.speed = maxSpeed * finalBoostMultiplier;

            // SUPER FAST asteroids
            spawner.objectSpeed = background.speed * asteroidSpeedMultiplier;

            // SUPER FAST spawn rate
            spawner.spawnInterval = finalSpawnInterval;

            // End boost after duration
            if (finalBoostTimer >= finalBoostDuration)
            {
                finalBoostActive = false;

                spawner.spawnInterval = normalSpawnInterval;
            }
        }
    }

    public void TriggerFinalBoost()
    {
        finalBoostActive = true;
        finalBoostTimer = 0f;

        spawner.spawnInterval = finalSpawnInterval;
    }
}
