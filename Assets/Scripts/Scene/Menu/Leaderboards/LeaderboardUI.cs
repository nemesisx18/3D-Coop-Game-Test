using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private LeaderboardItemUI leaderboardItemPrefab;
    [SerializeField] private Transform leaderboardParent;
    private LeaderboardData leaderboardData;

    private void Start()
    {
        leaderboardData = LeaderboardData.instance;

        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        for (int i = 0; i < leaderboardData.Scores.Count; i++)
        {
            LeaderboardItemUI item = Instantiate(leaderboardItemPrefab, leaderboardParent);
            item.Setup(leaderboardData.Scores[i], i + 1);
        }
        
    }
}
