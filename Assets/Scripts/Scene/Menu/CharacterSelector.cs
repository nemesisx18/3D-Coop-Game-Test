using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private GameObject p1DonePanel;
    [SerializeField] private GameObject[] selectablePlayer1Objects;

    [SerializeField] private Button player1NextButton;
    [SerializeField] private Button player1PreviousButton;
    [SerializeField] private Button player1ConfirmButton;

    [SerializeField] private int selectedPlayer1Index = 0;

    [Space]
    [Header("Player 2")]
    [SerializeField] private GameObject p2DonePanel;
    [SerializeField] private GameObject[] selectablePlayer2Objects;

    [SerializeField] private Button player2NextButton;
    [SerializeField] private Button player2PreviousButton;
    [SerializeField] private Button player2ConfirmButton;

    [SerializeField] private int selectedPlayer2Index = 0;

    private void OnDisable()
    {
        p1DonePanel.SetActive(false);
        p2DonePanel.SetActive(false);
    }

    private void Start()
    {
        p1DonePanel.SetActive(false);
        p2DonePanel.SetActive(false);

        selectedPlayer1Index = SaveData.SaveDataInstance.Player1SelectedCharacter;
        selectedPlayer2Index = SaveData.SaveDataInstance.Player2SelectedCharacter;
        selectablePlayer1Objects[selectedPlayer1Index].SetActive(true);
        selectablePlayer2Objects[selectedPlayer2Index].SetActive(true);

        if (selectedPlayer1Index == 0)
        {
            player1PreviousButton.interactable = false;
        }

        if (selectedPlayer2Index == 0)
        {
            player2PreviousButton.interactable = false;
        }

        if (selectedPlayer1Index == selectablePlayer1Objects.Length - 1)
        {
            player1NextButton.interactable = false;
        }

        if (selectedPlayer2Index == selectablePlayer2Objects.Length - 1)
        {
            player2NextButton.interactable = false;
        }

        player1PreviousButton.onClick.RemoveAllListeners();
        player1NextButton.onClick.RemoveAllListeners();
        player2PreviousButton.onClick.RemoveAllListeners();
        player2NextButton.onClick.RemoveAllListeners();
        player1ConfirmButton.onClick.RemoveAllListeners();
        player2ConfirmButton.onClick.RemoveAllListeners();

        player1PreviousButton.onClick.AddListener(OnP1SelectPrevious);
        player1NextButton.onClick.AddListener(OnP1SelectNext);
        player2PreviousButton.onClick.AddListener(OnP2SelectPrevious);
        player2NextButton.onClick.AddListener(OnP2SelectNext);
        player1ConfirmButton.onClick.AddListener(OnP1ConfirmSelected);
        player2ConfirmButton.onClick.AddListener(OnP2ConfirmSelected);
    }

    private void OnP1SelectPrevious()
    {
        selectablePlayer1Objects[selectedPlayer1Index].SetActive(false);
        selectedPlayer1Index--;

        selectablePlayer1Objects[selectedPlayer1Index].SetActive(true);
        player1NextButton.interactable = true;
        if (selectedPlayer1Index == 0)
        {
            player1PreviousButton.interactable = false;
        }
    }

    private void OnP2SelectPrevious()
    {
        selectablePlayer2Objects[selectedPlayer2Index].SetActive(false);
        selectedPlayer2Index--;
        selectablePlayer2Objects[selectedPlayer2Index].SetActive(true);
        player2NextButton.interactable = true;
        if (selectedPlayer2Index == 0)
        {
            player2PreviousButton.interactable = false;
        }
    }

    private void OnP1SelectNext()
    {
        selectablePlayer1Objects[selectedPlayer1Index].SetActive(false);
        selectedPlayer1Index++;
        selectablePlayer1Objects[selectedPlayer1Index].SetActive(true);
        player1PreviousButton.interactable = true;
        if (selectedPlayer1Index == selectablePlayer1Objects.Length - 1)
        {
            player1NextButton.interactable = false;
        }
    }

    private void OnP2SelectNext()
    {
        selectablePlayer2Objects[selectedPlayer2Index].SetActive(false);
        selectedPlayer2Index++;
        selectablePlayer2Objects[selectedPlayer2Index].SetActive(true);
        player2PreviousButton.interactable = true;
        if (selectedPlayer2Index == selectablePlayer2Objects.Length - 1)
        {
            player2NextButton.interactable = false;
        }
    }

    private void OnP1ConfirmSelected()
    {
        EventManager.TriggerEvent("Player1CharacterSelected", selectedPlayer1Index);

        p1DonePanel.SetActive(true);
    }

    private void OnP2ConfirmSelected()
    {
        EventManager.TriggerEvent("Player2CharacterSelected", selectedPlayer2Index);

        p2DonePanel.SetActive(true);
    }
}
