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

    private IEnumerator SpawnEnemy()
    {
        // 게임 시작 후 최초의 스폰은 바로 실행되지 않도록 대기
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

        while (_isSpawning)
        {
            // 스폰된 킬러가 없는 경우에만 적 생성
            if (GameObject.FindGameObjectsWithTag("Killer").Length < 1)
            {
                // 랜덤한 위치에서 적 생성
                Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                GameObject enemyObject = Instantiate(_killerPrefab, spawnPoint.position, spawnPoint.rotation);

                // 적이 플레이어를 바라보도록 회전
                Vector3 direction = (GameObject.FindWithTag("Player").transform.position - enemyObject.transform.position).normalized;
                enemyObject.transform.LookAt(enemyObject.transform.position + direction);

                // 생성된 적이 삭제될 때까지 대기
                yield return new WaitUntil(() => enemyObject == null);
            }

            // 일정 시간 동안 대기
            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
        }

        // 스폰 중지 후 다시 시작 가능하도록 설정
        _isSpawning = false;
    }
}
