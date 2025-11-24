using UnityEngine;

public class PickableRocket : BaseRocket
{
    private float minXPosition = -15f;
    private float maxXPosition = 15f;
    private float minZPosition = -12f;
    private float maxZPosition = 12f;
    private float xTargetPosition;
    private float zTargetPosition;

    [SerializeField] private Vector3 throwTarget;

    private void FixedUpdate()
    {
        Move(throwTarget);
    }

    public void ThrowRocket()
    {
        xTargetPosition = Random.Range(minXPosition, maxXPosition);
        zTargetPosition = Random.Range(minZPosition, maxZPosition);

        throwTarget = new Vector3(xTargetPosition, 1f, zTargetPosition);
    }
}
