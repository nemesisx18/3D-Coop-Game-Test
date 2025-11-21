public interface IBossState
{
    public void EnterState(BossController boss);
    public void UpdateState(BossController boss);
    public void ExitState(BossController boss);
}
