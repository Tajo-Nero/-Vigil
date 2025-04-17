using UnityEngine;

public class CartridgeCase_Ctrl : MonoBehaviour
{
    public float detectionRadius = 5f; // ���� �ݰ�
    public LayerMask playerLayer; // �÷��̾� ���̾� ����
    public AudioClip detectSound; // ���� �� ����� ����

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
    /// ���� �ݰ� ������ �÷��̾� ����
    /// </summary>
    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hitColliders.Length > 0)
        {
            if (!hasPlayedSound) // ���� ���带 �������� �ʾҴٸ�
            {
                PlaySound();
                hasPlayedSound = true;
            }
        }
        else
        {
            StopSound(); // �������� ������ ���� ����
            hasPlayedSound = false; // �ٽ� ���� �����ϰ� �ʱ�ȭ
        }
    }

    /// <summary>
    /// ���� �ݰ��� ������ ǥ��
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ���� �ݰ��� ���������� ǥ��
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // ���� ���� ǥ��
    }

    /// <summary>
    /// ���� ���
    /// </summary>
    void PlaySound()
    {
        if (audioSource != null && detectSound != null)
        {
            audioSource.PlayOneShot(detectSound);
            Debug.Log("�÷��̾� ������! ���� ����");
        }
    }

    void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // ���� ����
            Debug.Log("�÷��̾� �������� ����! ���� ����");
        }
    }
}