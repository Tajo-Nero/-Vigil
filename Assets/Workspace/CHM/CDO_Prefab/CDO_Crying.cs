using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDO_Crying : MonoBehaviour
{
    public AudioClip cryingSound; // 재생할 사운드
    private AudioSource audioSource;
    private Animator animator;
    private bool isPlayerNearby = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // 10초마다 사운드 재생
        InvokeRepeating(nameof(PlayCryingSound), 0f, 10f);
    }

    void PlayCryingSound()
    {
        if (!isPlayerNearby && audioSource != null && cryingSound != null)
        {
            audioSource.PlayOneShot(cryingSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            animator.SetBool("isStandUp", true);

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // 플레이어가 가까이 오면 울음 반복 중지
            CancelInvoke(nameof(PlayCryingSound));

            // 일정 시간이 지나면 다시 원래 상태로 (예: 5초 후 초기화)
            Invoke(nameof(ResetState), 5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // 플레이어가 멀어지면 다시 울음 반복 시작
            InvokeRepeating(nameof(PlayCryingSound), 10f, 10f);
        }
    }

    void ResetState()
    {
        animator.SetBool("isStandUp", false);
        isPlayerNearby = false;
    }
}