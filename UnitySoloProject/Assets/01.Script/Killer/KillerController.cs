using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{
    public float _movementSpeed = 3f; 
    public float _minDistanceToPlayer = 5f; 
    public float _minChaseTime = 5f; 
    public float _maxChaseTime = 8f; 

    private Transform _playerTransform; 
    private bool _isChasing = false; 
    private float _chaseTimer;

    [SerializeField]
    private AudioSource _boomSound;


    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _boomSound = GetComponent<AudioSource>();
        // 추격 시작 타이머 설정
        _chaseTimer = Random.Range(_minChaseTime, _maxChaseTime);
        StartCoroutine(StartChasing());
    }

    private void Update()
    {
        if (_isChasing)
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0;
            _playerTransform.rotation = Quaternion.LookRotation(-directionToPlayer);

            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);


            if (distanceToPlayer > _minDistanceToPlayer)
            {
                transform.Translate(directionToPlayer * _movementSpeed * Time.deltaTime, Space.World);
                
            }
            else
            {
                Light playerLight = _playerTransform.GetComponentInChildren<Light>();
                _boomSound.Play();
                if (playerLight != null)
                {
                    playerLight.intensity = 8f;
                }
                Debug.Log("!!!!!");
                _isChasing = false;
            }
           
        }
    }

    private IEnumerator StartChasing()
    {
        yield return new WaitForSeconds(_chaseTimer);
        _isChasing = true;
    }
}
