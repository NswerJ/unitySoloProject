using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseSound : MonoBehaviour
{
    public AudioClip soundClip;  // 재생할 사운드 클립
    private AudioSource audioSource; // 사운드 재생을 위한 오디오 소스
    private float nextSoundTime; // 다음 사운드 재생 시간

    void Start()
    {
        // 오디오 소스 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();

        // 게임 시작 후 4초에서 10초 사이의 랜덤한 시간에 사운드 재생
        nextSoundTime = Time.time + Random.Range(4f, 10f);
    }

    void Update()
    {
        // 현재 시간이 다음 사운드 재생 시간보다 크거나 같을 때
        if (Time.time >= nextSoundTime)
        {
            PlaySound(); // 사운드 재생

            // 3초 후에 사운드 중지
            Invoke("StopSound", 3f);

            // 다음 사운드 재생 시간 계산 (4초에서 10초 사이의 랜덤한 값)
            nextSoundTime = Time.time + Random.Range(4f, 10f);
        }
    }

    void PlaySound()
    {
        // 사운드 클립을 오디오 소스에 할당하고 재생
        audioSource.clip = soundClip;
        audioSource.Play();
    }

    void StopSound()
    {
        // 사운드 중지
        audioSource.Stop();
    }
}
