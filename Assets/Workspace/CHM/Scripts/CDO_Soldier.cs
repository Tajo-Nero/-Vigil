using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{


    Animator animator;
    AudioSource audioSource;
    private Renderer soldierRenderer; // 두 번째 자식 오브젝트의 Renderer 가져오기

    public bool isMeet = false;
    private bool isLiedown = false; // 엎드려 상태 확인

    private enum AnimationState { Attention, Liedown, Standup, Salute }
    private enum AudioState { Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Attention; // 기본 상태
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    public Texture inspectorTexture; // Inspector에서 할당할 텍스처

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // 두 번째 자식 오브젝트의 Renderer 가져오기
        Transform secondChild = transform.GetChild(1);
        if (secondChild != null)
        {
            soldierRenderer = secondChild.GetComponent<Renderer>();
        }

        // 오디오 클립 등록
        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Salute();
    }

    void Salute()
    {
        SetAnimationState(AnimationState.Salute);        
    }

    public void Rest()
    {
        if (isLiedown)
        {
            SetAnimationState(AnimationState.Standup);
            isLiedown = false;
        }
        else
        {
            SetAnimationState(AnimationState.Attention);
        }
    }

    public void Liedown()
    {
        SetAnimationState(AnimationState.Liedown);
        isLiedown = true;
    }

    void SetAnimationState(AnimationState newState)
    {
        currentState = newState;
        animator.SetBool("isAttention", false);
        animator.SetBool("isLiedown", false);
        animator.SetBool("isStandup", false);
        animator.SetBool("isSalute", false);

        animator.SetBool($"is{newState}", true);
        PlaySound((AudioState)newState);
    }

    void PlaySound(AudioState state)
    {
        if (audioClips.ContainsKey(state) && audioClips[state] != null)
        {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }

    //이름 홍길동으로 바꿈
    public void ApplyInspectorTexture()
    {
        if (soldierRenderer != null && soldierRenderer.material != null && inspectorTexture != null)
        {
            soldierRenderer.material.SetTexture("_MainTex", inspectorTexture);
        }
        else
        {
            Debug.LogWarning("Renderer, Material 또는 텍스처가 설정되지 않았습니다!");
        }
    }
}