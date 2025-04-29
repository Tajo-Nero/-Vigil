using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{


    Animator animator;
    AudioSource audioSource;
    private Renderer soldierRenderer; // �� ��° �ڽ� ������Ʈ�� Renderer ��������

    public bool isMeet = false;
    private bool isLiedown = false; // ����� ���� Ȯ��

    private enum AnimationState { Attention, Liedown, Standup, Salute }
    private enum AudioState { Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Attention; // �⺻ ����
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    public Texture inspectorTexture; // Inspector���� �Ҵ��� �ؽ�ó

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // �� ��° �ڽ� ������Ʈ�� Renderer ��������
        Transform secondChild = transform.GetChild(1);
        if (secondChild != null)
        {
            soldierRenderer = secondChild.GetComponent<Renderer>();
        }

        // ����� Ŭ�� ���
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

    //�̸� ȫ�浿���� �ٲ�
    public void ApplyInspectorTexture()
    {
        if (soldierRenderer != null && soldierRenderer.material != null && inspectorTexture != null)
        {
            soldierRenderer.material.SetTexture("_MainTex", inspectorTexture);
        }
        else
        {
            Debug.LogWarning("Renderer, Material �Ǵ� �ؽ�ó�� �������� �ʾҽ��ϴ�!");
        }
    }
}