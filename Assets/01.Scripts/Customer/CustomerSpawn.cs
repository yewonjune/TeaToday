using System;
using UnityEngine;

// 손님 SpawnPoint에서 스폰
public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private CustomerController customerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform entrancePoint;

    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxCustomer = 8;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < spawnInterval)
            return;

        timer = 0;

        TrySpawn();
    }

    private void TrySpawn()
    {
        if (CustomerManager.Instance == null)
        {
            Debug.LogError("[CustomerSpawn] CustomerManager가 없습니다.");
            return;
        }

        if (customerPrefab == null)
        {
            Debug.LogError("[CustomerSpawn] CustomerPrefab이 없습니다.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("[CustomerSpawn] SpawnPoint가 없습니다.");
            return;
        }

        if (entrancePoint == null)
        {
            Debug.LogError("[CustomerSpawn] EntrancePoint가 없습니다.");
            return;
        }

        if (CustomerManager.Instance.CustomerCount >= maxCustomer)
            return;

        if (CustomerManager.Instance.CanSpawnCustomer())
            return;

        CustomerController customer = Instantiate(
            customerPrefab,
            spawnPoint.position,
            Quaternion.identity);

        // 생성된 손님에게 씬의 위치 전달
        customer.Initialize(spawnPoint, entrancePoint);
    }
}
