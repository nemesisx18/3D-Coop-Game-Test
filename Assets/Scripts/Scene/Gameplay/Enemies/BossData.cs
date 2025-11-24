using UnityEngine;

public class BossData : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;

    [ContextMenu("Damage boss")]
    public void OnTakingDamage()
    {
        maxHealth--;

        if (maxHealth <= 0)
        {
            Debug.Log("Boss defeated!");
            Destroy(gameObject);

            EventManager.TriggerEvent("GameOver", "Win");
        }
        else
        {
            Debug.Log("Boss took damage! Remaining health: " + maxHealth);
        }
    }
}
