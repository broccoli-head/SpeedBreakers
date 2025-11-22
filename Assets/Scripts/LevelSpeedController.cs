using UnityEngine;
using System.Collections;

public class LevelSpeedController : MonoBehaviour
{
    public ScrollingBackground background;
    public ObstacleSpawner spawner;

    public float startSpeed = 5f;
    public float maxSpeed = 15f;
    public float acceleration = 0.6f;

    public float asteroidSpeedMultiplier = 1.8f;

    public float finalBoostDuration = 3f;
    public float finalBoostMultiplier = 4f;
    public float finalPhaseDuration = 1f;
    public float finalPhaseExtraMultiplier = 2.5f;

    public float normalSpawnInterval = 0.8f;
    public float finalSpawnInterval = 0.15f;

    private bool finalBoostActive = false;
    private float finalBoostTimer = 0f;

    public static bool PlayersInvincible = false;

    public System.Action OnLevelEnded;

    public GameObject barrierText;

    void Start()
    {
        background.speed = startSpeed;
        background.speedIncrease = acceleration;

        spawner.spawnInterval = normalSpawnInterval;
        spawner.objectSpeed = startSpeed * asteroidSpeedMultiplier;

        MoveObstacle.GlobalSpeedMultiplier = 1f;

        if (barrierText != null)
            barrierText.SetActive(false);
    }

    void Update()
    {
        if (!finalBoostActive)
        {
            spawner.objectSpeed = background.speed * asteroidSpeedMultiplier;
            return;
        }

        finalBoostTimer += Time.deltaTime;

        float currentMultiplier;

        if (finalBoostTimer < finalBoostDuration - finalPhaseDuration)
        {
            float t = finalBoostTimer / (finalBoostDuration - finalPhaseDuration);
            currentMultiplier = Mathf.Lerp(1f, finalBoostMultiplier, t);
        }
        else
        {
            float t = (finalBoostTimer - (finalBoostDuration - finalPhaseDuration)) / finalPhaseDuration;
            currentMultiplier = Mathf.Lerp(finalBoostMultiplier, finalBoostMultiplier * finalPhaseExtraMultiplier, t);
        }

        background.speed = maxSpeed * currentMultiplier;
        spawner.objectSpeed = background.speed * asteroidSpeedMultiplier;
        spawner.spawnInterval = finalSpawnInterval;

        MoveObstacle.GlobalSpeedMultiplier = currentMultiplier;

        if (finalBoostTimer >= finalBoostDuration)
        {
            finalBoostActive = false;

            PlayersInvincible = false;
            MoveObstacle.GlobalSpeedMultiplier = 1f;
            spawner.spawnInterval = normalSpawnInterval;

            StartCoroutine(EndLevelRoutine());
        }
    }

    public void TriggerFinalBoost()
    {
        finalBoostActive = true;
        finalBoostTimer = 0f;

        PlayersInvincible = true;
        spawner.spawnInterval = finalSpawnInterval;

        MoveObstacle.GlobalSpeedMultiplier = finalBoostMultiplier;

        if (barrierText != null)
            barrierText.SetActive(true);
    }

    private IEnumerator EndLevelRoutine()
    {
        while (background.speed > startSpeed + 0.05f)
        {
            background.speed = Mathf.Lerp(background.speed, startSpeed, 20f * Time.deltaTime);
            spawner.objectSpeed = background.speed * asteroidSpeedMultiplier;
            yield return null;
        }

        background.speed = startSpeed;
        spawner.objectSpeed = startSpeed * asteroidSpeedMultiplier;

        if (barrierText != null)
            barrierText.SetActive(false);

        OnLevelEnded?.Invoke();
    }
}
