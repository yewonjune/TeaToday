using UnityEngine;
using System.Collections.Generic;

// 빈자리 좌석 배정
public class SeatManager : MonoBehaviour
{
    public static SeatManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    // 가장 먼저 발견한 빈 의자 반환
    public Seat GetAvailableSeat()
    {
        List<Furniture> tables =
                FurnitureManager.Instance.GetFurnitureList(FurnitureType.Table);

        foreach (Furniture furniture in tables)
        {
            Table table = furniture as Table;

            if (table == null)
                continue;

            Seat seat = table.GetAvailableSeat();

            if (seat != null)
                return seat;
        }

        return null;
    }

    // 현재 빈 의자가 하나라도 있는지 확인
    public bool HasAvailableSeat()
    {
        return GetAvailableSeat() != null;
    }
}
