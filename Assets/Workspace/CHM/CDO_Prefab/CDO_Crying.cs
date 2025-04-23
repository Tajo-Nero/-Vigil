using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDO_Crying : MonoBehaviour
{
    public AudioClip cryingSound; // ����� ����
    private AudioSource audioSource;
    private Animator animator;
    private bool isPlayerNearby = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // 10�ʸ��� ���� ���
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

            // �ִϸ��̼� ����
            animator.SetBool("isStandUp", true);

            // ���� ����
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // ���� �ð��� ������ �ٽ� ���� ���·� (��: 5�� �� �ʱ�ȭ)
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