using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{


    Animator animator;
    AudioSource audioSource;

    public bool isMeet = false;
    private bool isLiedown = false; // ����� ���� Ȯ��

    // �ִϸ��̼� ���� Enum (Idle ����)
    private enum AnimationState { Attention, Liedown, Standup, Salute }

    // ����� ���� Enum (Idle ����)
    private enum AudioState { Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Attention; // �⺻ ���¸� Attention���� ����
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // ����� Ŭ�� ��� (Idle ����)
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);

        // �ʱ� ���� ���� (Idle ���� �� Attention �⺻ ����)
        SetAnimationState(AnimationState.Attention);
    }  

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Salute();
    }

    void Salute()
    {
        SetAnimationState(AnimationState.Salute);
    }

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
    }

    public void Liedown()
    {
        SetAnimationState(AnimationState.Liedown);
        isLiedown = true;
    }

    void SetAnimationState(AnimationState newState)
    {
        currentState = newState;
        animator.SetBool("isAttention", false);
        animator.SetBool("isLiedown", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isSalute", false);

        animator.SetBool($"is{newState}", true);
        PlaySound((AudioState)newState);
    }

    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }
}