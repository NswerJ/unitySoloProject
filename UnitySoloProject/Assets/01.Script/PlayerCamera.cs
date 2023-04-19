using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // 플레이어 Transform 컴포넌트
    [SerializeField] private float mouseSensitivity = 100f; // 마우스 감도
    [SerializeField]
    Texture2D cursorImg;
    private float xRotation = 0f; // X축 회전값

    private void Start()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
       
    }
}
