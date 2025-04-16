using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slender_Ctrl : MonoBehaviour
{
    private Animator animator;
    public Transform player; // 플레이어 타겟
    public float detectionRadius = 5f;  // 감지 반경
    public LayerMask playerLayer; // 플레이어 레이어   
    public float runSpeed = 0f;  // 뛰기 속도 (0~1)
    public float moveSpeed = 3.5f; // 이동 속도
    private bool isIdle = true; // Idle 상태 유지 여부

    private AudioSource audioSource;
    public AudioClip runSound; // Run 상태에서 재생할 사운드

    void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 가져오기
    }

    void Update()
    {
        DetectPlayer(); // 플레이어 감지    

        // 감지 상태에 따라 애니메이션 설정
        animator.SetFloat("isRun", runSpeed);
        animator.SetBool("isIdle", isIdle);

        // Idle 상태일 때 이동을 완전히 중지
        if (isIdle)
        {
            runSpeed = 0f;
        }
    }

    /// <summary>
    /// 플레이어 감지 후 이동 여부 결정.
    /// 감지되면 즉시 Run 상태로 변경, 감지 범위를 벗어나면 Idle 상태 유지.
    /// </summary>
    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hitColliders.Length > 0)
        {
            if (isIdle) // Idle 상태였다가 Run 상태로 변경될 때만 사운드 재생
            {
                audioSource.PlayOneShot(runSound);
            }

            runSpeed = Mathf.Lerp(runSpeed, 1f, Time.deltaTime * 2f);
            isIdle = false;
            ChasePlayer();
        }
        else
        {
            // 감지 범위를 벗어나면 즉시 Idle 상태로 변경하고 이동 중지
            runSpeed = 0f;
            isIdle = true;
        }
    }

    /// <summary>
    /// 플레이어 방향으로 이동하는 함수. (Idle 상태에서는 실행되지 않음)
    /// </summary>
    void ChasePlayer()
    {
        if (player != null && !isIdle) // Idle 상태가 아닐 때만 실행
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(player);
        }
    }
}