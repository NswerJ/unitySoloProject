using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    // Start is called before the first frame update
    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = _uiDocument.rootVisualElement;
        Button btn = root.Q<Button>("StartButton");

        btn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("Game");
        });
    }
}
