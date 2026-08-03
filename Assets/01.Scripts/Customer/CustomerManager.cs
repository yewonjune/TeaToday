using System.Collections.Generic;
using UnityEngine;

// 손님 등록/손님 제거/손님 수 관리/생성 가능 여부 판단
public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }

    // 현재 모든 손님
    public readonly List<CustomerController> customers = new();

    // 현재 손님 수
    public int CustomerCount => customers.Count;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
    }

    // 손님 등록
    public void Register(CustomerController customer)
    {
        if(!customers.Contains(customer))
        {
            customers.Add(customer);
        }    
    }

    // 손님 제거
    public void Unregister(CustomerController customer)
    {
        customers.Remove(customer);
    }

    // 새로운 손님을 생성할 수 있는지 확인
    public bool CanSpawnCustomer()
    {
        foreach (CustomerController customer in customers)
        {
            switch(customer.CurrentState)
            {
                case CustomerState.Entering:
                case CustomerState.Ordering:
                case CustomerState.MovingToSeat:
                case CustomerState.Sitting:
                    return false;
            }
        }

        return true;
    }

    //현재 존재하는 모든 손님
    public IReadOnlyList<CustomerController> Customers => customers;
}
