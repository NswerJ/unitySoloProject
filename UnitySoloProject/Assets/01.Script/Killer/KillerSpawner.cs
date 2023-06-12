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
    public int _spawnedKillerCount; // ���� ������ �� ��

    private bool _isSpawning; // ���� ������ ����

    private void Start()
    {
        // �� ���� ��ġ �迭 �ʱ�ȭ
        _spawnPoints = new Transform[_spawnPointsParent.childCount];
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = _spawnPointsParent.GetChild(i);
        }

        // �� ���� ����
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
            // ������ �ð� �������� ����
            float spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            // ������ ��ġ���� �� ����
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            GameObject enemyObject = Instantiate(_killerPrefab, spawnPoint.position, spawnPoint.rotation);

            // ���� �÷��̾ �ٶ󺸵��� ȸ��
            Vector3 direction = (GameObject.FindWithTag("Player").transform.position - enemyObject.transform.position).normalized;
            enemyObject.transform.LookAt(enemyObject.transform.position + direction);

            // ������ �� �� ����
            _spawnedKillerCount++;
        }
    }

    private void Update()
    {
        // �Ѹ����� �������� ���
        if (_spawnedKillerCount >= 1)
        {
            StopSpawning(); // ���� ����
        }
        else if (_spawnedKillerCount == 0)
        {
            StartSpawning(); // ���� ����
        }
    }
}
