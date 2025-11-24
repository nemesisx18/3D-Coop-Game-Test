using UnityEngine;

public class ItemRocket : MonoBehaviour
{
    [field: Range(1f, 20f), SerializeField] private float rocketSpeed;

    private float minXPosition = -15f;
    private float maxXPosition = 15f;
    private float minZPosition = -12f;
    private float maxZPosition = 12f;
    private float xTargetPosition;
    private float zTargetPosition;

    private bool isTargetingPlayer = false;

    [SerializeField] private Vector3 throwTarget;
    [SerializeField] private CharacterData targetPlayerPosition;

    private void FixedUpdate()
    {
        if (!isTargetingPlayer)
        {
            Move(throwTarget);
        }
        else
        {
            Move(targetPlayerPosition.CharacterPosition);
        }
    }

    public void ThrowRocket()
    {
        xTargetPosition = Random.Range(minXPosition, maxXPosition);
        zTargetPosition = Random.Range(minZPosition, maxZPosition);

        throwTarget = new Vector3(xTargetPosition, 1f, zTargetPosition);
    }

    public void LaunchRocket(bool isLockingPlayer, CharacterData target)
    {
        isTargetingPlayer = isLockingPlayer;

        targetPlayerPosition = target;
    }

    private void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, rocketSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnTakingDamage();

            EventManager.TriggerEvent("OnRocketHit");
            gameObject.SetActive(false);
        }
    }
}
