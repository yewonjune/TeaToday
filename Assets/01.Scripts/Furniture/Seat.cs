using UnityEngine;

public class Seat : MonoBehaviour
{
    [SerializeField] private Transform customerPoint;

    private CustomerController currentCustomer;
    public Transform CustomerPoint => customerPoint;

    public bool IsOccupied => currentCustomer != null;

    public void Reserve(CustomerController customer)
    {
        currentCustomer = customer;
    }

    public void Release()
    {
        currentCustomer = null;
    }
}
