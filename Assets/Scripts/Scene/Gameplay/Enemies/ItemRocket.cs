using UnityEngine;

public class ItemRocket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnTakingDamage();
            gameObject.SetActive(false);
        }
    }
}
