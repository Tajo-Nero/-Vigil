using UnityEngine;

public class CDO_Controller : MonoBehaviour
{
    public CDO_Soldier testScript; // Test ��ũ��Ʈ ����

    void Start()
    {
        if (testScript == null)
        {
            Debug.LogError("��ũ��Ʈ �Ⱦƶ�");
        }
    }

    void Update()
    {
       testScript.HandleKeyInput();
    }
}