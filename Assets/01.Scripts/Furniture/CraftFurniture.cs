using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static NUnit.Framework.Internal.OSPlatform;

// 해당 머신의 버튼, 제작중 아이콘, 제작 상태만 관리
public class CraftFurniture : Furniture
{
    [SerializeField] private Button craftButton;
    [SerializeField] private Image craftingIcon;

    private bool isCrafting;

    public bool IsCrafting => isCrafting;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (craftButton != null)
            craftButton.onClick.AddListener(OpenCraftPanel);

        if(craftingIcon != null)
            craftingIcon.gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (craftButton != null)
            craftButton.onClick.RemoveListener(OpenCraftPanel);
    }

    private void OpenCraftPanel()
    {
        if (isCrafting)
            return;

        if (CraftingUI.Instance == null)
        {
            Debug.LogError($"[{name}] CraftingUI가 없습니다.");
            return;
        }

        ProductType productType;

        switch (FurnitureType)
        {
            case FurnitureType.DrinkCraftMachine:
                productType = ProductType.Drink;
                break;

            case FurnitureType.FoodCraftMachine:
                productType = ProductType.Food;
                break;

            default:
                Debug.LogWarning(
                    $"[{name}] 제작 가구가 아닙니다. Type: {FurnitureType}");
                return;
        }

        CraftingUI.Instance.Open(productType, this);
    }

    // 제작 시작 시 머신 UI 변경
    public void BeginCraft(ProductData product)
    {
        isCrafting = true;

        craftButton.gameObject.SetActive(false);

        if (craftingIcon != null)
        {
            craftingIcon.sprite = product.icon;
            craftingIcon.gameObject.SetActive(true);
        }
    }

    // 제작 완료 시 머신 UI 원상복구
    public void FinishCraft()
    {
        if (craftingIcon != null)
            craftingIcon.gameObject.SetActive(false);

        craftButton.gameObject.SetActive(true);

        isCrafting = false;
    }

    //public void StartCraft(ProductData product)
    //{
    //    if (isCrafting)
    //        return;

    //    StartCoroutine(CraftRoutine(product));
    //}

    //private IEnumerator CraftRoutine(ProductData product)
    //{
    //    isCrafting = true;

    //    craftButton.gameObject.SetActive(false);

    //    //제작 중 Icon 띄우기
    //    if(craftingIcon != null)
    //    {
    //        craftingIcon.sprite = product.icon;
    //        craftingIcon.gameObject.SetActive(true);
    //    }

    //    yield return new WaitForSeconds(product.craftTime);

    //    InventoryManager.Instance.AddItem(product.productId, 1);

    //    if (craftingIcon != null)
    //        craftingIcon.gameObject.SetActive(false);

    //    craftButton.gameObject.SetActive(true);

    //    isCrafting = false;
    //}
}
