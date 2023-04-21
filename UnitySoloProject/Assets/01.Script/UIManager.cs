using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private float _turnSpeed = 10f;
    public Transform _cameraTrm;
    private float _currentYRotR = 0f;
    private float _currentYRotL;

    private float saveYRotL;
    private float time;

    public void RRotationChange()
    {
        _currentYRotR = Mathf.Clamp(_currentYRotR + 90f, -90f, 90f);
        //_cameraTrm.transform.rotation = Quaternion.Euler(0, _currentYRot, 0);
    }
    public void LRotationChange()
    {
        _currentYRotL = Mathf.Clamp(_currentYRotL - 90f, -90f, 90f);
        time = 0;
        saveYRotL = transform.rotation.y;
        StartCoroutine(lRotation());
        //_cameraTrm.transform.rotation = Quaternion.Euler(0, _currentYRot, 0);
    }

    private IEnumerator lRotation()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        while (rot.y != _currentYRotL)
        {
            time += Time.deltaTime;
            rot.y = Mathf.Lerp(saveYRotL, _currentYRotL, time);
            transform.rotation = Quaternion.Euler(0,rot.y,0);
            yield return null;
        }
    }
}
