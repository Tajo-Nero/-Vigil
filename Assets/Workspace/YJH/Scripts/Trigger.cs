using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZL.Unity.Vigil;

public class Trigger : MonoBehaviour
{
    public UnityEvent onCollisionEnterEvent;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onCollisionEnterEvent.Invoke();
        }
    }
}