using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;

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
    }

    private void Update()
    {

    }

    private void OnCharacterDefeated(object message)
    {
        remainingCharacters--;
        if (remainingCharacters <= 0)
        {
            Debug.Log("All characters defeated! Game Over.");

            EndGame("Lose");
        }
    }

    private void EndGame(object result)
    {
        string resultMessage = (string)result;

        gameFinalTime = gameTimer.Timer;
        
        switch(result)
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
