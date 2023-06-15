using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawner : MonoBehaviour
{
    public GameObject _killerPrefab; // 적 프리팹
    public Transform _spawnPointsParent; // 적 스폰 위치가 있는 Empty 오브젝트의 부모
    public float _minSpawnTime = 4f; // 적 스폰 최소 시간 간격
    public float _maxSpawnTime = 8f; // 적 스폰 최대 시간 간격

    private Transform[] _spawnPoints; // 적 스폰 위치 배열
    private bool _isSpawning; // 스폰 중인지 여부

    private void Start()
    {
        // 적 스폰 위치 배열 초기화
        _spawnPoints = new Transform[_spawnPointsParent.childCount];
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = _spawnPointsParent.GetChild(i);
        }

        // 적 스폰 시작
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    public void StopSpawning()
    {
        if (_isSpawning)
        {
            _isSpawning = false;
            StopCoroutine(SpawnEnemy());
        }
    }
    
    public void SecondSpawnTime()
    {
        _minSpawnTime = 3f;
        _maxSpawnTime = 5f;
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

        while (_isSpawning)
        {
            if (GameObject.FindGameObjectsWithTag("Killer").Length < 1)
            {
                Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                GameObject enemyObject = Instantiate(_killerPrefab, spawnPoint.position, spawnPoint.rotation);

                Vector3 direction = (GameObject.FindWithTag("Player").transform.position - enemyObject.transform.position).normalized;
                enemyObject.transform.LookAt(enemyObject.transform.position + direction);

                yield return new WaitUntil(() => enemyObject == null);
            }

            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
        }

        _isSpawning = false;
    }
}
