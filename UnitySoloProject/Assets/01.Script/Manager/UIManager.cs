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

    private Vector3 _currentRotation = Vector3.zero;

    private void Awake()
    {
        _light = GameObject.Find("LightHandle").GetComponent<Light>();
        _light.intensity = 1f;
        _currentRotation = _cameraTrm.eulerAngles;
    }

    public void RRotationChange()
    {
        float targetYRot = _currentRotation.y + 90f;
        if (targetYRot <= 180f && _turn == false)
        {
            _turn = true;
            _currentRotation = new Vector3(_currentRotation.x, targetYRot, _currentRotation.z);
            StartCoroutine(RotationChange(targetYRot));
        }
    }

    public void LRotationChange()
    {
        float targetYRot = _currentRotation.y - 90f;
        if (targetYRot >= 0f && _turn == false)
        {
            _turn = true;
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
                _light.intensity = 1f;
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
        Debug.Log(_light.enabled);
        while (elapsedTime < _turnSpeed)
        {
            elapsedTime += Time.deltaTime;
            float yRot = Mathf.Lerp(startRotation.y, targetYRot, elapsedTime / _turnSpeed);
            _cameraTrm.localRotation = Quaternion.Euler(0, yRot, 0);
            yield return null;
        }
        _cameraTrm.localRotation = Quaternion.Euler(0, targetYRot, 0);
        _light.enabled = true;
        _turn = false;
    }
}