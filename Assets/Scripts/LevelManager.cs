using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public LevelSpeedController speedController;
    public BackgroundProgressTracker progressTracker;
    public ScrollingBackground background;

    public int scrollIncreasePerLevel = 20; // Trudnoœæ — zwiêksza cel co level

    private int currentLevel = 1;
    public static int CurrentLevel = 0;


    private void OnEnable()
    {
        speedController.OnLevelEnded += HandleLevelEnd;
    }

    private void OnDisable()
    {
        speedController.OnLevelEnded -= HandleLevelEnd;
    }

    void Start()
    {
        StartNewLevel();
    }

    private void HandleLevelEnd()
    {
        currentLevel++;
        CurrentLevel++;
        StartCoroutine(LevelTransitionRoutine());
    }

    private IEnumerator LevelTransitionRoutine()
    {
        background.ChangeToRandomSet();
        background.ResetScrolling();
        progressTracker.ResetProgressImmediate();

        yield return new WaitForSeconds(0.3f);

        StartNewLevel();
    }

    private void StartNewLevel()
    {
        int pages = background.GetPageCount();
        int goal = pages + currentLevel * scrollIncreasePerLevel;

        progressTracker.SetGoal(goal);

        Debug.Log($"Level {currentLevel} started | pages={pages}, goal={goal}");
    }
}
