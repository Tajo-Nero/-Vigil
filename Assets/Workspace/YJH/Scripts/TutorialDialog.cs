using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class TutorialDialog : MonoBehaviour
{
    //TalkableBase TT;
    string[] dialogueLines;
    public CDO_Soldier CDO;
    public bool[] gesture= new bool[3];

    [SerializeField] GameObject fakeWall;
    [SerializeField] GameObject LeftHand;
    [SerializeField] GameObject LeftHandTracker;
    [SerializeField] GameObject RightHand;
    [SerializeField] GameObject RightHandTracker;
    [SerializeField] GameObject[] FingerImg;

    void Awake()
    {
        dialogueLines = new string[]
        {
            "��ħ�� ���� Ʃ�丮�� �� ���� ȯ���մϴ�",
            
            "���ݺ��� �÷��̾�� ��Ʈ�ѷ��� ���� ����",

            "���� ���������� ������ �÷��� �ؾ��մϴ�",

            "��Ʈ�ѷ��� �������� ��\n�ΰ��ӿ� ���� �νĽ��� �ֽʽÿ�",
            
            "�ν��� �Ϸ�Ǿ����ϴ�",

            "�޼� ���� �˷��帮�ڽ��ϴ�\n������ �̵��Ϸ��� ������ ���ּ���",

            "�� �ϼ̽��ϴ�",

            "�ڷ� �̵��Ϸ��� ������ ���ּ���",

            "�޸����� ������ ������ ���ּ���",

            "�ڷ� ������ ���� �հ����� ���ּ���",

            "���� �ձ��� �ٰ��� �ּ���",

            "������ ������ �˷��帮�ڽ��ϴ�\n�������� ��ȣ�ۿ� �Դϴ�",

            "��ʸ� �޾Ƽ� ��� �Ϸ��� ������ ������ ���ּ���",

            "�������� �ְ� ������ ������ ������ ���ּ���",
            
            "�����ϼ̽��ϴ�\n�����ʿ� �ִ� ��ư�� ���� �̵��Ͻʼ�\n����� ���ϴ�"

        };
        StartCoroutine(Talkad());
    }

    IEnumerator Talkad()
    {
        DialogueManager manager = FindObjectOfType<DialogueManager>();

        for(int i=0;i<4;i++)
        {
            manager.ShowDialogue(dialogueLines[i]);
            yield return new WaitForSeconds(4);
        }

        //�νĵǸ� ���� ���
        yield return new WaitUntil(() => LeftHand.activeSelf && RightHand.activeSelf);

        manager.ShowDialogue(dialogueLines[4]);
        yield return new WaitForSeconds(2);

        //������ ����
        manager.ShowDialogue(dialogueLines[5]);
        LeftHandTracker.SetActive(true);                
        yield return new WaitForSeconds(1);
        FingerImg[4].SetActive(true);
        FingerImg[0].SetActive(true);

        //�гθ� ��� �����
        yield return new WaitForSeconds(2);
        manager.HideDialogue();
        gesture[0] = true;

        //������ �νĵǸ� ������ 
        yield return new WaitUntil(()=> gesture[0]==false);
        manager.ShowDialogue(dialogueLines[6]);

        //�ڷΰ���
        yield return new WaitForSeconds(3);
        FingerImg[0].SetActive(false);
        manager.ShowDialogue(dialogueLines[7]);

        yield return new WaitForSeconds(1);
        FingerImg[1].SetActive(true);

        gesture[2] = true;
        yield return new WaitUntil(() => gesture[2]==false);
        manager.ShowDialogue(dialogueLines[6]);

        FingerImg[1].SetActive(false);

        //�ٱ�
        yield return new WaitForSeconds(3);
        manager.ShowDialogue(dialogueLines[8]);
        FingerImg[5].SetActive(true);

        gesture[1] = true;
        yield return new WaitUntil(() => gesture[1] == false);
        manager.ShowDialogue(dialogueLines[6]);


        FingerImg[5].SetActive(false);

        //�ڷ� ����
        yield return new WaitForSeconds(3);
        manager.ShowDialogue(dialogueLines[9]);
        FingerImg[6].SetActive(true);


        gesture[3] = true;
        yield return new WaitUntil(() => gesture[3] == false);

        manager.ShowDialogue(dialogueLines[6]);
        FingerImg[6].SetActive(false);
        FingerImg[4].SetActive(false);

        // �޼� �� ����ũ�� ���� �� �Ϻ����� ���
        yield return new WaitForSeconds(3);

        Destroy(fakeWall);
        manager.ShowDialogue(dialogueLines[10]);

        yield return new WaitUntil(() => CDO.isMeet);
        
        //������ ���� �˷��ֱ� ����
        RightHandTracker.SetActive(true);
        manager.ShowDialogue(dialogueLines[11]);

        yield return new WaitForSeconds(4);

        //����
        manager.ShowDialogue(dialogueLines[12]);
        FingerImg[7].SetActive(true);
        FingerImg[8].SetActive(true);

        gesture[4] = true;
        yield return new WaitUntil(() => gesture[4] == false);
        manager.ShowDialogue(dialogueLines[6]);
        FingerImg[8].SetActive(false);

        //�����
        yield return new WaitForSeconds(3);
        manager.ShowDialogue(dialogueLines[13]);
        FingerImg[9].SetActive(true);

        gesture[5] = true;
        yield return new WaitUntil(() => gesture[5] == false);
        manager.ShowDialogue(dialogueLines[6]);
        FingerImg[7].SetActive(false);
        FingerImg[9].SetActive(false);

        yield return new WaitForSeconds(3);
        manager.ShowDialogue(dialogueLines[14]);
        //�� ������ ����
        yield return new WaitForSeconds(6);

        manager.HideDialogue();
        yield break;
    }
    public void EndGester(int dd)
    {
        switch(dd)
        {
            case 1:
                gesture[0] = false;
                break;
            case 2:
                gesture[1] = false;
                break;
            case 3:
                gesture[2] = false;
                break;
            case 4:
                gesture[3] = false;
                break;
            case 5:
                gesture[4] = false;
                break;
            case 6:
                gesture[5] = false;
                break;

        }

            

    }
}
