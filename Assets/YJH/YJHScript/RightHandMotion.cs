using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class RightHandMotion : MonoBehaviour
{
    public CDO_Soldier cdo;
    public void Attention()
    {
        Debug.Log("대가리박아");
        cdo.Liedown();
    }
    public void PointAt()
    {
    }
    public void ThumbsUp()
    {
        Debug.Log("쉬어");
        cdo.Rest();
    }
}
