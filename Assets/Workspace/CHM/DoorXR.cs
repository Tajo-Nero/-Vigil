using UnityEngine;

public class DoorXR : MonoBehaviour
{
    public Rigidbody doorRigidbody;
    public float forceAmount = 10f;

    void Start()
    {
        doorRigidbody.isKinematic = true; // ó������ ���� ������
    }

    public void EnablePhysics()
    {
        Debug.Log("�� ���� Ȱ��ȭ!");
        doorRigidbody.isKinematic = false; // �ڵ��� ������ ���� ���������� �����̵��� ����
    }

    public void DisablePhysics()
    {
        Debug.Log("�� ���� ��Ȱ��ȭ!");
        doorRigidbody.isKinematic = true; // �ڵ��� ������ ���� �ٽ� ������
    }
}