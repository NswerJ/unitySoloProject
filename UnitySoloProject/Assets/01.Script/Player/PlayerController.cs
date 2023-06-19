using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
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

    [SerializeField]
    private UnityEvent firstSliderOn = null;
    [SerializeField]
    private UnityEvent changeChaseTime = null;

    private Light playerLight; // Reference to the player's Light component
    private float blinkSpeed = 2f; // Speed of blinking (how fast the intensity changes)
    private float blinkDuration = 3f; // Duration of one complete blink cycle
    private float blinkTimer; // Timer to track the progress of the blinking


    private void Start()
    {
        mainCamera = Camera.main;
        timer = decreaseInterval;
        slider.gameObject.SetActive(false);
        secondSlider.gameObject.SetActive(false);
        killerLayer = LayerMask.GetMask("Killer");

        playerLight = GetComponentInChildren<Light>(); // Get the Light component from a child object of the player
        blinkTimer = 0f; // Initialize the blink timer
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
                        changeChaseTime?.Invoke();
                        firstSliderOn?.Invoke();
                        slider.value = slider.maxValue;
                        secondSlider.gameObject.SetActive(true);
                        secondSliderActive = true;
                        StartBlinking();
                    }
                }

                if (secondSliderActive)
                {
                    secondSlider.value += 2;
                }

                PoolList.instance.Push(hit.transform.gameObject);
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
    private void StartBlinking()
    {
        // Reset the blink timer
        blinkTimer = 0f;

        // Start the blinking coroutine
        StartCoroutine(BlinkLightCoroutine());
    }
    private IEnumerator BlinkLightCoroutine()
    {
        // Calculate the blink phase (0 to 1) based on the blink timer and duration
        float blinkPhase = Mathf.PingPong(blinkTimer, blinkDuration) / blinkDuration;

        // Calculate the intensity value using a sin function to create the blinking effect
        float intensity = Mathf.Lerp(0.1f, 1f, Mathf.Sin(blinkPhase * Mathf.PI));

        // Set the light intensity
        playerLight.intensity = intensity;

        // Update the blink timer
        blinkTimer += Time.deltaTime * blinkSpeed;

        yield return null;

        // Check if the first slider is still filled
        if (slider.value >= slider.maxValue)
        {
            // Continue the blinking coroutine
            StartCoroutine(BlinkLightCoroutine());
        }
        else
        {
            // Reset the light intensity to 0
            playerLight.intensity = 0f;
        }
    }
}




