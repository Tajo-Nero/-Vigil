using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class jjamtaigeo : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip idleSound, catSound, eatSound;
    private XRGrabInteractable grabInteractable;
    private bool hasPlayedCatSound = false; // 캣 사운드가 이미 재생되었는지 체크

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 오브젝트가 XR Grab으로 잡힐 때 실행될 이벤트 등록
        grabInteractable.selectEntered.AddListener(OnGrabbed);

        PlayEatSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isEat", false);
            transform.LookAt(other.transform); // 플레이어 방향으로 바라보기
            audioSource.Stop();
            
            // 캣 사운드가 한 번만 재생되도록 설정
            if (!hasPlayedCatSound)
            {
                PlayCatSound();
                hasPlayedCatSound = true; // 재생 여부를 기록
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