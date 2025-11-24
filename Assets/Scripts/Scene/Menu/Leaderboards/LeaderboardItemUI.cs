using TMPro;
using UnityEngine;

public class LeaderboardItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private float score;
    private int rank;

    public void Setup(float _score, int _rank)
    {
        score = _score;
        rank = _rank;

        UpdateUI();
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(score / 60);
        int seconds = Mathf.FloorToInt(score % 60);

        // Update the UI Text
        scoreText.text = string.Format(rank + ". " + "{00:00}:{1:00}", minutes, seconds);
    }
}
