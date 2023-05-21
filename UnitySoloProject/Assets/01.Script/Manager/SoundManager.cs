using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    private Slider _soundSlider;

    private void OnEnable()
    {
        // UXML ���Ͽ��� Slider ��Ҹ� ã�� �Ҵ��մϴ�.
        _soundSlider = GetComponent<UIDocument>().rootVisualElement.Q<Slider>("soundslider");

        // Slider�� ���� ����� ������ ���带 �����ϴ� �޼��带 ȣ���մϴ�.
        _soundSlider.RegisterValueChangedCallback(OnSoundSliderValueChanged);
    }

    private void OnSoundSliderValueChanged(ChangeEvent<float> evt)
    {
        // Slider�� ���� ���� ���带 �����ϴ� ������ �����մϴ�.
        float soundValue = evt.newValue;
        
    }
}
