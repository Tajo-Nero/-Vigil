using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ �����ϰ� �ִϸ��̼� ���¸� �����ϴ� �ֵ����Ϻ� ��ũ��Ʈ
/// </summary>
public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    public float detectionRadius = 5f;  // �÷��̾� ���� �ݰ�
    public LayerMask playerLayer;

    private bool hasSaluted = false; // ���� ��� ���� Ȯ��
    private bool isLiedown = false;  // ����� ���� ���� ����
    private bool isSaluting = false; // ��� ���� ���� ����

    // �ִϸ��̼� ���� Enum
    private enum AnimationState { Idle, Attention, Liedown, Standup, Salute }

    // ����� ���� Enum
    private enum AudioState { Idle, Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        SetAnimationState(AnimationState.Idle); // �ʱ� ����: ����
    }

    private void Update()
    {
        DetectPlayer();  // �÷��̾� ���� ����
    }

    /// <summary>
    /// �÷��̾ �����Ͽ� ���� �� ���� ��� ����.
    /// </summary>
    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hitColliders.Length > 0 && !hasSaluted) // ���� �� ���� ��� ����
        {
            Salute();
        }
    }

    /// <summary>
    /// ��� �ִϸ��̼��� �����ϰ� ��� ���� ����.
    /// </summary>
    void Salute()
    {
        SetAnimationState(AnimationState.Salute);
        isSaluting = true;
        hasSaluted = true; // ���� ��� ���� �� �ٽ� �������� ����
    }

    /// <summary>
    /// ���� ������ �����ϸ�, ��� �Ǵ� ����� ���¸� �����ϰ� 2�� �� Idle ���·� ����.
    /// </summary>
    void Rest()
    {
        if (isSaluting)
        {
            SetAnimationState(AnimationState.Attention);
        }
        else if (isLiedown)
        {
            SetAnimationState(AnimationState.Standup);
        }

        Invoke("ReturnToIdle", 2f);
        hasSaluted = false;
        isSaluting = false;
        isLiedown = false;
    }

    /// <summary>
    /// �밡���ھ� �����ϰ� ���� ����.
    /// </summary>
    void Liedown()
    {
        SetAnimationState(AnimationState.Liedown);
        isLiedown = true;
        hasSaluted = false;
    }

    /// <summary>
    /// �ִϸ��̼� ���¸� �����ϴ� �Լ�.
    /// </summary>
    void SetAnimationState(AnimationState newState)
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttention", false);
        animator.SetBool("isLiedown", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isSalute", false);

        currentState = newState;
        animator.SetBool($"is{newState}", true);

        Debug.Log($"���� ����: {newState}");
    }

    /// <summary>
    /// 2�� �� Idle ���·� �����ϴ� �Լ�.
    /// </summary>
    void ReturnToIdle()
    {
        animator.SetBool("isAttention", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isIdle", true);
        isSaluting = false;
        isLiedown = false;

        Debug.Log("����!");
    }
}