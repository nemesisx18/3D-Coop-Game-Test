using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject EndGameMenu;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI timeResultText;

    [Space]
    [Header("Player 1")]
    [SerializeField] private GameObject[] player1Icons;
    [SerializeField] private GameObject[] player1Healths;

    [Space]
    [Header("Player 2")]
    [SerializeField] private GameObject[] player2Icons;
    [SerializeField] private GameObject[] player2Healths;

    private SaveData saveData;

    private const string MENU_SCENE_NAME = "MenuScene";

    private void OnEnable()
    {
        EventManager.StartListening("CharacterDamaged", OnCharacterDamaged);
        EventManager.StartListening("GameResult", OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CharacterDamaged", OnCharacterDamaged);
        EventManager.StopListening("GameResult", OnGameOver);
    }

    private void Start()
    {
        saveData = SaveData.SaveDataInstance;

        SetupPlayerAvatar();
        SetupButton();
    }

    private void SetupPlayerAvatar()
    {
        player1Icons[saveData.Player1SelectedCharacter].SetActive(true);
        player2Icons[saveData.Player2SelectedCharacter].SetActive(true);
    }

    private void SetupButton()
    {
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(OnRestartGame);

        returnToMenuButton.onClick.RemoveAllListeners();
        returnToMenuButton.onClick.AddListener(OnReturnToMenu);
    }

    private void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnReturnToMenu()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }

    private void OnCharacterDamaged(object message)
    {
        CharacterTakeDamageMessage dmgMessage = (CharacterTakeDamageMessage)message;

        switch (dmgMessage.CharacterIndex)
        {
            case 0:
                for (int i = 0; i < player1Healths.Length; i++)
                {
                    if (i < dmgMessage.RemainingHealth)
                    {
                        player1Healths[i].SetActive(true);
                    }
                    else
                    {
                        player1Healths[i].SetActive(false);
                    }
                }
                break;
            case 1:
                for (int i = 0; i < player2Healths.Length; i++)
                {
                    if (i < dmgMessage.RemainingHealth)
                    {
                        player2Healths[i].SetActive(true);
                    }
                    else
                    {
                        player2Healths[i].SetActive(false);
                    }
                }
                break;
            default:
                Debug.LogError("Invalid character index in OnCharacterDamaged");
                break;
        }
    }

    private void OnGameOver(object message)
    {
        GameResultMessage result = (GameResultMessage)message;

        int minutes = Mathf.FloorToInt(result.GameTime / 60);
        int seconds = Mathf.FloorToInt(result.GameTime % 60);

        switch (result.ResultBool)
        {
            case true:
                resultText.text = "You Win!";
                break;
            case false:
                resultText.text = "You Lost!";
                break;
        }

        timeResultText.text = string.Format("{00:00}:{1:00}", minutes, seconds);

        EndGameMenu.SetActive(true);
    }
}
