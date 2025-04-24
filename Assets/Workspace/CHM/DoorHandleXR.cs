using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandleSimple : MonoBehaviour
{
    public DoorXR doorScript; // 문 제어 스크립트
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
        Debug.Log("손잡이 잡힘!");
        handleRigidbody.isKinematic = false; // 핸들 움직이도록 설정
        doorScript.EnablePhysics(); // 문이 움직이도록 설정
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("손잡이 놓임!");
        handleRigidbody.isKinematic = false; // 핸들을 다시 고정
        doorScript.DisablePhysics(); // 문도 다시 고정
    }
}