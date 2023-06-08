using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseSound : MonoBehaviour
{
    public AudioClip soundClip;  // ����� ���� Ŭ��
    private AudioSource audioSource; // ���� ����� ���� ����� �ҽ�
    private float nextSoundTime; // ���� ���� ��� �ð�

    void Start()
    {
        // ����� �ҽ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();

        // ���� ���� �� 4�ʿ��� 10�� ������ ������ �ð��� ���� ���
        nextSoundTime = Time.time + Random.Range(4f, 10f);
    }

    void Update()
    {
        // ���� �ð��� ���� ���� ��� �ð����� ũ�ų� ���� ��
        if (Time.time >= nextSoundTime)
        {
            PlaySound(); // ���� ���

            // 3�� �Ŀ� ���� ����
            Invoke("StopSound", 3f);

            // ���� ���� ��� �ð� ��� (4�ʿ��� 10�� ������ ������ ��)
            nextSoundTime = Time.time + Random.Range(4f, 10f);
        }
    }

    void PlaySound()
    {
        // ���� Ŭ���� ����� �ҽ��� �Ҵ��ϰ� ���
        audioSource.clip = soundClip;
        audioSource.Play();
    }

    void StopSound()
    {
        // ���� ����
        audioSource.Stop();
    }
}
