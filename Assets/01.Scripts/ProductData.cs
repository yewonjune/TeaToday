using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "Scriptable Objects/ProductData")]
public class ProductData : ScriptableObject
{
    [Header("기본정보")]
    public int productId;
    public string productName;
    public ProductType productType;
    public Sprite icon;

    [Header("제작정보")]
    public int materialCost;
    public float craftTime;

    [Header("판매정보")]
    public int sellPrice;
    public float sellInterval;
    
}
