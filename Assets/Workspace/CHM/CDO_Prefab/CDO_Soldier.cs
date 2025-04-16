using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 감지하고 애니메이션 상태를 변경하는 최동오일병 스크립트.
/// </summary>
public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    public float detectionRadius = 5f;  // 플레이어 감지 반경
    public LayerMask playerLayer; // 플레이어가 속한 레이어

    private bool hasSaluted = false; // 최초 경례 여부 확인
    private bool isLiedown = false;  // 엎드려 상태 유지 여부
    private bool isSaluting = false; // 경례 상태 유지 여부

    // 애니메이션 상태 Enum
    private enum AnimationState { Idle, Attention, Liedown, Standup, Salute }

    // 오디오 상태 Enum
    private enum AudioState { Idle, Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Idle;

    // 오디오 클립 저장 Dictionary
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip idleSound;
    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    /// <summary>
    /// 초기 설정: 애니메이터와 오디오 소스를 가져오고 기본 상태를 Idle로 설정.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // 오디오 클립을 Dictionary에 등록
        audioClips.Add(AudioState.Idle, idleSound);
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);

        SetAnimationState(AnimationState.Idle); // 초기 상태: 쉬어
    }

    /// <summary>
    /// 매 프레임마다 플레이어를 감지하는 함수.
    /// </summary>
    private void Update()
    {
        DetectPlayer();  // 플레이어 감지 유지
    }

    /// <summary>
    /// 플레이어를 감지하여 최초 한 번만 경례를 실행.
    /// </summary>
    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hitColliders.Length > 0 && !hasSaluted) // 최초 한 번만 경례 실행
        {
            Salute();
        }
    }

    /// <summary>
    /// 경례 애니메이션을 실행하고 경례 상태를 유지.
    /// </summary>
    void Salute()
    {
        SetAnimationState(AnimationState.Salute);
        isSaluting = true;
        hasSaluted = true; // 최초 경례 실행 후 다시 감지하지 않음
    }

    /// <summary>
    /// 쉬어 동작을 실행하며, 경례 또는 엎드려 상태를 해제하고 2초 후 Idle 상태로 복귀.
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

        Invoke("ReturnToIdle", 2f); // 2초 후 Idle 상태로 복귀
        hasSaluted = false;
        isSaluting = false;
        isLiedown = false;
    }

    /// <summary>
    /// 엎드려 애니메이션을 실행하고 상태를 유지.
    /// </summary>
    void Liedown()
    {
        SetAnimationState(AnimationState.Liedown);
        isLiedown = true;
        hasSaluted = false;
    }

    /// <summary>
    /// 애니메이션 상태를 변경하는 함수.
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

        PlaySound((AudioState)newState); // 해당 상태의 사운드 재생
        Debug.Log($"현재 상태: {newState}");
    }

    /// <summary>
    /// 주어진 상태에 따라 오디오를 재생하는 함수.
    /// </summary>
    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]); // 해당 상태의 사운드 재생
        }
    }

    /// <summary>
    /// 2초 후 Idle 상태로 복귀하는 함수.
    /// </summary>
    void ReturnToIdle()
    {
        animator.SetBool("isAttention", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isIdle", true);
        isSaluting = false;
        isLiedown = false;

        PlaySound(AudioState.Idle);
        Debug.Log("쉬어!");
    }
}