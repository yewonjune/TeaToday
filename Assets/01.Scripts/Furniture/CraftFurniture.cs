using UnityEngine;
using UnityEngine.UI;

public class CraftFurniture : Furniture
{
    [SerializeField] private Button craftButton;
    [SerializeField] private CraftingUI craftingUI;

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
        if (craftingUI == null)
        {
            Debug.LogError($"[{name}] CraftingUI가 없습니다.");
            return;
        }

        switch (FurnitureType)
        {
            case FurnitureType.DrinkCraftMachine:
                craftingUI.Open(ProductType.Drink);
                break;

            case FurnitureType.FoodCraftMachine:
                craftingUI.Open(ProductType.Food);
                break;

            default:
                Debug.LogWarning(
                    $"[{name}] 제작 가구가 아닙니다. Type: {FurnitureType}");
                break;
        }
    }
}
