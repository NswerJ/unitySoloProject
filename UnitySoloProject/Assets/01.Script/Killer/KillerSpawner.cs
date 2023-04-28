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
    private int _spawnedKillerCount; // 현재 스폰된 적 수

    private void Start()
    {
        // 적 스폰 위치 배열 초기화
        _spawnPoints = new Transform[_spawnPointsParent.childCount];
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = _spawnPointsParent.GetChild(i);
        }

        // 첫번째 적 스폰
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        // 적 스폰 최소/최대 시간 간격 내에서 랜덤 시간 생성
        float spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);

        // 랜덤한 위치에서 적 생성
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        GameObject enemyObject = Instantiate(_killerPrefab, spawnPoint.position, spawnPoint.rotation);

        // 적이 플레이어를 바라보도록 회전
        Vector3 direction = (GameObject.FindWithTag("Player").transform.position - enemyObject.transform.position).normalized;
        enemyObject.transform.LookAt(enemyObject.transform.position + direction);

        // 스폰된 적 수 증가
        _spawnedKillerCount++;
        // T 키를 누르면 스폰된 모든 적 제거
        while (_spawnedKillerCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Killer"))
                {
                    Destroy(enemy);
                }
                _spawnedKillerCount = 0; // 스폰된 적 수 초기화
                break;
            }
            yield return null;
        }

        // 한마리만 스폰했을 경우
        if (_spawnedKillerCount == 1)
        {
            yield break; // 코루틴 종료
        }

        // 다음 적 스폰
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnEnemy());
    }
}
