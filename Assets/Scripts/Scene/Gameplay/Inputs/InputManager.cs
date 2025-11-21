using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputConfig[] inputConfigs;

    private void Update()
    {
        for (int i = 0; i < inputConfigs.Length; i++)
        {
            var inputConfig = inputConfigs[i];

            if (Input.GetKey(inputConfig.MoveUp))
            {
                EventManager.TriggerEvent("Move", new MoveMessage(Vector2.up, i));
            }
            if (Input.GetKey(inputConfig.MoveDown))
            {
                EventManager.TriggerEvent("Move", new MoveMessage(Vector2.down, i));
            }
            if (Input.GetKey(inputConfig.MoveLeft))
            {
                EventManager.TriggerEvent("Move", new MoveMessage(Vector2.left, i));
            }
            if (Input.GetKey(inputConfig.MoveRight))
            {
                EventManager.TriggerEvent("Move", new MoveMessage(Vector2.right, i));
            }
        }
    }
}
