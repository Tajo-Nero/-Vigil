using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slender_Ctrl : MonoBehaviour
{
    private Animator animator;
    public Transform player; // �÷��̾� Ÿ��
    public float detectionRadius = 5f;  // ���� �ݰ�
    public LayerMask playerLayer; // �÷��̾� ���̾�   
    public float runSpeed = 0f;  // �ٱ� �ӵ� (0~1)
    public float moveSpeed = 3.5f; // �̵� �ӵ�
    private bool isIdle = true; // Idle ���� ���� ����
    private bool isActive = true; // Ȱ�� ���� ���� ����

    private AudioSource audioSource;
    public AudioClip runSound; // Run ���¿��� ����� ����

    void Start()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ��������
        audioSource = GetComponent<AudioSource>(); // ����� �ҽ� ��������

        // 2�� �������� Ȱ��ȭ ���� ����
        InvokeRepeating("ToggleActiveState", 0f, 2f);
    }

    void Update()
    {
        DetectPlayer(); // �÷��̾� ����    

        // ���� ���¿� ���� �ִϸ��̼� ����
        animator.SetFloat("isRun", runSpeed);
        animator.SetBool("isIdle", isIdle);

        // Idle ������ �� �̵��� ������ ����
        if (isIdle)
        {
            runSpeed = 0f;
        }
    }

    /// <summary>
    /// Ȱ��ȭ ���¸� 2�ʸ��� ���� (������ ������ �ݺ�)
    /// </summary>
    void ToggleActiveState()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }

    /// <summary>
    /// �÷��̾� ���� �� �̵� ���� ����.
    /// �����Ǹ� ��� Run ���·� ����, ���� ������ ����� Idle ���� ����.
    /// </summary>
    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hitColliders.Length > 0)
        {
            // �÷��̾ �����Ǹ� ������ ������ �ݺ��� ���߰� �׻� Ȱ��ȭ ���� ����
            CancelInvoke("ToggleActiveState");
            isActive = true;
            gameObject.SetActive(true);

            if (isIdle) // Idle ���¿��� Run���� ����� ���� ���� ����
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(runSound);
                }
            }

            runSpeed = Mathf.Lerp(runSpeed, 1f, Time.deltaTime * 2f);
            isIdle = false;
            ChasePlayer();
        }
        else
        {
            // ���� ������ ����� ��� Idle ���·� �����ϰ� �̵� ����
            runSpeed = 0f;
            isIdle = true;

            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // �������� ������ ���� ����
            }
        }
    }

    /// <summary>
    /// �÷��̾� �������� �̵��ϴ� �Լ�. (Idle ���¿����� ������� ����)
    /// </summary>
    void ChasePlayer()
    {
        if (player != null && !isIdle) // Idle ���°� �ƴ� ���� ����
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(player);
        }
    }
}