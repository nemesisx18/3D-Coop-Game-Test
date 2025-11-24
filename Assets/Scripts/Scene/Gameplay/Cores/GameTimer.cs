using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public float Timer => timer;    

    private float timer;
    private bool timerRunning = false;

    private void OnEnable()
    {
        EventManager.StartListening("GameOver", StopTimer);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameOver", StopTimer);
    }

    void Start()
    {
        // Optionally start the timer immediately
        StartTimer();
    }

    void Update()
    {
        if (timerRunning)
        {
            timer = Time.deltaTime + timer;

            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            // Update the UI Text
            timerText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        timer = Time.time;
        timerRunning = true;
    }

    public void StopTimer(object message)
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f; // Resets the start time to the current time
        if (timerText != null)
        {
            timerText.text = "00:00"; // Resets the display
        }
    }
}
