using UnityEngine;
using UnityEngine.UI;

public class CraftFurniture : Furniture
{
    [SerializeField] private Button craftButton;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (craftButton != null)
            craftButton.onClick.AddListener(OpenCraftPanel);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (craftButton != null)
            craftButton.onClick.RemoveListener(OpenCraftPanel);
    }

    private void OpenCraftPanel()
    {
        if (CraftingUI.Instance == null)
        {
            Debug.LogError($"[{name}] CraftingUI가 없습니다.");
            return;
        }

        switch (FurnitureType)
        {
            case FurnitureType.DrinkCraftMachine:
                CraftingUI.Instance.Open(ProductType.Drink);
                break;

            case FurnitureType.FoodCraftMachine:
                CraftingUI.Instance.Open(ProductType.Food);
                break;

            default:
                Debug.LogWarning(
                    $"[{name}] 제작 가구가 아닙니다. Type: {FurnitureType}");
                break;
        }
    }
}
