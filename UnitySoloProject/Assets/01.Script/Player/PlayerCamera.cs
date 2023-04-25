using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // 플레이어 Transform 컴포넌트
    [SerializeField]
    Texture2D cursorImg;

    private void Start()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {

    }

    private void OnGUI()
    {
        GUI.skin.settings.cursorColor = Color.red;
    }

}
