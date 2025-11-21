using UnityEngine;

public class BossAttackVariant3State : BaseBossState
{
    public override void EnterState(BossController boss)
    {
        base.EnterState(boss);

        Debug.Log("BossAttackVariant1State: Entered Attack Variant 3 State");
    }

    public override void UpdateState(BossController boss)
    {
        base.UpdateState(boss);
    }

    public override void ExitState(BossController boss)
    {
        base.ExitState(boss);
    }
}
