using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

// 손님 상태 관리
public class CustomerController : MonoBehaviour
{
    public CustomerState CurrentState { get; private set; }
    public CustomerMovement movement;
    private Seat currentSeat;

    private Transform spawnPoint;
    //[SerializeField] private Transform entrancePoint;

    //[SerializeField] private Transform exitPoint;

    private void Awake()
    {
        movement = GetComponent<CustomerMovement>();

        movement.OnArrived += HandleArrived;
    }
    public void Initialize(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CustomerManager.Instance.Register(this);
        ChangeState(CustomerState.Entering);
    }
    private void OnDestroy()
    {
        if (movement != null)
            movement.OnArrived -= HandleArrived;
    }

    public void ChangeState(CustomerState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case CustomerState.Entering:
                EnterShop();
                break;
            case CustomerState.MovingToCounter:
                MoveToCounter();
                break;
            case CustomerState.Ordering:
                Order();
                break;
            case CustomerState.WaitingForDrink:
                WaitingForDrink();
                break;
            case CustomerState.MovingToSeat:
                MoveToSeat();
                break;
            case CustomerState.Sitting:
                Sit();
                break;
            case CustomerState.Returning:
                ReturnTray();
                break;
            case CustomerState.MovingToExit:
                MoveToExit();
                break;
            case CustomerState.Exiting:
                ExitShop();
                break;
        }
    }

    private void EnterShop()
    {
        Debug.Log("입장");

        Furniture entrance = GetFurniture(FurnitureType.Entrance);

        if (entrance == null)
        {
            Debug.LogError("EntrancePoint가 없습니다.", this);
            ChangeState(CustomerState.Exiting);
            return;
        }

        movement.MoveTo(entrance.CustomerPoint);
    }
    private void MoveToCounter()
    {
        currentSeat = SeatManager.Instance.GetAvailableSeat();

        if (currentSeat == null)
        {
            Debug.Log("빈자리가 없어 퇴장합니다.");

            ChangeState(CustomerState.Exiting);
            return;
        }

        // 주문하는 동안 다른 손님이 가져가지 못하게 미리 예약
        currentSeat.Reserve(this);

        Furniture counter =
    FurnitureManager.Instance.GetFurniture(FurnitureType.Counter);

        if (counter == null)
        {
            Debug.LogWarning("카운터가 없습니다.", this);

            ReleaseSeat();
            ChangeState(CustomerState.Exiting);
            return;
        }

        if (counter.CustomerPoint == null)
        {
            Debug.LogError("카운터의 CustomerPoint가 없습니다.", counter);

            ReleaseSeat();
            ChangeState(CustomerState.Exiting);
            return;
        }

        movement.MoveTo(counter.CustomerPoint);
    }

    private void Order()
    {
        Debug.Log("주문");

        ChangeState(CustomerState.WaitingForDrink);
    }
    private void WaitingForDrink()
    {
        Debug.Log("음료 대기");

        StartCoroutine(WaitForDrink());
    }

    private IEnumerator WaitForDrink()
    {
        yield return new WaitForSeconds(2f);

        ChangeState(CustomerState.MovingToSeat);
    }
    private void MoveToSeat()
    {
        Debug.Log("자리 이동");

        movement.MoveTo(currentSeat.CustomerPoint);
    }
    private void Sit()
    {
        Debug.Log("착석");

        StartCoroutine(SitRoutine());
    }

    // 임시
    private IEnumerator SitRoutine()
    {
        yield return new WaitForSeconds(10f);

        ChangeState(CustomerState.Returning);
    }
    private void ReturnTray()
    {
        Debug.Log("퇴식");
        currentSeat?.Release();

        currentSeat = null;

        Furniture returnDesk =
                FurnitureManager.Instance.GetFurniture(FurnitureType.ReturnDesk);

        if (returnDesk == null)
            return;

        movement.MoveTo(returnDesk.CustomerPoint);
    }
    private void MoveToExit()
    {
        Debug.Log("입구로 이동");

        ReleaseSeat();

        Furniture entrance = GetFurniture(FurnitureType.Entrance);
        movement.MoveTo(entrance.CustomerPoint);
    }

    private void ExitShop()
    {
        Debug.Log("퇴장");
        ReleaseSeat();

        movement.MoveTo(spawnPoint);
    }

    private void HandleArrived()
    {
        switch (CurrentState)
        {
            case CustomerState.Entering:
                ChangeState(CustomerState.MovingToCounter);
                break;

            // EntrancePoint에서 Counter의 CustomerPoint 도착
            case CustomerState.MovingToCounter:
                ChangeState(CustomerState.Ordering);
                break;

            case CustomerState.MovingToSeat:
                ChangeState(CustomerState.Sitting);
                break;

            case CustomerState.Returning:
                ChangeState(CustomerState.MovingToExit);
                break;

            case CustomerState.MovingToExit:
                ChangeState(CustomerState.Exiting);
                break;

            case CustomerState.Exiting:
                Destroy(gameObject);
                break;
        }
    }

    // 필요한 가구 FurnitureManager에서 가져오기
    private Furniture GetFurniture(FurnitureType type)
    {
        return FurnitureManager.Instance.GetFurniture(type);
    }

    private void ReleaseSeat()
    {
        if (currentSeat == null)
            return;

        currentSeat.Release();
        currentSeat = null;
    }


    private void OnDisable()
    {
        if (CustomerManager.Instance != null)
        {
            CustomerManager.Instance.Unregister(this);
        }
    }
}
