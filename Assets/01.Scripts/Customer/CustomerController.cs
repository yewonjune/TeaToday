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

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform entrancePoint;

    //[SerializeField] private Transform exitPoint;

    private void Awake()
    {
        movement = GetComponent<CustomerMovement>();

        movement.OnArrived += HandleArrived;
    }
    public void Initialize(Transform spawnPoint, Transform entrancePoint)
    {
        this.spawnPoint = spawnPoint;
        this.entrancePoint = entrancePoint;
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
            case CustomerState.Exiting:
                ExitShop();
                break;
        }
    }

    private void EnterShop()
    {
        Debug.Log("입장");

        if (entrancePoint == null)
        {
            Debug.LogError("EntrancePoint가 없습니다.", this);
            ChangeState(CustomerState.Exiting);
            return;
        }

        movement.MoveTo(entrancePoint);
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
        yield return new WaitForSeconds(50f);

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

    private void ExitShop()
    {
        Debug.Log("퇴장");

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
                ChangeState(CustomerState.Exiting);
                break;
            case CustomerState.Exiting:
                Destroy(gameObject);
                break;
        }
    }
    private void ReleaseSeat()
    {
        if (currentSeat == null)
            return;

        currentSeat.Release();
        currentSeat = null;
    }

    private void OnEnable()
    {
        CustomerManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        if (CustomerManager.Instance != null)
        {
            CustomerManager.Instance.Unregister(this);
        }
    }
}
