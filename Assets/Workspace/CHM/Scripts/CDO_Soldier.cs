using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    private Renderer soldierRenderer;

    private bool isLiedown = false;
    private bool isTrigger = false; // 트리거 안에 있는지 확인하는 변수
    public bool isMeet = false; 

    private enum AnimationState { Attention, Liedown, Standup, Salute }
    private enum AudioState { Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Attention;
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isTrigger = true; // 플레이어가 트리거 안에 있을 때 true 설정
        Salute();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isTrigger = false; // 플레이어가 트리거에서 벗어났을 때 false 설정
    }

    void Salute()
    {
        if (isTrigger) SetAnimationState(AnimationState.Salute);
    }

    public void Rest()
    {
        if (!isTrigger) return; // 트리거 영역 내에 없으면 실행 안 함

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
        if (!isTrigger) return; // 트리거 영역 내에 없으면 실행 안 함

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