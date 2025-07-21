using System.Collections.Generic;
using UnityEngine;

public class CDO_Soldier : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    private Renderer soldierRenderer;

    private bool isLiedown = false;
    private bool isTrigger = false; // Ʈ���� �ȿ� �ִ��� Ȯ���ϴ� ����
    public bool isMeet = false; 

    private enum AnimationState { Attention, Liedown, Standup, Salute }
    private enum AudioState { Attention, Liedown, Standup, Salute }

    private AnimationState currentState = AnimationState.Attention;
    private Dictionary<AudioState, AudioClip> audioClips = new Dictionary<AudioState, AudioClip>();

    public AudioClip attentionSound;
    public AudioClip liedownSound;
    public AudioClip standupSound;
    public AudioClip saluteSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioClips.Add(AudioState.Attention, attentionSound);
        audioClips.Add(AudioState.Liedown, liedownSound);
        audioClips.Add(AudioState.Standup, standupSound);
        audioClips.Add(AudioState.Salute, saluteSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isTrigger = true; // �÷��̾ Ʈ���� �ȿ� ���� �� true ����
        Salute();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isTrigger = false; // �÷��̾ Ʈ���ſ��� ����� �� false ����
    }

    void Salute()
    {
        if (isTrigger) SetAnimationState(AnimationState.Salute);
    }

    public void Rest()
    {
        if (!isTrigger) return; // Ʈ���� ���� ���� ������ ���� �� ��

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
        if (!isTrigger) return; // Ʈ���� ���� ���� ������ ���� �� ��

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
}