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
    public void LittleUp()
    {
        isLittle = true;
    }
    public void LittleDown()
    {
        isLittle = false;
    }

    public void HoldFist()
    {
        isThumbsUp = true;
        Debug.Log("∞»±‚ Ω√¿€");
    }
    public void WarmUpFist()
    {
        isThumbsUp = false;
        Debug.Log("∞»±‚ ≥°");
    }

}
