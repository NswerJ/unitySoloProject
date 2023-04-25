using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private float _turnSpeed = 10f;
    [SerializeField]
    private Light _light;
    public Transform _cameraTrm;

    [SerializeField]
    private bool _turn = false;

    // 시작 로테이션 값을 현재 로테이션 값으로 초기화합니다.
    private Vector3 _currentRotation = Vector3.zero;

    private float saveYRotL;

    private void Awake()
    {
        _light = FindObjectOfType<Light>();
        _light.intensity = 2f;
        _currentRotation = _cameraTrm.eulerAngles;
    }

    public void RRotationChange()
    {
        _turn = true;
        float targetYRot = _currentRotation.y + 90f;
        if (targetYRot <= 180f)
        {
            _currentRotation = new Vector3(_currentRotation.x, targetYRot, _currentRotation.z);
            StartCoroutine(RotationChange(targetYRot));
        }
    }

    public void LRotationChange()
    {
        _turn = true;
        float targetYRot = _currentRotation.y - 90f;
        if (targetYRot >= 0f)
        {
            _currentRotation = new Vector3(_currentRotation.x, targetYRot, _currentRotation.z);
            StartCoroutine(RotationChange(targetYRot));
        }
    }
    private void Update()
    {
        if (_turn)
        {
            if (_currentRotation.y == 90f)
            {
                _light.intensity = 2f;
            }
            else if (_currentRotation.y > 90f || _currentRotation.y < 90f)
            {
                _light.intensity = 8f;
            }
        }
    }

    private IEnumerator RotationChange(float targetYRot)
    {
        // 시작 로테이션 값을 현재 로테이션 값으로 변경합니다.
        Vector3 startRotation = _cameraTrm.localRotation.eulerAngles;
        Debug.Log(targetYRot);
        float elapsedTime = 0;
        _light.enabled = false;
        while (elapsedTime < _turnSpeed)
        {
            elapsedTime += Time.deltaTime;
            float yRot = Mathf.Lerp(startRotation.y, targetYRot, elapsedTime / _turnSpeed);
            _cameraTrm.localRotation = Quaternion.Euler(0, yRot, 0);
            yield return null;
        }

        _light.enabled = true;
        _turn = false;
        _cameraTrm.localRotation = Quaternion.Euler(0, targetYRot, 0);
    }
}