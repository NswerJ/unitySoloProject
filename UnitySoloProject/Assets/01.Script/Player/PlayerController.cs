using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Slider slider;
    public Slider secondSlider;
    public LayerMask targetLayer;

    private float decreaseAmount = 0.1f;
    private float decreaseInterval = 0.5f;
    private float timer;
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
            Vector3 clickPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                if (!firstSlider && !secondSliderActive)
                {
                    firstSlider = true;
                    slider.gameObject.SetActive(true);
                }

                if (firstSlider && !secondSliderActive)
                {
                    slider.value += 2;

                    if (slider.value >= slider.maxValue)
                    {
                        slider.value = slider.maxValue;
                        secondSlider.gameObject.SetActive(true);
                        secondSliderActive = true;
                    }
                }

                if (secondSliderActive)
                {
                    secondSlider.value += 2;
                }

                // Ǫ��(push) �ڵ� �߰�
                PoolList.instance.Push(hit.transform.gameObject);
            }
        }

        // ���� �ð����� ù ��° �����̴� �� ����
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




