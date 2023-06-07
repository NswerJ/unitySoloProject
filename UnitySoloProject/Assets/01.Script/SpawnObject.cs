using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject childObject;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public int maxObjectCount = 5;

    private float nextSpawnTime;

    void Start()
    {
        for (int i = 0; i < maxObjectCount; i++)
        {
            SpawnChildObject();
        }

        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && parentObject.transform.childCount < maxObjectCount)
        {
            SpawnChildObject();
            SetNextSpawnTime();
        }
    }

    void SpawnChildObject()
    {
        // 랜덤한 위치 계산
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );

        // 자식 오브젝트 생성 및 부모 오브젝트에 추가
        GameObject spawnedObject = PoolList.instance.Pop("Object", randomPosition);

        if (spawnedObject == null)
        {
            Debug.LogError("오브젝트 풀에서 " + childObject.name + "을 가져올 수 없습니다.");
            return;
        }

        spawnedObject.transform.SetParent(parentObject.transform);
    }

    void SetNextSpawnTime()
    {
        float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        nextSpawnTime = Time.time + spawnInterval;
    }
}
