using System.Collections.Generic;
using UnityEngine;

public class LeaderboardData : MonoBehaviour
{
    public static LeaderboardData instance;

    private List<float> scores = new List<float>();

    public List<float> Scores => scores;

    private void OnEnable()
    {
        EventManager.StartListening("GameResult", AddScore);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameResult", AddScore);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadScoreFromSaveData();
    }

    private void LoadScoreFromSaveData()
    {
        if (SaveData.SaveDataInstance.Scores != null)
        {
            scores = SaveData.SaveDataInstance.Scores;
        }
    }

    public void AddScore(object score)
    {
        GameResultMessage message = (GameResultMessage)score;

        if (!message.ResultBool)
        {
            return;
        }

        if (scores.Count == 10)
        {
            float longestScore = Mathf.Max(scores.ToArray());
            if (message.GameTime > longestScore)
            {
                scores.Remove(longestScore);
            }
            else
            {
                return;
            }
        }

        scores.Add(message.GameTime);

        SortLeaderboard(scores);

        SaveData.SaveDataInstance.UpdateLeaderboard();
    }

    public void SortLeaderboard(List<float> score)
    {
        score.Sort((a, b) => a.CompareTo(b));
    }
}
