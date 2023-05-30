using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider slider; // �����̴��� �Ҵ��� ����
    public LayerMask targetLayer; // ���̾� ����ũ�� Ÿ�� ���̾ ����

    private float decreaseAmount = 0.2f; // �����̴��� ������ ��
    private float decreaseInterval = 1f; // �����̴��� �����ϴ� �ð� ����
    private float timer; // �����̴� ���Ҹ� ���� Ÿ�̸�
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
            // Ư�� ���̾ �¾��� ��� �����̴� �� ����
            slider.value += 1;
        }
        }
        // ���� �ð����� �����̴� �� ����
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            slider.value -= decreaseAmount;
            timer = decreaseInterval;
        }
    }
}




