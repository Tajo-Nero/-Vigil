using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 감지하고 애니메이션 상태를 변경하는 최동오일병 스크립트
/// </summary>
public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    public float detectionRadius = 5f;  // 플레이어 감지 반경
    public LayerMask playerLayer;

    private bool hasSaluted = false; // 최초 경례 여부 확인
    private bool isLiedown = false;  // 엎드려 상태 유지 여부
    private bool isSaluting = false; // 경례 상태 유지 여부

    // 애니메이션 상태 Enum
    private enum AnimationState { Idle, Attention, Liedown, Standup, Salute }

    // 오디오 상태 Enum
    private enum AudioState { Idle, Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        SetAnimationState(AnimationState.Idle); // 초기 상태: 쉬어
    }

    private void Update()
    {
        DetectPlayer();  // 플레이어 감지 유지
    }

    /// <summary>
    /// 플레이어를 감지하여 최초 한 번만 경례 실행.
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
    /// 경례 애니메이션을 실행하고 경례 상태 유지.
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

        Invoke("ReturnToIdle", 2f);
        hasSaluted = false;
        isSaluting = false;
        isLiedown = false;
    }

    /// <summary>
    /// 대가리박아 실행하고 상태 유지.
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

        Debug.Log($"현재 상태: {newState}");
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

        Debug.Log("쉬어!");
    }
}