using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandMotion : MonoBehaviour
{
    public bool isThumbsUp = false;
    public bool isLittle = false;
    public GameObject xrOri;

    public Transform headTransform;

    Vector3 moveDir;

    public float moveSpeed = 2f;

    private void Start()
    {
    }
    void FixedUpdate()
    {
        if (isThumbsUp)
        {
            moveDir = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;
            xrOri.transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        else if(isLittle)
        {
            moveDir = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;
            xrOri.transform.position -= moveDir * moveSpeed * Time.deltaTime;
        }
        
    }
    public void Backward()
    {
        isLittle = true;
    }
    public void StopBackward()
    {
        isLittle = false;
    }
    public void MoveFast()
    {
        isThumbsUp = true;
        moveSpeed = 5f;
    }
    public void StopFast()
    {
        isThumbsUp = false;
        moveSpeed = 2f;
    }
    public void Forward()
    {
        isThumbsUp = true;
        Debug.Log("∞»±‚ Ω√¿€");
    }
    public void StopForward()
    {
        isThumbsUp = false;
        Debug.Log("∞»±‚ ≥°");
    }
    public void TurnBack()
    {
        xrOri.transform.Rotate(0f, 180f, 0f);
    }
}
