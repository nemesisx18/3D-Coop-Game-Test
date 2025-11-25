using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public float Timer => timer;    

    private float timer;
    private bool timerRunning = false;

    private UnityAction onGameOver;

    private void OnEnable()
    {
        EventManager.StartListening("GameOver", onGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameOver", onGameOver);
    }

    private void Awake()
    {
        onGameOver = new UnityAction(StopTimer);
    }

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (timerRunning)
        {
            timer = Time.deltaTime + timer;

            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            timerText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        timer = 0f;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f; 
        if (timerText != null)
        {
            timerText.text = "00:00";
        }
    }
}
