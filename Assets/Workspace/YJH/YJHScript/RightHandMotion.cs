using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class RightHandMotion : MonoBehaviour
{
    public CDO_Soldier cdo;
    public void Attention()
    {
        Debug.Log("�밡���ھ�");
        cdo.Liedown();
    }
    public void PointAt()
    {
    }
    public void ThumbsUp()
    {
        Debug.Log("����");
        cdo.Rest();
    }
}
