using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider slider;
    public Slider secondSlider;
    public LayerMask targetLayer;
    public LayerMask killerLayer;

    private float decreaseAmount = 0.1f;
    private float decreaseInterval = 0.5f;
    private float timer;
    private Camera mainCamera;
    private bool firstSlider = false;
    private bool secondSliderActive = false;
    KillerSpawner killerSpawner;


    private void Start()
    {
        mainCamera = Camera.main;
        timer = decreaseInterval;
        slider.gameObject.SetActive(false);
        secondSlider.gameObject.SetActive(false);
        killerLayer = LayerMask.GetMask("Killer");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetLayer))
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

                PoolList.instance.Push(hit.transform.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, killerLayer))
            {
                GameObject clickedObject = hit.collider.gameObject;
                Destroy(clickedObject);
                StartCoroutine(RespawnKiller()); // 삭제 후 잠시 후에 적 재스폰
            }
        }

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
        if(secondSlider.value == 100)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }

    private IEnumerator RespawnKiller()
    {
        // 잠시 기다린 후에 적 재스폰
        yield return new WaitForSeconds(Random.Range(4f, 8f));

        // 적이 0마리인 경우에만 재스폰
        if (GameObject.FindGameObjectsWithTag("Killer").Length == 0)
        {
            killerSpawner = FindObjectOfType<KillerSpawner>();
            killerSpawner.StartSpawning();
        }
    }
}




