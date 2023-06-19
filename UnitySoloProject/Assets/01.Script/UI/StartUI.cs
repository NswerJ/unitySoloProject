using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartUI : MonoBehaviour
{
    public AudioSource _clickAudio;
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
            _clickAudio.Play();
            StartCoroutine(GameScene());
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

    private IEnumerator GameScene()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Game");
    }
}
