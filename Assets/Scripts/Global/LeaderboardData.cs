using NUnit.Framework;
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

    private void Start()
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

    public void AddScore(object score)
    {
        GameResultMessage message = (GameResultMessage)score;

        if(!message.ResultBool)
        {
            return;
        }

        if (scores.Count == 10)
        {
            float lowestScore = Mathf.Min(scores.ToArray());
            if (message.GameTime > lowestScore)
            {
                scores.Remove(lowestScore);
            }
            else
            {
                return; // Do not add the new score if it's not higher than the lowest
            }
        }
        
        scores.Add(message.GameTime);

        SortLeaderboard();

        SaveData.SaveDataInstance.UpdateLeaderboard();
    }

    public void SortLeaderboard()
    {
        scores.Sort((a, b) => b.CompareTo(a));
    }
}
