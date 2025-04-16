using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    // �ִϸ��̼� ���� Enum
    private enum AnimationState { Idle, Salute, Attention, Liedown, Standup }

    // ����� ���� Enum (�ִϸ��̼ǰ� ������ �׸�)
    // ��ó�� �����Ҷ� Idle���¶� ���� ���尡 �ڵ����� ���� 
    private enum AudioState { Idle, Salute, Attention, Liedown, Standup }

    private AnimationState currentState = AnimationState.Idle;

    // ����� Ŭ���� ������ Dictionary
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip idleSound;
    public AudioClip saluteSound;
    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (animator == null) Debug.LogError("�ִϸ����Ͱ� �����ϴ�!");
        if (audioSource == null) Debug.LogError("����� �ҽ��� �����ϴ�!");

        // ����� Ŭ���� Dictionary�� ����
        audioClips.Add(AudioState.Idle, idleSound);
        audioClips.Add(AudioState.Salute, saluteSound);
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);

        SetAnimationState(AnimationState.Idle);
    }

    public void HandleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetAnimationState(AnimationState.Salute);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetAnimationState(AnimationState.Attention);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetAnimationState(AnimationState.Liedown);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetAnimationState(AnimationState.Standup);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetAnimationState(AnimationState.Idle);
    }

    void SetAnimationState(AnimationState newState)
    {
        // ��� �ִϸ��̼� ���� �ʱ�ȭ
        animator.SetBool("isIdle", false);
        animator.SetBool("isSalute", false);
        animator.SetBool("isAttention", false);
        animator.SetBool("isLiedown", false);
        animator.SetBool("isStandup", false);

        // �� ���� ����
        currentState = newState;
        animator.SetBool($"is{newState}", true);

        // �ִϸ��̼� ���� + ����� ���
        PlaySound((AudioState)newState);

        Debug.Log($"���� ����: {newState}");
    }

    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }
}