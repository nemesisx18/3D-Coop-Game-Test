using UnityEngine;

public class PickableRocket : BaseRocket, IInteractable
{
    private float minXPosition = -15f;
    private float maxXPosition = 15f;
    private float minZPosition = -12f;
    private float maxZPosition = 12f;
    private float xTargetPosition;
    private float zTargetPosition;

    private Vector3 throwTarget;

    private BossController bossController;

    private Transform targetParent;

    private bool canMove = false;
    private bool onInteract = false;
    private bool explosive = false;

    private void Start()
    {
        GameObject boss = GameObject.FindWithTag("Enemy");
        bossController = boss.GetComponent<BossController>();
    }

    private void Update()
    {
        if (explosive)
        {
            throwTarget = bossController.BossPosition;
        }

        if (onInteract)
        {
            transform.localPosition = Vector3.zero;
        }

        if (Vector3.Distance(transform.position, throwTarget) < 0.1f)
        {
            canMove = false;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move(throwTarget);
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
        if (bossController != null)
        {
            onInteract = false;
            canMove = true;
            explosive = true;
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
        if (other.gameObject.CompareTag("Enemy") && explosive)
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
