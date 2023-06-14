using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverUI : MonoBehaviour
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
        Button quitBtn = root.Q<Button>("QuitButton");
        Button reStartBtn = root.Q<Button>("ReStartButton");

        quitBtn.RegisterCallback<ClickEvent>(e =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        reStartBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("StartScene");
        });
    }
}
