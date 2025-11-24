using System.Collections;
using UnityEngine;

public class BossAttackVariant3State : BaseBossState
{
    public override void EnterState(BossController boss)
    {
        base.EnterState(boss);

        EventManager.TriggerEvent("LockingPlayer", true);

        boss.StartCoroutine(ChangeStateAfterTime());
    }

    public override void UpdateState(BossController boss)
    {
        base.UpdateState(boss);
    }

    public override void ExitState(BossController boss)
    {
        base.ExitState(boss);
    }

    private IEnumerator ChangeStateAfterTime()
    {
        yield return new WaitForSeconds(2f);
        EventManager.TriggerEvent("LockingPlayer", false);

        yield return new WaitForSeconds(3f);
        EventManager.TriggerEvent("OnRocketHit");
    }
}
