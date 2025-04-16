using UnityEngine;

public class CDO_Controller : MonoBehaviour
{
    public CDO_Soldier CDO_SoldierCtrScript; // Test 스크립트 참조

    void Start()
    {
        if (CDO_SoldierCtrScript == null)
        {
            Debug.LogError("스크립트 꽂아라");
        }
    }
    
}