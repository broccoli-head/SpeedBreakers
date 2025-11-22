using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public TMP_Text scoreDisplay;
    public TMP_Text endScreenScore;
    public int pointsPerSecond = 4;
    private float score = 0f;

    void Update()
    {
        score += pointsPerSecond * Time.deltaTime;
        string displayableScore = Mathf.FloorToInt(score).ToString();
        scoreDisplay.text = displayableScore;
        endScreenScore.text = "Score: " + displayableScore + "\nBarriers passed: "+ LevelManager.CurrentLevel;
    }
}
