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

            // 애니메이션 실행
            animator.SetBool("isStandUp", true);

            // 사운드 중지
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // 일정 시간이 지나면 다시 원래 상태로 (예: 5초 후 초기화)
            Invoke(nameof(ResetState), 5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void ResetState()
    {
        animator.SetBool("isStandUp", false);
        isPlayerNearby = false;
    }
}