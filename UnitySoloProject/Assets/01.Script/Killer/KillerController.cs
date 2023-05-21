using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{
    public float _movementSpeed = 3f; // 이동 속도
    public float _minDistanceToPlayer = 5f; // 플레이어와의 최소 거리
    public float _minChaseTime = 5f; // 추격 시작까지의 최소 시간
    public float _maxChaseTime = 8f; // 추격 시작까지의 최대 시간

    private Transform _playerTransform; // 플레이어의 Transform 컴포넌트
    private bool _isChasing = false; // 추격 상태 여부
    private float _chaseTimer; // 추격 타이머

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;

        // 추격 시작 타이머 설정
        _chaseTimer = Random.Range(_minChaseTime, _maxChaseTime);
        StartCoroutine(StartChasing());
    }

    private void Update()
    {
        if (_isChasing)
        {
            // 플레이어를 향하는 방향 계산
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

            // 플레이어를 바라보기 위한 회전
            _playerTransform.rotation = Quaternion.LookRotation(-directionToPlayer);

            // 플레이어와의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

            // 플레이어와의 거리가 최소 거리보다 크면 이동
            if (distanceToPlayer > _minDistanceToPlayer)
            {
                // 이동
                transform.Translate(directionToPlayer * _movementSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                // 플레이어 스크립트의 Light 컴포넌트를 찾아서 intensity 변경
                Light playerLight = _playerTransform.GetComponentInChildren<Light>();
                if (playerLight != null)
                {
                    playerLight.intensity = 2f;
                }
            }
        }
    }

    private IEnumerator StartChasing()
    {
        yield return new WaitForSeconds(_chaseTimer);
        _isChasing = true;
    }
}
