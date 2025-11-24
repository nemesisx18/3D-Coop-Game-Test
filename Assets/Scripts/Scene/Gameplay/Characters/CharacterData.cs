using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterData : MonoBehaviour, IDamageable
{
    [SerializeField] private int characterHealth = 3;

    [SerializeField] private Canvas characterCanvas;

    [SerializeField] private Transform rocketPlaceholder;

    private CharacterMotor characterMotor;

    private int characterIndex;

    private bool isBeingTargeted = false;

    public Vector3 CharacterPosition { get; private set; }

    public int CharacterHealth => characterHealth;
    public int CharacterIndex => characterIndex;

    private float maxDistance = 10f;

    private void Start()
    {
        characterMotor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        if (isBeingTargeted)
        {
            CharacterPosition = transform.position;
        }

    }

    public void SetupCharacter(int index)
    {
        characterIndex = index;

        characterCanvas.worldCamera = Camera.main;
    }

    public void OnMove(Vector2 move)
    {
        characterMotor.MoveCharacter(move);
    }

    public void OnTakingDamage()
    {
        characterHealth--;
        EventManager.TriggerEvent("CharacterDamaged", new CharacterTakeDamageMessage(characterIndex, characterHealth));

        if (characterHealth <= 0)
        {
            EventManager.TriggerEvent("CharacterDefeated", characterIndex);

            gameObject.SetActive(false);
        }
    }

    public void OnActionKeyPressed()
    {
        switch (CheckRocketInPlaceholder())
        {
            case true:
                LaunchRocket();
                break;
            case false:
                InteractWithObject();
                break;
        }
    }

    private void InteractWithObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.OnInteract(rocketPlaceholder);
            }
        }
    }

    private void LaunchRocket()
    {
        if (rocketPlaceholder.childCount > 0)
        {
            Transform rocket = rocketPlaceholder.GetChild(0);
            PickableRocket rocketComponent = rocket.GetComponent<PickableRocket>();
            if (rocketComponent != null)
            {
                rocketComponent.Launch();
                rocket.SetParent(null);
            }
        }
    }

    public void OnBeingTargeted(bool isTargeted)
    {
        isBeingTargeted = isTargeted;

        if (characterCanvas.gameObject != null)
        {
            characterCanvas.gameObject.SetActive(isBeingTargeted);
        }
    }

    private bool CheckRocketInPlaceholder()
    {
        return rocketPlaceholder.childCount > 0;
    }
}
