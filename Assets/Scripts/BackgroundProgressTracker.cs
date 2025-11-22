using UnityEngine;
using UnityEngine.UI;

public class BackgroundProgressTracker : MonoBehaviour
{
    public Slider progressBar;
    public int scrollsToGoal = 10;
    public float smoothSpeed = 15f;

    public LevelSpeedController speedController;

    private float targetValue = 0f;
    private bool finalBoostTriggered = false;

    private void Start()
    {
        progressBar.minValue = 0f;
        progressBar.maxValue = 1f;
        progressBar.value = 0f;
    }

    private void Update()
    {
        float normalized = Mathf.Clamp01(
            ScrollingBackground.TotalScrolledDistance /
            (ScrollingBackground.BackgroundHeight * scrollsToGoal)
        );

        targetValue = normalized;

        progressBar.value = Mathf.MoveTowards(
            progressBar.value,
            targetValue,
            smoothSpeed * Time.deltaTime
        );

        if (!finalBoostTriggered && progressBar.value >= 0.999f)
        {
            finalBoostTriggered = true;
            speedController.TriggerFinalBoost();
        }
    }
}
