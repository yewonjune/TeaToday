using System.Collections.Generic;
using UnityEngine;

//보유한 아이템 수량 관리
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {  get; private set; }

    // Key: 아이템 ID
    // Value : 보유 수량
    private Dictionary<int,int> inventory = new Dictionary<int, int>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }    
        Instance = this;
    }

    // 아이템 추가
    public void Additem(int itemId, int amount)
    {
        if (amount <= 0)
            return;

        if(inventory.ContainsKey(itemId))
        {
            inventory[itemId] += amount;
        }
        else
        {
            inventory.Add(itemId, amount);
        }

        Debug.Log($"[Inventory] {itemId} +{amount} / 현재: {inventory[itemId]}");
    }

    // 아이템 사용
    public bool RemoveItem(int itemId, int amount)
    {
        if(amount <= 0)
            return false;

        if(!HasItem(itemId, amount))
        {
            Debug.Log($"[Inventory] {itemId} 수량 부족");
            return false;
        }

        inventory[itemId] -= amount;

        Debug.Log($"[Inventory] {itemId} -{amount} / 현재: {inventory[itemId]}");

        return true;
    }

    // 필요한 수량을 가지고 있는지 확인
    public bool HasItem(int itemId, int amount)
    {
        return inventory.TryGetValue(itemId, out int currentAmount)
            && amount <= currentAmount;
    }

    // 현재 보유 수량 가져오기
    public int GetItemAmount(int itemId)
    {
        if(inventory.TryGetValue(itemId,out int amount))
            return amount;

        return 0;
    }
}
