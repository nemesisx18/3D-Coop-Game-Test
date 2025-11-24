using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;

    private bool isP1Alive = true;
    private bool isP2Alive = true;

    private float gameFinalTime;

    private int remainingCharacters = 2;

    private void OnEnable()
    {
        EventManager.StartListening("CharacterDefeated", OnCharacterDefeated);
        EventManager.StartListening("GameOver", EndGame);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CharacterDefeated", OnCharacterDefeated);
        EventManager.StopListening("GameOver", EndGame);

        Time.timeScale = 1;
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnCharacterDefeated(object message)
    {
        int charaID = (int)message;

        switch (charaID)
        {
            case 0:
                isP1Alive = false;
                remainingCharacters--; 
                break;
            case 1:
                isP2Alive = false;
                remainingCharacters--; 
                break;
        }

        if (!isP1Alive && !isP2Alive)
        {
            EndGame("Lose");
        }
    }

    private void EndGame(object result)
    {
        string resultMessage = (string)result;

        gameFinalTime = gameTimer.Timer;

        Time.timeScale = 0;

        switch (result)
        {
            case "Win":
                EventManager.TriggerEvent("GameResult", new GameResultMessage(true, gameFinalTime));
                break;
            case "Lose":
                EventManager.TriggerEvent("GameResult", new GameResultMessage(false, gameFinalTime));
                break;
            default:
                Debug.LogError("Invalid game result: " + resultMessage);
                break;
        }
    }
}
