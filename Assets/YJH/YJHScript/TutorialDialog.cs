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
            "불침번 게임 튜토리얼에 온 것을 환영합니다",
            
            "지금부터 플레이어는 컨트롤러를 내려 놓고\n손의 움직임으로 게임을 플레이 해야합니다",

            "컨트롤러를 내려놓은 후\n인게임에 손을 인식시켜 주십시오"
            
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
