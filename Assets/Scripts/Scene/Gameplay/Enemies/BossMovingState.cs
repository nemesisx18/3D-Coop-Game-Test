using UnityEngine;

public class BossMovingState : BaseBossState
{
    private float moveSpeed = 10f;

    private float newXPosition;
    private float minXPosition = -20f;
    private float maxXPosition = 20f;

    private Vector3 targetPosition;

    public override void EnterState(BossController boss)
    {
        base.EnterState(boss);

        newXPosition = Random.Range(minXPosition, maxXPosition);

        targetPosition = new Vector3(newXPosition, boss.MaxHeight, boss.transform.position.z);

        boss.Rb.isKinematic = true;
    }

    public override void UpdateState(BossController boss)
    {
        base.UpdateState(boss);

        boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(boss.transform.position, targetPosition) < 0.1f)
        {
            boss.ChangeState(new BossAttackVariant1State());
        }
    }

    public override void ExitState(BossController boss)
    {
        base.ExitState(boss);

        boss.Rb.isKinematic = false;
        boss.transform.position = new Vector3(boss.transform.position.x, boss.DefaultHeight, boss.transform.position.z);
    }
}
