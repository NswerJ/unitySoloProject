using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawner : MonoBehaviour
{
    public GameObject _killerPrefab; // �� ������
    public Transform _spawnPointsParent; // �� ���� ��ġ�� �ִ� Empty ������Ʈ�� �θ�
    public float _minSpawnTime = 4f; // �� ���� �ּ� �ð� ����
    public float _maxSpawnTime = 8f; // �� ���� �ִ� �ð� ����

    private Transform[] _spawnPoints; // �� ���� ��ġ �迭
    private int _spawnedKillerCount; // ���� ������ �� ��

    private void Start()
    {
        // �� ���� ��ġ �迭 �ʱ�ȭ
        _spawnPoints = new Transform[_spawnPointsParent.childCount];
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = _spawnPointsParent.GetChild(i);
        }

        // ù��° �� ����
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        // �� ���� �ּ�/�ִ� �ð� ���� ������ ���� �ð� ����
        float spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);

        // ������ ��ġ���� �� ����
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        GameObject enemyObject = Instantiate(_killerPrefab, spawnPoint.position, spawnPoint.rotation);

        // ���� �÷��̾ �ٶ󺸵��� ȸ��
        Vector3 direction = (GameObject.FindWithTag("Player").transform.position - enemyObject.transform.position).normalized;
        enemyObject.transform.LookAt(enemyObject.transform.position + direction);

        // ������ �� �� ����
        _spawnedKillerCount++;
        // T Ű�� ������ ������ ��� �� ����
        while (_spawnedKillerCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Killer"))
                {
                    Destroy(enemy);
                }
                _spawnedKillerCount = 0; // ������ �� �� �ʱ�ȭ
                break;
            }
            yield return null;
        }

        // �Ѹ����� �������� ���
        if (_spawnedKillerCount == 1)
        {
            yield break; // �ڷ�ƾ ����
        }

        // ���� �� ����
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnEnemy());
    }
}
