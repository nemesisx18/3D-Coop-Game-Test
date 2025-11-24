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
        RandomizeNextPosition(boss);

        boss.Rb.isKinematic = true;
    }

    public override void UpdateState(BossController boss)
    {
        base.UpdateState(boss);
        Move(boss);
        ChangeToNextState(boss);
    }

    public override void ExitState(BossController boss)
    {
        base.ExitState(boss);

        boss.Rb.isKinematic = false;
        boss.transform.position = new Vector3(boss.transform.position.x, boss.DefaultHeight, boss.transform.position.z);
    }

    private void RandomizeNextPosition(BossController boss)
    {
        newXPosition = Random.Range(minXPosition, maxXPosition);

        targetPosition = new Vector3(newXPosition, boss.MaxHeight, boss.transform.position.z);
    }

    private void Move(BossController boss)
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void ChangeToNextState(BossController boss)
    {
        if (Vector3.Distance(boss.transform.position, targetPosition) < 0.1f)
        {
            boss.ChangeState(new BossAttackVariant1State());
        }
    }
}
