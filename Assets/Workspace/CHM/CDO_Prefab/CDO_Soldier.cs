using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{


    Animator animator;
    AudioSource audioSource;

    public bool isMeet = false;
    private bool isLiedown = false; // 엎드려 상태 확인

    // 애니메이션 상태 Enum (Idle 제거)
    private enum AnimationState { Attention, Liedown, Standup, Salute }

    // 오디오 상태 Enum (Idle 제거)
    private enum AudioState { Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Attention; // 기본 상태를 Attention으로 변경
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // 오디오 클립 등록 (Idle 제거)
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);

        // 초기 상태 설정 (Idle 제거 후 Attention 기본 상태)
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