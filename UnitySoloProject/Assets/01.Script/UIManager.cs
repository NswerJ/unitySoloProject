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

    // 시작 로테이션 값을 현재 로테이션 값으로 초기화합니다.
    private Vector3 _currentRotation = Vector3.zero;

    private float saveYRotL;

    private void Awake()
    {
        _light = transform.Find("LightHandle").GetComponent<Light>(); 
    }

    public void RRotationChange()
    {
        float targetYRot = _currentRotation.y + 90f;
        if (targetYRot <= 180f)
        {
            _currentRotation = new Vector3(_currentRotation.x, targetYRot, _currentRotation.z);
            StartCoroutine(RotationChange(targetYRot));
        }
    }

    public void LRotationChange()
    {
        float targetYRot = _currentRotation.y - 90f;
        if (targetYRot >= 0f)
        {
            _currentRotation = new Vector3(_currentRotation.x, targetYRot, _currentRotation.z);
            StartCoroutine(RotationChange(targetYRot));
        }
    }
    private void Update()
    {
        if (_currentRotation.y == 90f)
        {
            _light.intensity = 2f;
        }
        else if (_currentRotation.y == 0 || _currentRotation.y == 180)
        {
            _light.intensity = 8f;
        }
    }

    private IEnumerator RotationChange(float targetYRot)
    {
        // 시작 로테이션 값을 현재 로테이션 값으로 변경합니다.
        Vector3 startRotation = _cameraTrm.localRotation.eulerAngles;
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
        _cameraTrm.localRotation = Quaternion.Euler(0, targetYRot, 0);
    }
}