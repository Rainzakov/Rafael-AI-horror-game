using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacterMovement _movement;

    [SerializeField]
    private PlayerCharacherStamina _stamina;

    [SerializeField]
    private InventortManager _inventory;

    public PlayerCharacterMovement Movement => _movement;
    public PlayerCharacherStamina Stamina => _stamina;
    public InventortManager Inventort => _inventory;
}
