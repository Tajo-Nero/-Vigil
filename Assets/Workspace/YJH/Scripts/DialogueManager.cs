using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; //대화창 전체 패널
    public TextMeshProUGUI dialogueText; //대화 내용 표시할 Text 컴포넌트

    //패널 키고 대화 표시하는 메서드
    public void ShowDialogue(string fullText)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = fullText;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}