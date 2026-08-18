using UnityEngine;
using UnityEngine.InputSystem;

public class CraftTest : MonoBehaviour
{
    [SerializeField] private ProductData product;
    [SerializeField] private CraftFurniture currentMachine;

    private void Start()
    {
        // 테스트용 다향조각 100개 지급
        InventoryManager.Instance.AddItem(3001, 100);
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CraftingManager.Instance.TryCraft(product, currentMachine);
        }
    }
    
}
