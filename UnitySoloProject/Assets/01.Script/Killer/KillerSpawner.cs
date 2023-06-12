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
    public int _spawnedKillerCount; // 현재 스폰된 적 수

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

    private void StartSpawning()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    private void StopSpawning()
    {
        if (_isSpawning)
        {
            _isSpawning = false;
            StopCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (_isSpawning)
        {
            // 랜덤한 시간 간격으로 스폰
            float spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            // 랜덤한 위치에서 적 생성
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            GameObject enemyObject = Instantiate(_killerPrefab, spawnPoint.position, spawnPoint.rotation);

            // 적이 플레이어를 바라보도록 회전
            Vector3 direction = (GameObject.FindWithTag("Player").transform.position - enemyObject.transform.position).normalized;
            enemyObject.transform.LookAt(enemyObject.transform.position + direction);

            // 스폰된 적 수 증가
            _spawnedKillerCount++;
        }
    }

    private void Update()
    {
        // 한마리만 스폰했을 경우
        if (_spawnedKillerCount >= 1)
        {
            StopSpawning(); // 스폰 중지
        }
        else if (_spawnedKillerCount == 0)
        {
            StartSpawning(); // 스폰 시작
        }
    }
}
