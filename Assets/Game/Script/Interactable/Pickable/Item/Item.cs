using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, Iinteractable, IPickable
{
    [SerializeField]
    public ItemData _data;

    public UnityEvent OnItemPicked;
    public string Name => _data.Name;

    [ContextMenu("intercet item")]

    public void Interact(PlayerCharacher character)
    {
        Pickup(character);
    }

    public void Pickup(PlayerCharacher character)
    {
        ItemData newData = new ItemData(_data.ID, _data.Name);
        character.Inventory.AddItems(newData);
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}
