using UnityEngine;

using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [Space]

    [SerializeField]

    private UnityEvent onCollisionEnterEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onCollisionEnterEvent.Invoke();
        }
    }
}