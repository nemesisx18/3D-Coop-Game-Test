using System.Collections;
using UnityEngine;

public class ExplosiveRocket : BaseRocket
{
    [SerializeField] private CharacterData targetPlayerPosition;

    private void OnDisable()
    {
        EventManager.TriggerEvent("OnRocketHit");
    }

    private void FixedUpdate()
    {
        Move(targetPlayerPosition.CharacterPosition);

        if (Vector3.Distance(transform.position, targetPlayerPosition.CharacterPosition) < 0.5f)
        {
            Debug.Log("Stop tracking target");
        }
    }

    public void LaunchRocket(CharacterData target)
    {
        targetPlayerPosition = target;
    }

    private IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(0.75f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnTakingDamage();

                gameObject.SetActive(false);
            }
        }
    }
}
