using UnityEngine;

public struct MoveMessage
{
    public Vector2 Move { get; }
    public int PlayerId { get; }

    public MoveMessage(Vector2 move, int playerId)
    {
        Move = move;
        PlayerId = playerId;
    }
}
