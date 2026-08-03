using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FurnitureManager : MonoBehaviour
{
    public static FurnitureManager Instance {  get; private set; }

    private readonly List<Furniture> furnitures = new();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #region Register

    // 가구가 생기면 리스트에 넣음
    public void Register(Furniture furniture)
    {
        if(!furnitures.Contains(furniture))
            furnitures.Add(furniture);
    }

    public void Unregister(Furniture furniture)
    {
        furnitures.Remove(furniture);
    }

    #endregion


    #region Find

    // 하나인 가구  예)카운터,반납대 등
    public Furniture GetFurniture(FurnitureType type)
    {
        foreach (Furniture furniture in furnitures)
        {
            if (furniture.FurnitureType == type)
                return furniture;
        }

        return null;
    }

    // 여러개인 가구  예)테이블,의자 등
    public List<Furniture> GetFurnitureList(FurnitureType type)
    {
        List<Furniture> result = new();

        foreach (Furniture furniture in furnitures)
        {
            if (furniture.FurnitureType == type)
                result.Add(furniture);
        }

        return result;
    }

    #endregion
}
