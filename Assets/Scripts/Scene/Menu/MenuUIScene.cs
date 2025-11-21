using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIScene : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button charaSelectButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button bestScoreButton;
    [SerializeField] private Button exitButton;

    [Space]
    [SerializeField] private Button charaSelectBackButton;
    [SerializeField] private Button settingsBackButton;
    [SerializeField] private Button bestScoreBackButton;

    [Space]
    [SerializeField] private GameObject charaSelectPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject bestScorePanel;

    [Space]
    [Header("Settings")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    [Space]
    [Header("Character Select Display")]
    [SerializeField] private GameObject[] DisplayedP1Character;
    [SerializeField] private GameObject[] DisplayedP2Character;

    private int currentSelectedP1Character;
    private int currentSelectedP2Character;

    private const string LOADING_SCENE_NAME = "LoadingScene";

    private SaveData saveData;

    private void Start()
    {
        playButton.onClick.RemoveAllListeners();
        charaSelectButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        bestScoreButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        charaSelectBackButton.onClick.RemoveAllListeners();
        settingsBackButton.onClick.RemoveAllListeners();
        bestScoreBackButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(OnPlayButtonSelected);
        charaSelectButton.onClick.AddListener(ToggleCharacterSelectMenu);
        settingsButton.onClick.AddListener(ToggleSettingsMenu);
        bestScoreButton.onClick.AddListener(ToggleBestScoreMenu);
        exitButton.onClick.AddListener(OnExitButtonSelected);

        charaSelectBackButton.onClick.AddListener(ToggleCharacterSelectMenu);
        settingsBackButton.onClick.AddListener(ToggleSettingsMenu);
        bestScoreBackButton.onClick.AddListener(ToggleBestScoreMenu);

        saveData = SaveData.SaveDataInstance;

        LoadSavedSettings();

        musicToggle.onValueChanged.AddListener(delegate { ChangeMusicSetting(); });
        sfxToggle.onValueChanged.AddListener(delegate { ChangeSfxSetting(); });

    }

    private void OnPlayButtonSelected()
    {
        SceneManager.LoadScene(LOADING_SCENE_NAME);
    }

    private void ToggleCharacterSelectMenu()
    {
        bool isActive = charaSelectPanel.activeSelf;
        isActive = !isActive;

        charaSelectPanel.SetActive(isActive);

        DisplayedP1Character[currentSelectedP1Character].SetActive(false);
        DisplayedP2Character[currentSelectedP2Character].SetActive(false);

        DisplayedP1Character[saveData.Player1SelectedCharacter].SetActive(true);
        DisplayedP2Character[saveData.Player2SelectedCharacter].SetActive(true);

        currentSelectedP1Character = saveData.Player1SelectedCharacter;
        currentSelectedP2Character = saveData.Player2SelectedCharacter;
    }

    private void ToggleSettingsMenu()
    {
        bool isActive = settingsPanel.activeSelf;
        isActive = !isActive;

        settingsPanel.SetActive(isActive);
    }

    private void ToggleBestScoreMenu()
    {
        bool isActive = bestScorePanel.activeSelf;
        isActive = !isActive;

        bestScorePanel.SetActive(isActive);
    }

    private void OnExitButtonSelected()
    {
        Application.Quit();
    }

    private void LoadSavedSettings()
    {
        musicToggle.onValueChanged.RemoveAllListeners();
        sfxToggle.onValueChanged.RemoveAllListeners();


        musicToggle.isOn = AudioManager.AudioInstance.IsMusicOn;
        sfxToggle.isOn = AudioManager.AudioInstance.IsSfxOn;

        currentSelectedP1Character = saveData.Player1SelectedCharacter;
        currentSelectedP2Character = saveData.Player2SelectedCharacter;

        DisplayedP1Character[saveData.Player1SelectedCharacter].SetActive(true);
        DisplayedP2Character[saveData.Player2SelectedCharacter].SetActive(true);
    }

    public void ChangeMusicSetting()
    {
        Debug.Log("Music Toggle is now: " + musicToggle.isOn);
        EventManager.TriggerEvent("ToggleMusic");
    }

    public void ChangeSfxSetting()
    {
        Debug.Log("SFX Toggle is now: " + sfxToggle.isOn);
        EventManager.TriggerEvent("ToggleSfx");
    }
}
