using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Material[] roomMaterials;
    public float saturationIncreaseSpeed = 0.1f;
    private float[] colorSaturation;
    private bool playerInside = false;
    private Vector3 lastPlayerPosition;
    private AudioSource audioSource;

    void Start()
    {
        colorSaturation = new float[roomMaterials.Length];
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 초기화
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            lastPlayerPosition = other.transform.position;

            for (int i = 0; i < roomMaterials.Length; i++)
            {
                colorSaturation[i] = 0f;
            }

            if (audioSource != null)
            {
                audioSource.Play(); // 오디오 시작
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            for (int i = 0; i < roomMaterials.Length; i++)
            {
                colorSaturation[i] = 0f;
                roomMaterials[i].color = Color.white;
            }

            if (audioSource != null)
            {
                audioSource.Stop(); // 오디오 중지
            }
        }
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (playerInside && player != null)
        {
            if (player.transform.position != lastPlayerPosition)
            {
                lastPlayerPosition = player.transform.position;

                for (int i = 0; i < roomMaterials.Length; i++)
                {
                    colorSaturation[i] += saturationIncreaseSpeed * Time.deltaTime;
                    colorSaturation[i] = Mathf.Clamp(colorSaturation[i], 0f, 1f);

                    roomMaterials[i].color = Color.HSVToRGB(0f, colorSaturation[i], 1f);
                }
            }
        }
    }
}