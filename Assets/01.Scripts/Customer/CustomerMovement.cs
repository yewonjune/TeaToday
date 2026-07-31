using System;
using UnityEngine;

// ¼Õ´Ô ¿òÁ÷ÀÓ
public class CustomerMovement : MonoBehaviour
{
    [Header("MOVE")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float arriveDistance = 0.05f;

    private Transform target;

    public bool isMoving {  get; private set; }

    public event Action OnArrived;

    // Update is called once per frame
    void Update()
    {
        if (!isMoving || target == null)
            return;

        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed*Time.deltaTime);
    }
}
