using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductSlotUI : MonoBehaviour
{
    [SerializeField] private TMP_Text productNameText;
    [SerializeField] private Image productImage;
    [SerializeField] private TMP_Text craftTimeText;
    [SerializeField] private TMP_Text materialCostText;
    [SerializeField] private Button button;

    private ProductData productData;
    private CraftingUI craftingUI;

    public void Initialize(ProductData product, CraftingUI ui)
    {
        productData = product;
        craftingUI = ui;

        productNameText.text = product.productName;
        productImage.sprite = product.icon;
        craftTimeText.text = $"{product.craftTime}√ ";
        materialCostText.text = $"{product.materialCost}";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickSlot);
    }

    private void OnClickSlot()
    {
        craftingUI.SelectProduct(productData);
    }
}