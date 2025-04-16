using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    // 애니메이션 상태 Enum
    private enum AnimationState { Idle, Salute, Attention, Liedown, Standup }

    // 오디오 상태 Enum (애니메이션과 동일한 항목)
    // 맨처음 실행할때 Idle상태라 쉬어 사운드가 자동으로 나옴 
    private enum AudioState { Idle, Salute, Attention, Liedown, Standup }

    private AnimationState currentState = AnimationState.Idle;

    // 오디오 클립을 저장할 Dictionary
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

        if (animator == null) Debug.LogError("애니메이터가 없습니다!");
        if (audioSource == null) Debug.LogError("오디오 소스가 없습니다!");

        // 오디오 클립을 Dictionary에 매핑
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
        // 모든 애니메이션 상태 초기화
        animator.SetBool("isIdle", false);
        animator.SetBool("isSalute", false);
        animator.SetBool("isAttention", false);
        animator.SetBool("isLiedown", false);
        animator.SetBool("isStandup", false);

        // 새 상태 적용
        currentState = newState;
        animator.SetBool($"is{newState}", true);

        // 애니메이션 실행 + 오디오 재생
        PlaySound((AudioState)newState);

        Debug.Log($"현재 상태: {newState}");
    }

    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }
}