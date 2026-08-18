using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NUnit.Framework.Internal.OSPlatform;

//어떤 상품을 고르고 어떤 머신에서 만들지 전달
public class CraftingUI : MonoBehaviour
{
    public static CraftingUI Instance { get; private set; }

    [Header("Panel")]
    [SerializeField] private GameObject productCraftPanel;

    [Header("상품 목록")]
    [SerializeField] private Transform content;
    [SerializeField] private ProductSlotUI productSlotPrefab;
    [SerializeField] private List<ProductData> products;

    [Header("상단")]
    [SerializeField] private TMP_Text titleText;

    [Header("상세 정보")]
    [SerializeField] private Image detailProductIcon;
    [SerializeField] private TMP_Text detailProductName;
    [SerializeField] private TMP_Text detailCraftTime;
    [SerializeField] private TMP_Text detailMaterialCost;
    [SerializeField] private Button craftButton;

    private ProductData selectedProduct;
    private CraftFurniture currentMachine;

    private void Awake()
    {
        Instance = this;

        craftButton.onClick.AddListener(CraftSelectedProduct);
    }

    public void Open(ProductType productType, CraftFurniture machine)
    {
        currentMachine = machine;

        productCraftPanel.SetActive(true);

        // 제목 변경
        titleText.text =
            productType == ProductType.Drink
            ? "DRINK"
            : "FOOD";

        // 이전 상품 선택 초기화
        selectedProduct = null;

        // 기존 슬롯 제거
        CloseProductSlots();

        // 타입에 맞는 상품 생성
        CreateProductSlots(productType);
    }

    //private void Start()
    //{
    //    CreateProductSlots();

    //    craftButton.onClick.AddListener(CraftSelectedProduct);
    //}

    public void Close()
    {
        productCraftPanel.SetActive(false);
    }

    private void CreateProductSlots(ProductType productType)
    {
        foreach (ProductData product in products)
        {
            if (product.productType != productType)
                continue;

            ProductSlotUI slot =
                Instantiate(productSlotPrefab, content);

            slot.Initialize(product, this);
        }
    }

    private void CloseProductSlots()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    public void SelectProduct(ProductData product)
    {
        selectedProduct = product;

        detailProductIcon.sprite = product.icon;
        detailProductName.text = product.productName;
        detailCraftTime.text = $"{product.craftTime}초";
        detailMaterialCost.text = $"{product.materialCost}";
    }

    private void CraftSelectedProduct()
    {
        if (selectedProduct == null)
        {
            Debug.Log("[CraftingUI] 선택된 상품이 없습니다.");
            return;
        }

        CraftingManager.Instance.TryCraft(selectedProduct, currentMachine);
    }
}
