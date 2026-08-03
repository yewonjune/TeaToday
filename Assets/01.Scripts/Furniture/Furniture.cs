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


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (customerPoint == null)
        {
            Debug.LogWarning(
                $"[{name}] CustomerPoint가 연결되지 않았습니다.",
                this);
        }
    }
#endif
}
