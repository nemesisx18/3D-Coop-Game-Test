using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public bool IsMusicOn { get; private set; }
    public bool IsSfxOn { get; private set; }

    private SaveData saveData;

    private UnityAction onSwitchMusicValue;
    private UnityAction onSwitchSfxValue;

    private void Awake()
    {
        if (AudioInstance == null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        onSwitchMusicValue = new UnityAction(ToggleMusic);
        onSwitchSfxValue = new UnityAction(ToggleSfx);
    }

    private void OnEnable()
    {
        EventManager.StartListening("ToggleMusic", onSwitchMusicValue);
        EventManager.StartListening("ToggleSfx", onSwitchSfxValue);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ToggleMusic", onSwitchMusicValue);
        EventManager.StopListening("ToggleSfx", onSwitchSfxValue);
    }

    private void Start()
    {
        saveData = SaveData.SaveDataInstance;
        LoadData();
    }

    private void Update()
    {
        musicSource.mute = !IsMusicOn;
        sfxSource.mute = !IsSfxOn;
    }

    private void LoadData()
    {
        int musicDataHolder = saveData.MusicValue;

        if (musicDataHolder == 1)
        {
            IsMusicOn = true;
        }
        else
        {
            IsMusicOn = false;
        }

        Debug.Log("Music is " + IsMusicOn);

        int sfxDataHolder = saveData.SfxValue;

        if (sfxDataHolder == 1)
        {
            IsSfxOn = true;
        }
        else
        {
            IsSfxOn = false;
        }

        Debug.Log("SFX is " + IsSfxOn);
    }

    private void ToggleMusic()
    {
        IsMusicOn = !IsMusicOn;
        if(IsMusicOn)
        {
            saveData.UpdateMusicValue(1);
        }
        else
        {
            saveData.UpdateMusicValue(0);
        }
    }

    private void ToggleSfx()
    {
        IsSfxOn = !IsSfxOn;
        if(IsSfxOn)
        {
            saveData.UpdateSfxValue(1);
        }
        else
        {
            saveData.UpdateSfxValue(0);
        }
    }
}
