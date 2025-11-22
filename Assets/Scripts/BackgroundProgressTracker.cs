using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundProgressTracker : MonoBehaviour
{
    public Slider progressBar;
    public float smoothSpeed = 15f;

    public LevelSpeedController speedController;
    public ScrollingBackground scrollingBackground;

    private float targetValue = 0f;
    private bool finalBoostTriggered = false;

    public int scrollsToGoal = 10;

    private void Start()
    {
        if (scrollingBackground == null)
            scrollingBackground = FindAnyObjectByType<ScrollingBackground>();

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

    public void SetGoal(int newGoal)
    {
        scrollsToGoal = newGoal;
        ResetProgressImmediate();
    }

    public void ResetProgressImmediate()
    {
        progressBar.value = 0f;
        targetValue = 0f;
        finalBoostTriggered = false;
        ScrollingBackground.ResetTotalScrolledDistance();
    }
}
