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
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 초기화
    }

    private void OnEnable()
    {
        StartCoroutine(StartEffectAfterDelay(5f)); // 5초 후 효과 시작
    }

    private void OnDisable()
    {
        ResetMaterials(); // 스크립트가 비활성화될 때 머테리얼 초기화

        if (audioSource != null)
        {
            audioSource.Stop(); // 오디오 중지
        }
    }

    private IEnumerator StartEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // delay만큼 대기

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            lastPlayerPosition = player.transform.position;
        }

        if (audioSource != null)
        {
            audioSource.Play(); // 오디오 시작
        }

        StartCoroutine(ChangeMaterialColor()); // 머테리얼 색 변경 시작
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

            yield return null; // 다음 프레임까지 대기
        }
    }

    // 모든 머테리얼을 흰색으로 초기화하는 함수
    private void ResetMaterials()
    {
        for (int i = 0; i < roomMaterials.Length; i++)
        {
            colorSaturation[i] = 0f;
            roomMaterials[i].color = Color.white;
        }
    }
}