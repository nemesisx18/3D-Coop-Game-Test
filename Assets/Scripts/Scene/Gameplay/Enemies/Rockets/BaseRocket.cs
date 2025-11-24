using UnityEngine;

public class BaseRocket : MonoBehaviour
{
    protected float rocketSpeed = 10f;

    protected virtual void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, rocketSpeed * Time.deltaTime);
    }
}
