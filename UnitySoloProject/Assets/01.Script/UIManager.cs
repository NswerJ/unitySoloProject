using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private float _turnSpeed = 10f;
    public Transform _cameraTrm;

    private float _currentYRot = 0f;

    public void RRotationChange()
    {
        _currentYRot = Mathf.Clamp(_currentYRot + 90f, -90f, 90f);
        _cameraTrm.transform.rotation = Quaternion.Euler(0, _currentYRot, 0);
    }
    public void LRotationChange()
    {
        _currentYRot = Mathf.Clamp(_currentYRot - 90f, -90f, 90f);
        _cameraTrm.transform.rotation = Quaternion.Euler(0, _currentYRot, 0);
    }
}
