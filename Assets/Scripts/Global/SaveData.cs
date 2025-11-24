using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData SaveDataInstance;

    [SerializeField] private int player1SelectedCharacter;
    [SerializeField] private int player2SelectedCharacter;
    [SerializeField] private int musicValue;
    [SerializeField] private int sfxValue;
    [SerializeField] private List<float> scores = new List<float>();

    public int Player1SelectedCharacter => player1SelectedCharacter;
    public int Player2SelectedCharacter => player2SelectedCharacter;

    public int MusicValue => musicValue;

    public int SfxValue => sfxValue;

    public List<float> Scores => scores;

    public void Awake()
    {
        if (SaveDataInstance == null)
        {
            SaveDataInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadFromJson();
    }

    private void OnEnable()
    {
        EventManager.StartListening("Player1CharacterSelected", OnPlayer1CharacterSelected);
        EventManager.StartListening("Player2CharacterSelected", OnPlayer2CharacterSelected);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Player1CharacterSelected", OnPlayer1CharacterSelected);
        EventManager.StopListening("Player2CharacterSelected", OnPlayer2CharacterSelected);
    }

    public void LoadDefaultData()
    {
        musicValue = 1;
        sfxValue = 1;

        SaveIntoJson();
    }

    public void SaveIntoJson()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);

            LeaderboardData.instance.SortLeaderboard(scores);
        }
        else
        {
            LoadDefaultData();
        }
    }

    public void UpdateMusicValue(int newValue)
    {
        musicValue = newValue;
        SaveIntoJson();
    }

    public void UpdateSfxValue(int newValue)
    {
        sfxValue = newValue;
        SaveIntoJson();
    }

    public void OnPlayer1CharacterSelected(object index)
    {
        int playerIndex = (int)index;

        player1SelectedCharacter = playerIndex;

        SaveIntoJson();
    }

    public void OnPlayer2CharacterSelected(object index)
    {
        int playerIndex = (int)index;

        player2SelectedCharacter = playerIndex;

        SaveIntoJson();
    }

    public void UpdateLeaderboard()
    {
        scores = LeaderboardData.instance.Scores;

        SaveIntoJson();
    }
}
