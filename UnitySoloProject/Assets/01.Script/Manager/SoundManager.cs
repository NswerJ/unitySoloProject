using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    private Slider _soundSlider;

    private void OnEnable()
    {
        // UXML 파일에서 Slider 요소를 찾아 할당합니다.
        _soundSlider = GetComponent<UIDocument>().rootVisualElement.Q<Slider>("soundslider");

        // Slider의 값이 변경될 때마다 사운드를 조절하는 메서드를 호출합니다.
        _soundSlider.RegisterValueChangedCallback(OnSoundSliderValueChanged);
    }

    private void OnSoundSliderValueChanged(ChangeEvent<float> evt)
    {
        // Slider의 값에 따라 사운드를 조절하는 로직을 구현합니다.
        float soundValue = evt.newValue;
        
    }
}
