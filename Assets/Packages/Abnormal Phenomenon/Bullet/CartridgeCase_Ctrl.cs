using UnityEngine;

public class CartridgeCase_Ctrl : MonoBehaviour
{
    public float detectionRadius = 5f; // 감지 반경
    public LayerMask playerLayer; // 플레이어 레이어 설정
    public AudioClip detectSound; // 감지 시 재생할 사운드

    private AudioSource audioSource;
    private bool hasPlayedSound = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DetectPlayer();
    }

    /// <summary>
    /// 구형 반경 내에서 플레이어 감지
    /// </summary>
    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hitColliders.Length > 0)
        {
            if (!hasPlayedSound) // 아직 사운드를 실행하지 않았다면
            {
                PlaySound();
                hasPlayedSound = true;
            }
        }
        else
        {
            StopSound(); // 감지되지 않으면 사운드 정지
            hasPlayedSound = false; // 다시 감지 가능하게 초기화
        }
    }

    /// <summary>
    /// 감지 반경을 기즈모로 표시
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 감지 반경을 빨간색으로 표시
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 범위 표시
    }

    /// <summary>
    /// 사운드 재생
    /// </summary>
    void PlaySound()
    {
        if (audioSource != null && detectSound != null)
        {
            audioSource.PlayOneShot(detectSound);
            Debug.Log("플레이어 감지됨! 사운드 실행");
        }
    }

    void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // 사운드 정지
            Debug.Log("플레이어 감지되지 않음! 사운드 중지");
        }
    }
}