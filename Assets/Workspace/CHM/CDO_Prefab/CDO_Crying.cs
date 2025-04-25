using System.Collections;
using UnityEngine;

public class CDO_Crying : MonoBehaviour
{
    public AudioClip cryingSound; // 재생할 사운드
    private AudioSource audioSource;
    private Animator animator;
    private bool isPlayerNearby = false;

    public GameObject soldierPrefab; // 솔져 프리팹
    public GameObject slenderPrefab; // 슬렌더맨 프리팹

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // 솔져 프리팹 비활성화
        if (soldierPrefab != null)
        {
            soldierPrefab.SetActive(false);
        }     

        // 슬렌더맨 프리팹 비활성화 (초기 상태)
        if (slenderPrefab != null)
        {
            slenderPrefab.SetActive(false);
        }
       

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

            // 슬렌더맨 프리팹 활성화
            if (slenderPrefab != null)
            {
                slenderPrefab.SetActive(true);
            }

            // 플레이어가 가까이 오면 울음 반복 중지
            CancelInvoke(nameof(PlayCryingSound));
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // 플레이어가 멀어지면 다시 울음 반복 시작
            InvokeRepeating(nameof(PlayCryingSound), 0f, 20f);
        }
    }

    void ResetState()
    {
        animator.SetBool("isStandUp", false);
        isPlayerNearby = false;
    }
}