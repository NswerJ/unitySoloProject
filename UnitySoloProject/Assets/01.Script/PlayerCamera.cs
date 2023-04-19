using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // �÷��̾� Transform ������Ʈ
    [SerializeField] private float mouseSensitivity = 100f; // ���콺 ����
    [SerializeField]
    Texture2D cursorImg;
    private float xRotation = 0f; // X�� ȸ����

    private void Start()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
       
    }
}
