using UnityEngine;
using System.Collections;

public class RoomThreeTrigger : MonoBehaviour
{
    public Material[] roomMaterials;
    public float saturationIncreaseSpeed = 0.1f;
    private float[] colorSaturation;
    private Vector3 lastPlayerPosition;
    private AudioSource audioSource;

    void Start()
    {
        colorSaturation = new float[roomMaterials.Length];
        audioSource = GetComponent<AudioSource>(); // ����� �ҽ� �ʱ�ȭ
    }

    private void OnEnable()
    {
        StartCoroutine(StartEffectAfterDelay(5f)); // 5�� �� ȿ�� ����
    }

    private void OnDisable()
    {
        ResetMaterials(); // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� ���׸��� �ʱ�ȭ

        if (audioSource != null)
        {
            audioSource.Stop(); // ����� ����
        }
    }

    private IEnumerator StartEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // delay��ŭ ���

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            lastPlayerPosition = player.transform.position;
        }

        if (audioSource != null)
        {
            audioSource.Play(); // ����� ����
        }

        StartCoroutine(ChangeMaterialColor()); // ���׸��� �� ���� ����
    }

    private IEnumerator ChangeMaterialColor()
    {
        while (true)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null && player.transform.position != lastPlayerPosition)
            {
                lastPlayerPosition = player.transform.position;

                for (int i = 0; i < roomMaterials.Length; i++)
                {
                    colorSaturation[i] += saturationIncreaseSpeed * Time.deltaTime;
                    colorSaturation[i] = Mathf.Clamp(colorSaturation[i], 0f, 1f);

                    roomMaterials[i].color = Color.HSVToRGB(0f, colorSaturation[i], 1f);
                }
            }

            yield return null; // ���� �����ӱ��� ���
        }
    }

    // ��� ���׸����� ������� �ʱ�ȭ�ϴ� �Լ�
    private void ResetMaterials()
    {
        for (int i = 0; i < roomMaterials.Length; i++)
        {
            colorSaturation[i] = 0f;
            roomMaterials[i].color = Color.white;
        }
    }
}