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
        Button startBtn = root.Q<Button>("StartButton");
        Button exBtn = root.Q<Button>("ExplainButton");
        VisualElement exPlain = root.Q("Explain");

        startBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("Game");
        });
        exBtn.RegisterCallback<ClickEvent>(e =>
        {
            exPlain.AddToClassList("down");
        });
    }
}
