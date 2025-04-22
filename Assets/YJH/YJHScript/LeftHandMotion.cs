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

    public float moveSpeed = 1f;

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
        Debug.Log(xrOri.transform.rotation);
        Quaternion.Inverse(xrOri.transform.rotation);
        Debug.Log(xrOri.transform.rotation);
    }
}
