using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [field: Range(0, 20), SerializeField] private float characterSpeed = 15f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveCharacter(Vector2 move)
    {
        rb.MovePosition(transform.position + new Vector3(move.x, 0, move.y) * Time.deltaTime * characterSpeed);
    }
}
