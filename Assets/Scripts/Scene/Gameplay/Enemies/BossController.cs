using UnityEngine;

public class BossController : MonoBehaviour
{
    public Rigidbody Rb;
    public bool IsFlying = false;

    public float DefaultHeight;
    public float MaxHeight = 12f;

    private IBossState currentState;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();

        DefaultHeight = transform.position.y;

        ChangeState(new BossMovingState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IBossState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        currentState.EnterState(this);
    }

}
