using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{
    public float _movementSpeed = 3f; // �̵� �ӵ�
    public float _minDistanceToPlayer = 5f; // �÷��̾���� �ּ� �Ÿ�
    public float _minChaseTime = 5f; // �߰� ���۱����� �ּ� �ð�
    public float _maxChaseTime = 8f; // �߰� ���۱����� �ִ� �ð�

    private Transform _playerTransform; // �÷��̾��� Transform ������Ʈ
    private bool _isChasing = false; // �߰� ���� ����
    private float _chaseTimer; // �߰� Ÿ�̸�

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;

        // �߰� ���� Ÿ�̸� ����
        _chaseTimer = Random.Range(_minChaseTime, _maxChaseTime);
        StartCoroutine(StartChasing());
    }

    private void Update()
    {
        if (_isChasing)
        {
            // �÷��̾ ���ϴ� ���� ���
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

            // �÷��̾ �ٶ󺸱� ���� ȸ��
            _playerTransform.rotation = Quaternion.LookRotation(-directionToPlayer);

            // �÷��̾���� �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

            // �÷��̾���� �Ÿ��� �ּ� �Ÿ����� ũ�� �̵�
            if (distanceToPlayer > _minDistanceToPlayer)
            {
                // �̵�
                transform.Translate(directionToPlayer * _movementSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                // �÷��̾� ��ũ��Ʈ�� Light ������Ʈ�� ã�Ƽ� intensity ����
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
