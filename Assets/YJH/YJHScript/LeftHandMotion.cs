using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandMotion : MonoBehaviour
{
    public bool isFist = false;
    public GameObject xrOri;
    public Transform headTransform; // XR Origin 안의 Main Camera (보통은 CenterEyeAnchor)

    public float moveSpeed = 1f;

    void FixedUpdate()
    {
        if (isFist)
        {
            Vector3 moveDir = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;
            xrOri.transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }
    public void HoldFist()
    {
        isFist = true;
        Debug.Log("걷기 시작");
    }
    public void WarmUpFist()
    {
        isFist = false;
        Debug.Log("걷기 끝");
    }

}
