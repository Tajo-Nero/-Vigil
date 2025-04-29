using System.Collections;
using UnityEngine;

public class CDO_Crying : MonoBehaviour
{
    public AudioClip cryingSound; // ����� ����
    private AudioSource audioSource;
    private Animator animator;
    private bool isPlayerNearby = false;

    public GameObject soldierPrefab; // ���� ������
    public GameObject slenderPrefab; // �������� ������

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // ���� ������ ��Ȱ��ȭ
        if (soldierPrefab != null)
        {
            soldierPrefab.SetActive(false);
        }     

        // �������� ������ ��Ȱ��ȭ (�ʱ� ����)
        if (slenderPrefab != null)
        {
            slenderPrefab.SetActive(false);
        }
       

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
            animator.SetBool("isStandUp", true);

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // �������� ������ Ȱ��ȭ
            if (slenderPrefab != null)
            {
                slenderPrefab.SetActive(true);
            }

            // �÷��̾ ������ ���� ���� �ݺ� ����
            CancelInvoke(nameof(PlayCryingSound));
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // �÷��̾ �־����� �ٽ� ���� �ݺ� ����
            InvokeRepeating(nameof(PlayCryingSound), 0f, 20f);
        }
    }

    void ResetState()
    {
        animator.SetBool("isStandUp", false);
        isPlayerNearby = false;
    }
}