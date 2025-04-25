using UnityEngine;

public class PhoneBox_Ctrl : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private Light childLight;
    private bool isSoundPlaying = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        childLight = GetComponentInChildren<Light>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource가 추가되지 않았습니다!");
        }

        // 8초마다 PlaySound 실행
        // 나중에 생성될때 실행하면됨 
        InvokeRepeating("PlaySound", 0f, 8f);
    }

    void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
            isSoundPlaying = true;
            RandomizeLight(); // 사운드 실행 시 라이트 깜빡이기 시작
        }
    }

    public void StopSound()
    {
        if (_audioSource != null)
        {
            _audioSource.loop = false;
            _audioSource.Stop();
            isSoundPlaying = false;
            CancelInvoke("RandomizeLight"); // 라이트 깜빡이는 호출 중단
            CancelInvoke("PlaySound"); // 라이트 깜빡이는 호출 중단
            Debug.Log("사운드 종료");
        }
    }

    void RandomizeLight()
    {
        if (!isSoundPlaying || childLight == null) return;

        childLight.enabled = !childLight.enabled; // 라이트 깜빡이기
        float nextToggleTime = Random.Range(0.3f, 1.2f); // 짧은 간격으로 변경
        Invoke("RandomizeLight", nextToggleTime); // 다음 호출 예약
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open(); // 플레이어가 닿으면 문 열기
        }
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
}