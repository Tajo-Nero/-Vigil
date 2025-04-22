using UnityEngine;

public class PhoneBox_Ctrl : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource가 추가되지 않았습니다!");
        }

        // 8초마다 PlaySound 실행
        InvokeRepeating("PlaySound", 0f, 8f);
    }

    public void Open()
    {
        if (_animator != null)
        {
            _animator.SetBool("isOpen", true);
        }
    }

    public void Close()
    {
        if (_animator != null)
        {
            _animator.SetBool("isOpen", false);
        }
    }

    public void ToggleDoor()
    {
        if (_animator != null)
        {
            _animator.SetBool("isOpen", !_animator.GetBool("isOpen"));
        }
    }

    void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play(); // 8초마다 사운드 실행
            Debug.Log("사운드 실행!");
        }
    }
    public void StopSound()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
            Debug.Log("사운드 종료");
            //사운드 꺼짐과 동시에 뭔가 튀어나오게 하자 
        }
    }
}