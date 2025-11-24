using UnityEngine;

public struct GameResultMessage
{
    public bool ResultBool;
    public float GameTime;

    public GameResultMessage(bool isWin, float gameTime)
    {
        ResultBool = isWin;
        GameTime = gameTime;
    }
}
