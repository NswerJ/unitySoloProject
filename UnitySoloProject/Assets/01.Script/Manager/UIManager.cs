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
    private bool _canRotate = true; // Added variable to control rotation

    private Vector3 _currentRotation = Vector3.zero;

    private void Awake()
    {
        _light = GameObject.Find("LightHandle").GetComponent<Light>();
        _light.intensity = 1f;
        _currentRotation = _cameraTrm.eulerAngles;
    }

    public void Update()
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

        float mousePositionX = Input.mousePosition.x;
        float screenWidth = Screen.width;

        if (_canRotate && mousePositionX < screenWidth * 0.25f)
        {
            LRotationChange();
        }
        else if (_canRotate && mousePositionX > screenWidth * 0.75f)
        {
            RRotationChange();
        }
        else
        {
            StopRotation();
        }
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

    public void StopRotation()
    {
        _turn = false;
    }

    private IEnumerator RotationChange(float targetYRot)
    {
        _canRotate = false; // Disable rotation during the delay

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

        _cameraTrm.localRotation = Quaternion.Euler(0, targetYRot, 0);
        _light.enabled = true;
        _turn = false;

        yield return new WaitForSeconds(.5f); // Add a 1-second delay
        _canRotate = true; // Enable rotation after the delay
    }
}