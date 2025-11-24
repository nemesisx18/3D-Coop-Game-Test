using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterData : MonoBehaviour, IDamageable
{
    [SerializeField] private int characterHealth = 3;

    [SerializeField] private Canvas characterCanvas;

    private CharacterMotor characterMotor;

    private int characterIndex;

    private bool isBeingTargeted = false;

    public Vector3 CharacterPosition { get; private set; }

    public int CharacterHealth => characterHealth;
    public int CharacterIndex => characterIndex;

    private float maxDistance = 5f;

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
            EventManager.TriggerEvent("CharacterDefeated", new CharacterDefeatedMessage(characterIndex));

            //TO:DO Disable character visuals and interactions
            gameObject.SetActive(false);
        }
    }

    public void OnActionKeyPressed()
    {
        //TO:DO Implement pickup rocket logic
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            if (hit.collider != null && hit.collider.CompareTag("Rocket"))
            {
                // Implement logic to pick up the rocket
                Debug.Log("Rocket picked up!");
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
}
