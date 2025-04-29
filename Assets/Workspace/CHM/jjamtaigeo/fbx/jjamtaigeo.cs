using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class jjamtaigeo : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip idleSound, catSound, eatSound;
    private XRGrabInteractable grabInteractable;
    private bool hasPlayedCatSound = false; // Ĺ ���尡 �̹� ����Ǿ����� üũ

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // ������Ʈ�� XR Grab���� ���� �� ����� �̺�Ʈ ���
        grabInteractable.selectEntered.AddListener(OnGrabbed);

        PlayEatSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isEat", false);
            transform.LookAt(other.transform); // �÷��̾� �������� �ٶ󺸱�
            audioSource.Stop();
            
            // Ĺ ���尡 �� ���� ����ǵ��� ����
            if (!hasPlayedCatSound)
            {
                PlayCatSound();
                hasPlayedCatSound = true; // ��� ���θ� ���
            }
        }
    }

    public void OnGrabbed(SelectEnterEventArgs args)
    {        
        PlayIdleSound();
    }
   

    void PlayIdleSound()
    {
        audioSource.PlayOneShot(idleSound);
        animator.SetBool("isIdle", true);
        animator.SetBool("isCatSound", false);
        
    }

    void PlayCatSound()
    {
        audioSource.PlayOneShot(catSound);
        animator.SetBool("isCatSound", true);
        animator.SetBool("isEat", false);
        animator.SetBool("isIdle", false);
    }

    void PlayEatSound()
    {
        audioSource.PlayOneShot(eatSound);
        animator.SetBool("isEat", true);
        
    }
}