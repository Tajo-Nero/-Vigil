using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ �����ϰ� �ִϸ��̼� ���¸� �����ϴ� �ֵ����Ϻ� ��ũ��Ʈ.
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

    // ����� Ŭ�� ���� Dictionary
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip idleSound;
    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // ����� Ŭ���� Dictionary�� ���
        audioClips.Add(AudioState.Idle, idleSound);
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);

        SetAnimationState(AnimationState.Idle); // �ʱ� ����: ����
    }

    private void Update()
    {
        DetectPlayer();  // �÷��̾� ���� ����
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hitColliders.Length > 0 && !hasSaluted) // ���� �� ���� ��� ����
        {
            Salute();
        }
    }

    void Salute()
    {
        SetAnimationState(AnimationState.Salute);
        isSaluting = true;
        hasSaluted = true;
    }

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

    void Liedown()
    {
        SetAnimationState(AnimationState.Liedown);
        isLiedown = true;
        hasSaluted = false;
    }

    void SetAnimationState(AnimationState newState)
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttention", false);
        animator.SetBool("isLiedown", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isSalute", false);

        currentState = newState;
        animator.SetBool($"is{newState}", true);

        PlaySound((AudioState)newState); // �ش� ������ ���� ���
        Debug.Log($"���� ����: {newState}");
    }

    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]); // �ش� ������ ���� ���
        }
    }

    void ReturnToIdle()
    {
        animator.SetBool("isAttention", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isIdle", true);
        isSaluting = false;
        isLiedown = false;

        PlaySound(AudioState.Idle);
        Debug.Log("����!");
    }
}