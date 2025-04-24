using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandleSimple : MonoBehaviour
{
    public DoorXR doorScript; // �� ���� ��ũ��Ʈ
    private XRGrabInteractable grabInteractable;
    private Rigidbody handleRigidbody;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        handleRigidbody = GetComponent<Rigidbody>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("������ ����!");
        handleRigidbody.isKinematic = false; // �ڵ� �����̵��� ����
        doorScript.EnablePhysics(); // ���� �����̵��� ����
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("������ ����!");
        handleRigidbody.isKinematic = false; // �ڵ��� �ٽ� ����
        doorScript.DisablePhysics(); // ���� �ٽ� ����
    }
}