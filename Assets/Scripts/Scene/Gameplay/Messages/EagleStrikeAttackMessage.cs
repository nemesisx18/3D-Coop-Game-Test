using UnityEngine;

public struct EagleStrikeAttackMessage
{
    public int TargetPlayerIndex;
    public bool IsTargeting;

    public EagleStrikeAttackMessage(int targetPlayerIndex, bool isTargeting)
    {
        TargetPlayerIndex = targetPlayerIndex;
        IsTargeting = isTargeting;
    }
}
