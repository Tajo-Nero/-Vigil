using UnityEngine;

public class CDO_Controller : MonoBehaviour
{
    public CDO_Soldier CDO_SoldierCtrScript; // Test ��ũ��Ʈ ����

    void Start()
    {
        if (CDO_SoldierCtrScript == null)
        {
            Debug.LogError("��ũ��Ʈ �Ⱦƶ�");
        }
    }
    
}