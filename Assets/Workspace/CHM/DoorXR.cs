using UnityEngine;

public class DoorXR : MonoBehaviour
{
    public Rigidbody doorRigidbody;
    public float forceAmount = 10f;

    void Start()
    {
        doorRigidbody.isKinematic = true; // 처음에는 문이 고정됨
    }

    public void EnablePhysics()
    {
        Debug.Log("문 물리 활성화!");
        doorRigidbody.isKinematic = false; // 핸들을 잡으면 문이 물리적으로 움직이도록 설정
    }

    public void DisablePhysics()
    {
        Debug.Log("문 물리 비활성화!");
        doorRigidbody.isKinematic = true; // 핸들을 놓으면 문이 다시 고정됨
    }
}