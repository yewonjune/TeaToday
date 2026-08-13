using System.Collections;
using UnityEngine;

// 음료/음식 제작관리
public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance { get; private set; }

    // 다향조각 ID
    private const int DAHYANG_PIECE_ID = 3001;

    private bool isCrafting;
    
    // 현재 제작 중인지 확인
    public bool IsCrafting => isCrafting;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    // 상품제작 시도
    public void TryCraft(ProductData product)
    {
        if(product == null)
        {
            Debug.LogError("[CraftingManager] ProductData가 없습니다.");
            return;
        }

        // 이미 다른 상품을 제작 중
        if (isCrafting)
        {
            Debug.Log("[CraftingManager] 이미 제작 중입니다.");
            return;
        }

        // 다향조각이 충분한지 확인
        if (!InventoryManager.Instance.HasItem(
                DAHYANG_PIECE_ID,
                product.materialCost))
        {
            Debug.Log("[CraftingManager] 다향조각이 부족합니다.");
            return;
        }

        InventoryManager.Instance.RemoveItem(DAHYANG_PIECE_ID, product.materialCost);

        StartCoroutine(Craft(product));

    }

    // 상품제작
    private IEnumerator Craft(ProductData product)
    {
        isCrafting = true;

        Debug.Log($"[CraftingManager] {product.productName} 제작 시작");

        yield return new WaitForSeconds(product.craftTime);

        InventoryManager.Instance.AddItem(product.productId, 1);

        Debug.Log($"[CraftingManager] {product.productName} 제작 완료");

        isCrafting = false;
    }
}
