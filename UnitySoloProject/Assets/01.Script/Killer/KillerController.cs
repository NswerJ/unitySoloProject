using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class KillerController : MonoBehaviour
{
    public float _movementSpeed = 3f;
    public float _minDistanceToPlayer = 5f;
    public float _minChaseTime = 5f;
    public float _maxChaseTime = 8f;

    private Transform _playerTransform;
    public bool _isChasing = false;
    public bool _killKiller = false;
    [SerializeField]
    private float _chaseTimer;

    [SerializeField]
    private AudioSource _boomSound;

    UIManager _UIManager;

    public Volume volume;
    private Vignette vig;

    public LayerMask killerLayer;
    KillerSpawner killerSpawner;

    public float intensityIncrement = 0.05f; // Intensity 증가량
    public float smoothnessIncrement = 0.1f; // Smoothness 증가량
    public float changeInterval = 1f; // 변경 간격

    private float targetIntensity;
    private float targetSmoothness;
    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _boomSound = GetComponent<AudioSource>();
        // 추격 시작 타이머 설정
        _chaseTimer = Random.Range(_minChaseTime, _maxChaseTime);
        StartCoroutine(StartChasing());
        volume = _playerTransform.GetComponentInChildren<Volume>();
        _UIManager = FindObjectOfType<UIManager>();
        killerLayer = LayerMask.GetMask("Killer");
        _killKiller = true;
    }

    private void Update()
    {

        if (_isChasing)
        {
            _UIManager._canRotate = false;
            _killKiller = false;
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

            if (volume.profile.TryGet(out vig))
            {
                StartCoroutine(ChangeVignetteValues());
            }
            else
            {
                Debug.LogWarning("Vignette component not found in the Global Volume!");
            }
        }
        else
        {
            if (_killKiller)
            {
                InputCheck();
            }
        }

    }
    private void InputCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_isChasing)
            {
                Debug.Log("_isChasing is true!!!");
                return;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, killerLayer))
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    Destroy(clickedObject);
                    StartCoroutine(RespawnKiller()); // 삭제 후 잠시 후에 적 재스폰
                }
            }

        }
    }

    private IEnumerator RespawnKiller()
    {
        // 잠시 기다린 후에 적 재스폰
        yield return new WaitForSeconds(Random.Range(4f, 8f));

        // 적이 0마리인 경우에만 재스폰
        if (GameObject.FindGameObjectsWithTag("Killer").Length == 0)
        {
            killerSpawner = FindObjectOfType<KillerSpawner>();
            killerSpawner.StartSpawning();
        }
    }
    private IEnumerator ChangeVignetteValues()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            targetIntensity += intensityIncrement;
            targetSmoothness += smoothnessIncrement;

            vig.intensity.value = Mathf.Clamp01(targetIntensity);
            vig.smoothness.value = Mathf.Clamp01(targetSmoothness);

            yield return new WaitForSeconds(changeInterval);
            if (vig.intensity.value == 1 && vig.smoothness.value == 1)
            {
                break;
            }
        }
        SceneManager.LoadScene("GameOver");
    }

    public void ChageChaseTime()
    {
        _chaseTimer = 3f;
        Debug.Log(_chaseTimer);
    }

    private IEnumerator StartChasing()
    {
        yield return new WaitForSeconds(_chaseTimer);
        _isChasing = true;
    }
}
