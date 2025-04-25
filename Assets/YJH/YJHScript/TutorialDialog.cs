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
            "불침번 게임 튜토리얼에 온 것을 환영합니다",
            
            "지금부터 플레이어는 컨트롤러를 내려 놓고",

            "손의 움직임으로 게임을 플레이 해야합니다",

            "컨트롤러를 내려놓은 후\n인게임에 손을 인식시켜 주십시오",
            
            "인식이 완료되었습니다",

            "왼손 먼저 알려드리겠습니다\n앞으로 이동하려면 검지만 펴주세요",

            "잘 하셨습니다",

            "뒤로 이동하려면 엄지만 펴주세요",

            "달리려면 검지와 중지만 펴주세요",

            "뒤로 도려면 새끼 손가락만 펴주세요",

            "뒤를 돌아 군인 앞까지 다가가 주세요",

            "오른손 사용법을 알려드리겠습니다\n오른손은 상호작용 전용입니다"

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

        //인식되면 다음 대사
        yield return new WaitUntil(() => LeftHand.activeSelf && RightHand.activeSelf);

        manager.ShowDialogue(dialogueLines[4]);
        yield return new WaitForSeconds(2);

        //앞으로 가기
        manager.ShowDialogue(dialogueLines[5]);
        LeftHandTracker.SetActive(true);                
        yield return new WaitForSeconds(1);
        FingerImg[4].SetActive(true);
        FingerImg[0].SetActive(true);

        //패널만 잠시 숨기기
        yield return new WaitForSeconds(2);
        manager.HideDialogue();
        gesture[0] = true;

        //제스쳐 인식되면 나오기 
        yield return new WaitUntil(()=> gesture[0]==false);
        manager.ShowDialogue(dialogueLines[6]);

        //뒤로가기
        yield return new WaitForSeconds(3);
        FingerImg[0].SetActive(false);
        manager.ShowDialogue(dialogueLines[7]);

        yield return new WaitForSeconds(1);
        FingerImg[1].SetActive(true);

        gesture[2] = true;
        yield return new WaitUntil(() => gesture[2]==false);
        manager.ShowDialogue(dialogueLines[6]);

        FingerImg[1].SetActive(false);

        //뛰기
        yield return new WaitForSeconds(3);
        manager.ShowDialogue(dialogueLines[8]);
        FingerImg[5].SetActive(true);

        gesture[1] = true;
        yield return new WaitUntil(() => gesture[1] == false);
        manager.ShowDialogue(dialogueLines[6]);


        FingerImg[5].SetActive(false);

        //뒤로 돌기
        yield return new WaitForSeconds(3);
        manager.ShowDialogue(dialogueLines[9]);
        FingerImg[6].SetActive(true);


        gesture[3] = true;
        yield return new WaitUntil(() => gesture[3] == false);

        manager.ShowDialogue(dialogueLines[6]);
        FingerImg[6].SetActive(false);
        FingerImg[4].SetActive(false);

        // 왼손 끝 페이크벽 삭제 후 일병한테 출발
        yield return new WaitForSeconds(3);

        Destroy(fakeWall);
        manager.ShowDialogue(dialogueLines[10]);

        yield return new WaitUntil(() => CDO.isMeet);
        
        //오른손 사용법 알려주기 시작
        RightHandTracker.SetActive(true);
        manager.ShowDialogue(dialogueLines[11]);




        //다 끝난후 삭제
        yield return new WaitForSeconds(3);
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
        }

            

    }
}
