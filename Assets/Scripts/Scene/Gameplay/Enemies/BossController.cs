using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class BossController : MonoBehaviour
{
    public Rigidbody Rb;
    public Transform RaycastPoint;

    public GameObject FireFlameEffect;
    public GameObject EagleAttackStrikeArea;

    public bool IsFlying = false;

    public float DefaultHeight;
    public float MaxHeight = 12f;

    public Transform BossPosition { get; private set; }

    private IBossState currentState;

    private void OnEnable()
    {
        EventManager.StartListening("OnRocketHit", OnRocketExploded);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnRocketHit", OnRocketExploded);
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();

        DefaultHeight = transform.position.y;

        ChangeState(new BossMovingState());
    }

    private void Update()
    {
        BossPosition = this.transform;

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

    private void OnRocketExploded()
    {
        if (currentState != null)
        {
            ChangeState(new BossMovingState());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnTakingDamage();
            }
        }
    }
}
