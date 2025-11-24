using UnityEngine;

public class PickableRocket : BaseRocket, IInteractable
{
    private float minXPosition = -15f;
    private float maxXPosition = 15f;
    private float minZPosition = -12f;
    private float maxZPosition = 12f;
    private float xTargetPosition;
    private float zTargetPosition;

    [SerializeField] private Vector3 throwTarget;

    private Transform targetParent;

    private CapsuleCollider rocketCollider;

    private bool canMove = false;
    private bool onInteract = false;

    private void Start()
    {
        rocketCollider = GetComponent<CapsuleCollider>();
        rocketCollider.isTrigger = false;
    }

    private void Update()
    {
        if (canMove)
        {
            Move(throwTarget);
        }

        if(onInteract)
        {
            transform.localPosition = Vector3.zero;
        }

        if (Vector3.Distance(transform.position, throwTarget) < 0.1f)
        {
            canMove = false;
        }
    }

    public void ThrowRocket()
    {
        xTargetPosition = Random.Range(minXPosition, maxXPosition);
        zTargetPosition = Random.Range(minZPosition, maxZPosition);

        throwTarget = new Vector3(xTargetPosition, 1f, zTargetPosition);

        canMove = true;
    }

    public void Launch()
    {
        GameObject boss = GameObject.FindWithTag("Enemy");

        if (boss != null)
        {
            BossController bossController = boss.GetComponent<BossController>();
            if (bossController != null)
            {
                throwTarget = bossController.BossPosition.position;

                onInteract = false;
                rocketCollider.isTrigger = true;
                canMove = true;
            }
        }
    }

    public void OnInteract(Transform target)
    {
        targetParent = target;
        onInteract = true;

        transform.SetParent(targetParent);
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnTakingDamage();
                Destroy(gameObject);
            }
        }
        
    }
}
