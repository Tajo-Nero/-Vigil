using UnityEngine;

public class CDO_Blood_Ctrl : MonoBehaviour
{
    public Transform target; // ���� ��ǥ
    public float speed = 1f; // �̵� �ӵ�
    public float minDistance = 0.1f; // ��ǥ�� �������� �� ���ߴ� �Ÿ�
    public float scaleIncreaseRate = 0.1f; // ������ ���� �ӵ�
    public float maxScale = 2f; // �ִ� ������

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        // ��ǥ���� �Ÿ� ���
        float distance = Vector3.Distance(transform.position, target.position);

        // ��ǥ���� �̵� (�ּ� �Ÿ� �̻��� ����)
        if (distance > minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(target);

            if (animator != null)
            {
                animator.SetFloat("Run", speed / 5f); // �ִϸ��̼� ����
            }

            // �̵� �� ������ ���������� ����
            if (transform.localScale.x < maxScale)
            {
                transform.localScale += Vector3.one * scaleIncreaseRate * Time.deltaTime;
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetFloat("Run", 0f); // ���߸� �ִϸ��̼� ����
            }
        }
    }
}