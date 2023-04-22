using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private float _turnSpeed = 10f;

    public Transform _cameraTrm;

    // ���� �����̼� ���� ���� �����̼� ������ �ʱ�ȭ�մϴ�.
    private Vector3 _currentRotation = Vector3.zero;

    private float saveYRotL;

    public void RRotationChange()
    {
        float targetYRot = _currentRotation.y + 90f;
        if (targetYRot <= 180f)
        {
            StartCoroutine(RotationChange(targetYRot));
            _currentRotation = new Vector3(_currentRotation.x, targetYRot, _currentRotation.z);
        }
    }

    public void LRotationChange()
    {
        float targetYRot = _currentRotation.y - 90f;
        if (targetYRot >= 0f)
        {
            StartCoroutine(RotationChange(targetYRot));
            _currentRotation = new Vector3(_currentRotation.x, targetYRot, _currentRotation.z);
        }
    }

    private IEnumerator RotationChange(float targetYRot)
    {
        // ���� �����̼� ���� ���� �����̼� ������ �����մϴ�.
        Vector3 startRotation = _cameraTrm.localRotation.eulerAngles;
        float elapsedTime = 0;

        while (elapsedTime < _turnSpeed)
        {
            elapsedTime += Time.deltaTime;
            float yRot = Mathf.Lerp(startRotation.y, targetYRot, elapsedTime / _turnSpeed);
            _cameraTrm.localRotation = Quaternion.Euler(0, yRot, 0);
            yield return null;
        }

        _cameraTrm.localRotation = Quaternion.Euler(0, targetYRot, 0);
    }
}