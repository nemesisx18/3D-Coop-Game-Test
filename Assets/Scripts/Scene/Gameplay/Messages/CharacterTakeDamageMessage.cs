using UnityEngine;

public struct CharacterTakeDamageMessage 
{
    public int CharacterIndex { get; }
    public int RemainingHealth { get; }

    public CharacterTakeDamageMessage(int characterIndex, int remainingHealth)
    {
        CharacterIndex = characterIndex;
        RemainingHealth = remainingHealth;
    }
}
