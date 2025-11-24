public class BossAttackVariant3State : BaseBossState
{
    public override void EnterState(BossController boss)
    {
        base.EnterState(boss);

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
