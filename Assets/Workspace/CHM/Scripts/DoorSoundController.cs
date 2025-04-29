using UnityEngine;
using System.Collections;

public class DoorSoundController : MonoBehaviour
{
    public AudioClip doorOpeningSound;
    private AudioSource audioSource;
    private bool isOpening = false;
    public float maxOpenAngle = 90f; // 최대 열림 각도
    public float openSpeed = 30f; // 문이 열리는 속도

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOpening)
        {
            isOpening = true;
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        float soundDuration = doorOpeningSound.length;
        audioSource.PlayOneShot(doorOpeningSound);
        StartCoroutine(OpenDoorGradually(soundDuration));
    }

    IEnumerator OpenDoorGradually(float duration)
    {
        float startTime = Time.time;
        float startAngle = transform.rotation.eulerAngles.y;
        float targetAngle = Mathf.Clamp(startAngle + maxOpenAngle, startAngle, 180f);

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            float newAngle = Mathf.Lerp(startAngle, targetAngle, t);
            transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
            yield return null;
        }
    }
}