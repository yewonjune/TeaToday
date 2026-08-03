using UnityEngine;

public class Table : Furniture
{
    [SerializeField] private Seat[] seats;

    //빈 의자 하나 반환
    public Seat GetAvailableSeat()
    {
        foreach(Seat seat in seats)
        {
            if (!seat.IsOccupied)
                return seat;
        }

        return null;
    }

    //현재 테이블에 빈 의자가 있는가?
    public bool HasAvailableSeat()
    {
        return GetAvailableSeat() != null;
    }
}
