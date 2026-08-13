using UnityEngine;

public class CraftPanelController : MonoBehaviour
{
    [SerializeField] private CraftingUI craftingUI;

    //음료 제작 UI 열기
    public void OpenDrinkCraftPanel()
    {
        craftingUI.Open(ProductType.Drink);
    }
    public void OpenFoodCraftPanel()
    {
        craftingUI.Open(ProductType.Food);
    }

    public void CloseDrinkCraftPanel()
    {
        craftingUI.gameObject.SetActive(false);
    }
}
