using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; //��ȭâ ��ü �г�
    public TextMeshProUGUI dialogueText; //��ȭ ���� ǥ���� Text ������Ʈ

    //�г� Ű�� ��ȭ ǥ���ϴ� �޼���
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