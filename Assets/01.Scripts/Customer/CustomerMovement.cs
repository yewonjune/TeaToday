using System;
using UnityEngine;

// 손님 움직임
public class CustomerMovement : MonoBehaviour
{
    [Header("MOVE")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float arriveDistance = 0.05f;

    private Transform target;

    public bool IsMoving {  get; private set; }

    public event Action OnArrived;

    // Update is called once per frame
    void Update()
    {
        if (!IsMoving || target == null)
            return;

        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed*Time.deltaTime);

        float distance = Vector2.Distance(transform.position, target.position);

        if(distance <= arriveDistance)
        {
            transform.position = target.position;
            IsMoving = false;
            OnArrived?.Invoke();
        }
    }

    //이동 시작
    public void MoveTo(Transform destination)
    {
        target = destination;
        IsMoving = true;
    }

    //이동 중지
    public void Stop()
    {
        IsMoving = false;
    }
}
