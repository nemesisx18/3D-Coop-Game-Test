using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private CharacterData[] characterDataPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    private List<CharacterData> spawnedCharacters = new List<CharacterData>();

    private int player1CharacterID;
    private int player2CharacterID;

    private void OnEnable()
    {
        EventManager.StartListening("Move", MoveCharacter);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Move", MoveCharacter);
    }

    private void Start()
    {
        player1CharacterID = SaveData.SaveDataInstance.Player1SelectedCharacter;
        player2CharacterID = SaveData.SaveDataInstance.Player2SelectedCharacter;

        SpawnCharacter();
    }

    private void SpawnCharacter()
    {
        CharacterData character_1 = Instantiate(characterDataPrefabs[player1CharacterID], spawnPoints[0].position, Quaternion.identity);
        character_1.SetupCharacter(0);
        spawnedCharacters.Add(character_1);

        CharacterData character_2 = Instantiate(characterDataPrefabs[player2CharacterID], spawnPoints[1].position, Quaternion.identity);
        character_2.SetupCharacter(1);
        spawnedCharacters.Add(character_2);
    }

    private void MoveCharacter(object message)
    {
        MoveMessage moveMessage = (MoveMessage)message;

        spawnedCharacters[moveMessage.PlayerId].OnMove(moveMessage.Move);
    }
}
