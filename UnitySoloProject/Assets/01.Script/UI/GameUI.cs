using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _button;
    private bool _isButtonActive = false;
    private bool _isGamePaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {

        VisualElement root = _uiDocument.rootVisualElement;
        _button = root.Q<VisualElement>("Button");

        _button.RegisterCallback<ClickEvent>(e =>
        {
            ToggleButtonActivation();
        });
        Button startBtn = root.Q<Button>("StartButton");
        Button exBtn = root.Q<Button>("ExplainButton");
        Button exBtnX = root.Q<Button>("ExButtonX");
        VisualElement exPlain = root.Q("Explain");
        Button stBtn = root.Q<Button>("SettingButton");
        Button stBtnX = root.Q<Button>("StButtonX");
        VisualElement setting = root.Q("Setting");
        Button quitBtn = root.Q<Button>("QuitButton");
        quitBtn.RegisterCallback<ClickEvent>(e =>
        {
            QuitGame();
        });


        /*startBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("Game");
        });*/
        exBtn.RegisterCallback<ClickEvent>(e =>
        {
            exPlain.AddToClassList("down");
            PauseGame();
        });
        exBtnX.RegisterCallback<ClickEvent>(e =>
        {
            exPlain.RemoveFromClassList("down");
            ResumeGame();
        });
        stBtn.RegisterCallback<ClickEvent>(s =>
        {
            setting.AddToClassList("down");
            PauseGame();
        });
        stBtnX.RegisterCallback<ClickEvent>(s =>
        {
            setting.RemoveFromClassList("down");
            ResumeGame();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isButtonActive)
            {
                _button.RemoveFromClassList("down");
                _isButtonActive = false;
                ResumeGame();
            }
            else
            {
                _button.AddToClassList("down");
                _isButtonActive = true;
                PauseGame();
            }
        }
    }

    private void ToggleButtonActivation()
    {
        if (_isButtonActive)
        {
            _button.RemoveFromClassList("down");
            _isButtonActive = false;
        }
        else
        {
            _button.AddToClassList("down");
            _isButtonActive = true;
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        _isGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        _isGamePaused = false;
    }




}
