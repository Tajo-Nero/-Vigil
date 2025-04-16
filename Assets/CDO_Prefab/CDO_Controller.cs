using UnityEngine;

public class CDO_Controller : MonoBehaviour
{
    public CDO_Soldier testScript; // Test 스크립트 참조

    void Start()
    {
        if (testScript == null)
        {
            Debug.LogError("스크립트 꽂아라");
        }
    }

    void Update()
    {
       testScript.HandleKeyInput();
    }
}