using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int remainingCharacters = 2;

    private void OnEnable()
    {
        EventManager.StartListening("CharacterDefeated", OnCharacterDefeated);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CharacterDefeated", OnCharacterDefeated);
    }

    private void OnCharacterDefeated(object message)
    {
        remainingCharacters--;
        if (remainingCharacters <= 0)
        {
            Debug.Log("All characters defeated! Game Over.");
        }
    }
}
