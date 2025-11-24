using UnityEngine;

public class BossAttackVariant3State : BaseBossState
{
    private bool hasAttacked = false;

    public override void EnterState(BossController boss)
    {
        base.EnterState(boss);

        Debug.Log("Entered Attack Variant 3 State");

        EventManager.TriggerEvent("LockingPlayer", true);
    }

    public override void UpdateState(BossController boss)
    {
        base.UpdateState(boss);
    }

    public override void ExitState(BossController boss)
    {
        base.ExitState(boss);

        EventManager.TriggerEvent("LockingPlayer", false);
    }
}
