using UnityEngine;

public class ExplosiveRocket : BaseRocket
{
    [SerializeField] private CharacterData targetPlayerPosition;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        Move(targetPlayerPosition.CharacterPosition);
    }

    public void LaunchRocket(CharacterData target)
    {
        targetPlayerPosition = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnTakingDamage();

                Destroy(gameObject);
            }
        }
    }
}
