using UnityEngine;

public class Furniture : MonoBehaviour
{
    [Header("Futniture")]
    [SerializeField] private FurnitureType furnitureType;

    [Header("Customer")]
    [SerializeField] private Transform customerPoint;

    public FurnitureType FurnitureType => furnitureType;
    public Transform CustomerPoint => customerPoint;

    protected virtual void OnEnable()
    {
        FurnitureManager.Instance?.Register(this);
    }

    protected virtual void OnDisable()
    {
        FurnitureManager.Instance?.Unregister(this);
    }
}
