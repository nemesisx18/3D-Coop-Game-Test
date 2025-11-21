using UnityEngine;

[System.Serializable]
public struct InputConfig
{
    [SerializeField] private KeyCode moveUp;
    [SerializeField] private KeyCode moveDown;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode actionKey;

    public KeyCode MoveUp => moveUp;
    public KeyCode MoveDown => moveDown;
    public KeyCode MoveLeft => moveLeft;
    public KeyCode MoveRight => moveRight;
    public KeyCode ActionKey => actionKey;
}
