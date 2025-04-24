using System.Collections;
using UnityEngine;

public class TutorialDialog : MonoBehaviour
{
    //TalkableBase TT;
    string[] dialogueLines;

    bool recognize = false;
    [SerializeField] GameObject LeftHand;
    [SerializeField] GameObject LeftHandTracker;
    [SerializeField] GameObject RightHand;
    [SerializeField] GameObject RightHandTracker;
    [SerializeField] GameObject IngameCanva;

    void Awake()
    {
        dialogueLines = new string[]
        {
            "��ħ�� ���� Ʃ�丮�� �� ���� ȯ���մϴ�",
            
            "���ݺ��� �÷��̾�� ��Ʈ�ѷ��� ���� ����\n���� ���������� ������ �÷��� �ؾ��մϴ�",

            "��Ʈ�ѷ��� �������� ��\n�ΰ��ӿ� ���� �νĽ��� �ֽʽÿ�"
            
        };
        StartCoroutine(Talkad());
    }

    IEnumerator Talkad()
    {
        DialogueManager manager = FindObjectOfType<DialogueManager>();

        for(int i=0;i<dialogueLines.Length;i++)
        {
            manager.ShowDialogue(dialogueLines[i]);
            yield return new WaitForSeconds(4);
        }

        yield return new WaitUntil(() => LeftHand.activeSelf && RightHand.activeSelf);

        LeftHandTracker.SetActive(true);
        RightHandTracker.SetActive(true);
        IngameCanva.SetActive(true);
        manager.HideDialogue();

        yield break;
    }
}
