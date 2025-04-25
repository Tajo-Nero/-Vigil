using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 감지 반경에 들어오면 특정 애니메이션을 실행하는 스크립트.
/// 또한 쉬어 및 엎드려 동작을 수행할 수 있음.
/// </summary>
public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    private bool isLiedown = false; // 엎드려 상태 확인

    public bool isMeet = false;
    // 애니메이션 상태 Enum
    private enum AnimationState { Idle, Attention, Liedown, Standup, Salute }

    // 오디오 상태 Enum
    private enum AudioState { Idle, Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Idle;
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip idleSound;
    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    /// <summary>
    /// 초기 설정: 애니메이터 및 오디오 소스 가져오기, 오디오 클립 등록.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // 오디오 클립 등록
        audioClips.Add(AudioState.Idle, idleSound);
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);
    }

    /// <summary>
    /// 키 입력에 따라 동작 실행 (쉬어, 엎드려)
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
    /// 플레이어가 감지 범위에 들어오면 경례 실행.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isMeet=true;
        Salute();
    }

    /// <summary>
    /// 경례 애니메이션 실행.
    /// </summary>
    void Salute()
    {
        SetAnimationState(AnimationState.Salute);
    }

    /// <summary>
    /// 쉬어 동작 실행. 엎드려 상태일 경우 일어서도록 처리.
    /// 1초 후 Idle 상태로 복귀.
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
    /// 엎드려 애니메이션 실행.
    /// </summary>
    public void Liedown()
    {
        SetAnimationState(AnimationState.Liedown);
        isLiedown = true;
    }

    /// <summary>
    /// 애니메이션 상태 변경.
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
    /// 해당 애니메이션 상태에 맞는 오디오 재생.
    /// </summary>
    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }

    /// <summary>
    /// 1초 후 Idle 상태로 복귀.
    /// </summary>
    void ReturnToIdle()
    {
        SetAnimationState(AnimationState.Idle);
    }
}