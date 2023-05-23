using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;


public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;
    public UIDocument document;


    private VisualElement root;


    private void OnEnable()
    {
        root = document.rootVisualElement;
        volumeSlider = root.Q<Slider>("volumeSlider");
        volumeSlider.RegisterCallback<ChangeEvent<float>>(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(ChangeEvent<float> evt)
    {
        Debug.Log("¾¾¹ß");
        mixer.SetFloat("StartBGM", volumeSlider.value);
    }
}
