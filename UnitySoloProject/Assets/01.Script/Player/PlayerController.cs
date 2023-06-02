using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        mainCamera = Camera.main;
        timer = decreaseInterval;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetLayer))
            {
                // 특정 레이어에 맞았을 경우 첫 번째 슬라이더 값 증가
                slider.value += 1;
            }
        }

        // 일정 시간마다 첫 번째 슬라이더 값 감소
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            slider.value -= decreaseAmount;
            timer = decreaseInterval;
        }

        // 첫 번째 슬라이더가 최댓값에 도달하면 두 번째 슬라이더 활성화
        if (slider.value >= slider.maxValue)
        {
            secondSlider.gameObject.SetActive(true);
        }

        if (secondSlider.gameObject.activeSelf)
        {
            // 두 번째 슬라이더의 값 증가
            secondSlider.value += 1;
        }
    }
}




