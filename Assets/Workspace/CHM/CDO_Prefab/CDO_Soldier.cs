using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ���� �ݰ濡 ������ Ư�� �ִϸ��̼��� �����ϴ� ��ũ��Ʈ.
/// ���� ���� �� ����� ������ ������ �� ����.
/// </summary>
public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    private bool isLiedown = false; // ����� ���� Ȯ��

    public bool isMeet = false;
    // �ִϸ��̼� ���� Enum
    private enum AnimationState { Idle, Attention, Liedown, Standup, Salute }

    // ����� ���� Enum
    private enum AudioState { Idle, Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Idle;
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip idleSound;
    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    /// <summary>
    /// �ʱ� ����: �ִϸ����� �� ����� �ҽ� ��������, ����� Ŭ�� ���.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // ����� Ŭ�� ���
        audioClips.Add(AudioState.Idle, idleSound);
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);
    }

    /// <summary>
    /// Ű �Է¿� ���� ���� ���� (����, �����)
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Rest();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Liedown();
        }
    }

    /// <summary>
    /// �÷��̾ ���� ������ ������ ��� ����.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isMeet=true;
        Salute();
    }

    /// <summary>
    /// ��� �ִϸ��̼� ����.
    /// </summary>
    void Salute()
    {
        SetAnimationState(AnimationState.Salute);
    }

    /// <summary>
    /// ���� ���� ����. ����� ������ ��� �Ͼ���� ó��.
    /// 1�� �� Idle ���·� ����.
    /// </summary>
    public void Rest()
    {
        if (isLiedown)
        {
            SetAnimationState(AnimationState.Standup);
            isLiedown = false;
        }
        else
        {
            SetAnimationState(AnimationState.Attention);
        }

        Invoke(nameof(ReturnToIdle), 1f);
    }

    /// <summary>
    /// ����� �ִϸ��̼� ����.
    /// </summary>
    public void Liedown()
    {
        SetAnimationState(AnimationState.Liedown);
        isLiedown = true;
    }

    /// <summary>
    /// �ִϸ��̼� ���� ����.
    /// </summary>
    void SetAnimationState(AnimationState newState)
    {
        currentState = newState;
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttention", false);
        animator.SetBool("isLiedown", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isSalute", false);

        animator.SetBool($"is{newState}", true);
        PlaySound((AudioState)newState);
    }

    /// <summary>
    /// �ش� �ִϸ��̼� ���¿� �´� ����� ���.
    /// </summary>
    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }

    /// <summary>
    /// 1�� �� Idle ���·� ����.
    /// </summary>
    void ReturnToIdle()
    {
        SetAnimationState(AnimationState.Idle);
    }
}