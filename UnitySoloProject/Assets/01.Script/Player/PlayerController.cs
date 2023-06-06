using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Slider slider; // 첫 번째 슬라이더를 할당할 변수
    public Slider secondSlider; // 두 번째 슬라이더를 할당할 변수
    public LayerMask targetLayer; // 레이어 마스크로 타겟 레이어를 지정

    private float decreaseAmount = 0.2f; // 슬라이더가 감소할 양
    private float decreaseInterval = 0.5f; // 슬라이더가 감소하는 시간 간격
    private float timer; // 슬라이더 감소를 위한 타이머
    private Camera mainCamera;
    private bool firstSlider = false;
    private bool secondSliderActive = false;

    RaycastHit hit;

    private void Awake()
    {
        slider = FindObjectOfType<Slider>();
        secondSlider = slider.transform.GetChild(0).GetComponent<Slider>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        timer = decreaseInterval;
        slider.gameObject.SetActive(false);
        secondSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetLayer))
            {
                if (!firstSlider && !secondSliderActive)
                {
                    firstSlider = true;
                    slider.gameObject.SetActive(true);
                }

                if (firstSlider && !secondSliderActive)
                {
                    slider.value += 1;

                    if (slider.value >= slider.maxValue)
                    {
                        slider.value = slider.maxValue;
                        secondSlider.gameObject.SetActive(true);
                        secondSliderActive = true;
                    }
                }

                if (secondSliderActive)
                {
                    secondSlider.value += 1;
                }
            }
        }

        // 일정 시간마다 첫 번째 슬라이더 값 감소
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (firstSlider && !secondSliderActive)
            {
                slider.value -= decreaseAmount;

                if (slider.value <= 0)
                {
                    slider.value = 0;
                    firstSlider = false;
                    slider.gameObject.SetActive(false);
                }
            }

            if (secondSliderActive)
            {
                secondSlider.value -= decreaseAmount;

                if (secondSlider.value <= 0)
                {
                    secondSlider.value = 0;
                    secondSlider.gameObject.SetActive(false);
                    secondSliderActive = false;
                }
            }

            timer = decreaseInterval;
        }
    }
}




