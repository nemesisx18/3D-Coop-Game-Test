using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject EndGameMenu;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMenuButton;

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
    }

    private void OnDisable()
    {
        EventManager.StopListening("CharacterDamaged", OnCharacterDamaged);
    }

    private void Start()
    {
        saveData = SaveData.SaveDataInstance;

        player1Icons[saveData.Player1SelectedCharacter].SetActive(true);
        player2Icons[saveData.Player2SelectedCharacter].SetActive(true);

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

    private void UpdateHealthUI()
    {

    }

    private void OnCharacterDamaged(object message)
    {
        CharacterTakeDamageMessage dmgMessage = (CharacterTakeDamageMessage)message;

        switch(dmgMessage.CharacterIndex)
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
}
