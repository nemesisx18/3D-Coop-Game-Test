using System.Collections;
using UnityEngine;

public class BossAttackVariant1State : BaseBossState
{
    private bool hasAttacked = false;

    public override void EnterState(BossController boss)
    {
        base.EnterState(boss);

        boss.FireFlameEffect.SetActive(true);
        boss.FireFlameEffect.transform.localScale = new Vector3(0.1f, 0.02f, 0.1f);
    }

    public override void UpdateState(BossController boss)
    {
        base.UpdateState(boss);
        if (!hasAttacked)
        {
            boss.StartCoroutine(LaserUptime(boss));
        }
    }

    public override void ExitState(BossController boss)
    {
        base.ExitState(boss);
        hasAttacked = false;

        
    }

    private void Attack(BossController boss)
    {
        RaycastHit hit;
        Debug.DrawRay(boss.RaycastPoint.position, boss.transform.forward * 50f, Color.red, 2f);

        boss.FireFlameEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        if (Physics.Raycast(boss.RaycastPoint.position, boss.transform.forward, out hit, 50f))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnTakingDamage();
            }
        }
    }

    private IEnumerator LaserUptime(BossController boss)
    {
        hasAttacked = true;
        Attack(boss);
        yield return new WaitForSeconds(2.0f);
        boss.FireFlameEffect.transform.localScale = new Vector3(0.1f, 0.02f, 0.1f);
        boss.FireFlameEffect.SetActive(false);

        boss.ChangeState(new BossAttackVariant2State());
    }
}
