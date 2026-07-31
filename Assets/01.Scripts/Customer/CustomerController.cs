using System;
using UnityEngine;
using UnityEngine.XR;

// 손님 상태 관리
public class CustomerController : MonoBehaviour
{
    public CustomerState CurrentState { get; private set; }
    public CustomerMovement movement;
    public CustomerOrder order;

    private void Awake()
    {
        movement = GetComponent<CustomerMovement>();
        order = GetComponent<CustomerOrder>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeState(CustomerState.Entering);
    }

    public void ChangeState(CustomerState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case CustomerState.Entering:
                EnterShop();
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
    }
    private void Order()
    {
        Debug.Log("주문");
    }
    private void WaitingForDrink()
    {
        Debug.Log("음료대기");
    }
    private void MoveToSeat()
    {
        Debug.Log("자리이동");
    }
    private void Sit()
    {
        Debug.Log("착석");
    }
    private void ReturnTray()
    {
        Debug.Log("퇴식");
    }

    private void ExitShop()
    {
        Debug.Log("퇴장");
    }
}
