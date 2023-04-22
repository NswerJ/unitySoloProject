using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private float _turnSpeed = 10f;

    public Transform _cameraTrm;

    public bool _isRotChange = true;

    private float _currentYRotR = 0f;
    private float _currentYRotL = 0f;
    
    private float saveYRotL;

    public void RRotationChange()
    {
        _currentYRotR = Mathf.Clamp(_currentYRotR + 90f, -90f, 90f);
        StartCoroutine(RotationChange(_currentYRotR));
    }

    public void LRotationChange()
    {
        
        _currentYRotL = Mathf.Clamp(_currentYRotL - 90f, -90f, 90f);
        StartCoroutine(RotationChange(_currentYRotL));
    }

    private IEnumerator RotationChange(float targetYRot)
    {
        float startYRot = _cameraTrm.localRotation.eulerAngles.y;
        float elapsedTime = 0;

        while (elapsedTime < _turnSpeed)
        {
            elapsedTime += Time.deltaTime;
            float yRot = Mathf.Lerp(startYRot, targetYRot, elapsedTime / _turnSpeed);
            _cameraTrm.localRotation = Quaternion.Euler(0, yRot, 0);
            yield return null;
        }
        _cameraTrm.localRotation = Quaternion.Euler(0, targetYRot, 0);
    }
}