using System.Collections;
using UnityEngine;

public class BossAttackVariant2State : BaseBossState
{
    private float minXPosition = -15f;
    private float maxXPosition = 15f;
    private float minZPosition = -12f;
    private float maxZPosition = 12f;
    private float xTargetPosition;
    private float zTargetPosition;

    private Vector3 flyPos;
    private Vector3 targetPosition;
    private Vector3 originalPosition;

    private bool canMove = true;
    private bool returnToOriginal = false;
    private bool hasAttacked = false;

    public override void EnterState(BossController boss)
    {
        base.EnterState(boss);

        hasAttacked = false;
        returnToOriginal = false;

        originalPosition = boss.transform.position;

        boss.Rb.isKinematic = true;

        xTargetPosition = Random.Range(minXPosition, maxXPosition);
        zTargetPosition = Random.Range(minZPosition, maxZPosition);

        flyPos = new Vector3(boss.transform.position.x, 30f, boss.transform.position.z);
        targetPosition = new Vector3(xTargetPosition, 30f, zTargetPosition);

        boss.EagleAttackStrikeArea.transform.position = new Vector3(xTargetPosition, boss.EagleAttackStrikeArea.transform.position.y, zTargetPosition);
        boss.EagleAttackStrikeArea.SetActive(true);
    }

    public override void UpdateState(BossController boss)
    {
        base.UpdateState(boss);

        if (canMove)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, flyPos, 50f * Time.deltaTime);
        }
        if (Vector3.Distance(boss.transform.position, flyPos) < 0.1f)
        {
            canMove = false;
            boss.transform.position = targetPosition;
        }

        if (returnToOriginal)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, originalPosition, 15f * Time.deltaTime);

            if (Vector3.Distance(boss.transform.position, originalPosition) < 0.1f)
            {
                boss.ChangeState(new BossAttackVariant3State());

                returnToOriginal = false;
            }
        }

        if (hasAttacked)
        {
            return;
        }

        boss.StartCoroutine(AttackDelayCoroutine(boss, 3.0f));
    }

    public override void ExitState(BossController boss)
    {
        base.ExitState(boss);

        boss.EagleAttackStrikeArea.SetActive(false);
    }

    private IEnumerator AttackDelayCoroutine(BossController boss, float delay)
    {
        hasAttacked = true;
        yield return new WaitForSeconds(delay);

        boss.Rb.isKinematic = false;
        boss.Rb.AddForce(Vector3.down * 10000f, ForceMode.Acceleration);

        if (boss.Rb.angularVelocity == Vector3.zero)
        {
            yield return new WaitForSeconds(1f);
            returnToOriginal = true;
        }
    }
}
