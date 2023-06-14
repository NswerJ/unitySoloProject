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
        Button exBtnX = root.Q<Button>("ExButtonX");
        VisualElement exPlain = root.Q("Explain");
        Button stBtn = root.Q<Button>("SettingButton");
        Button stBtnX = root.Q<Button>("StButtonX");
        VisualElement setting = root.Q("Setting");
        

        startBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("Game");
        });
        exBtn.RegisterCallback<ClickEvent>(e =>
        {
            exPlain.AddToClassList("down");
        });
        exBtnX.RegisterCallback<ClickEvent>(e =>
        {
            exPlain.RemoveFromClassList("down");
        });
        stBtn.RegisterCallback<ClickEvent>(s =>
        {
            setting.AddToClassList("down");
        });
        stBtnX.RegisterCallback<ClickEvent>(s =>
        {
            setting.RemoveFromClassList("down");
        });
    }
}
