using UnityEngine;

public class ExplosiveRocket : BaseRocket
{
    [SerializeField] private CharacterData targetPlayerPosition;

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
        //IDamageable damageable = other.GetComponent<IDamageable>();
        //if (damageable != null)
        //{
        //    damageable.OnTakingDamage();

        //    EventManager.TriggerEvent("OnRocketHit");
        //    gameObject.SetActive(false);
        //}
    }
}
