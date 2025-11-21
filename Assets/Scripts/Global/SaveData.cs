using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData SaveDataInstance;

    [SerializeField] private int player1SelectedCharacter;
    [SerializeField] private int player2SelectedCharacter;
    [SerializeField] private int musicValue;
    [SerializeField] private int sfxValue;

    public int Player1SelectedCharacter => player1SelectedCharacter;
    public int Player2SelectedCharacter => player2SelectedCharacter;

    public int MusicValue => musicValue;

    public int SfxValue => sfxValue;

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

        Debug.Log("Data Saved into JSON " + json);
    }

    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("Loading Data from JSON");

            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else
        {
            Debug.Log("No Save File Found, Loading Default Data");

            LoadDefaultData();
        }
    }

    public void UpdateMusicValue(int newValue)
    {
        musicValue = newValue;
        SaveIntoJson();

        Debug.Log("Music Value Updated to: " + newValue);
    }

    public void UpdateSfxValue(int newValue)
    {
        sfxValue = newValue;
        SaveIntoJson();

        Debug.Log("SFX Value Updated to: " + newValue);
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
}
