using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    public Rigidbody Rb;
    public Transform RaycastPoint;

    public GameObject FireFlameEffect;
    public GameObject EagleAttackStrikeArea;

    public float DefaultHeight;
    public float MaxHeight = 12f;

    public Vector3 BossPosition { get; private set; }

    private IBossState currentState;

    private UnityAction onRocketExplode;

    private void OnEnable()
    {
        EventManager.StartListening("OnRocketHit", onRocketExplode);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnRocketHit", onRocketExplode);
    }

    private void Awake()
    {
        onRocketExplode = new UnityAction(OnRocketExploded);
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();

        DefaultHeight = transform.position.y;

        ChangeState(new BossMovingState());
    }

    private void Update()
    {
        BossPosition = transform.position;

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
