using UnityEngine;

public class CDO_Blood_Ctrl : MonoBehaviour
{
    public Transform target; // 따라갈 목표
    public float speed = 1f; // 이동 속도
    public float minDistance = 0.1f; // 목표에 도착했을 때 멈추는 거리
    public float scaleIncreaseRate = 0.1f; // 스케일 증가 속도
    public float maxScale = 2f; // 최대 스케일

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        // 목표와의 거리 계산
        float distance = Vector3.Distance(transform.position, target.position);

        // 목표까지 이동 (최소 거리 이상일 때만)
        if (distance > minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(target);

            if (animator != null)
            {
                animator.SetFloat("Run", speed / 5f); // 애니메이션 실행
            }

            // 이동 중 스케일 점진적으로 증가
            if (transform.localScale.x < maxScale)
            {
                transform.localScale += Vector3.one * scaleIncreaseRate * Time.deltaTime;
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetFloat("Run", 0f); // 멈추면 애니메이션 종료
            }
        }
    }
}