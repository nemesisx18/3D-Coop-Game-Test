using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterData : MonoBehaviour, IDamageable
{
    [SerializeField] private int characterHealth = 3;
    
    private CharacterMotor characterMotor;

    private int characterIndex;

    public int CharacterHealth => characterHealth;
    public int CharacterIndex => characterIndex;

    private void Start()
    {
        characterMotor = GetComponent<CharacterMotor>();
    }

    public void SetupCharacter(int index)
    {
        characterIndex = index;
    }

    public void OnMove(Vector2 move)
    {
        characterMotor.MoveCharacter(move);
    }

    public void OnTakingDamage()
    {
        characterHealth--;
        EventManager.TriggerEvent("CharacterDamaged", new CharacterTakeDamageMessage(characterIndex, characterHealth));

        if (characterHealth <= 0)
        {
            EventManager.TriggerEvent("CharacterDefeated", new CharacterDefeatedMessage(characterIndex));

            //TO:DO Disable character visuals and interactions
        }
    }
}
