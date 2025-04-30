using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZL.Unity.Vigil;

public class Trigger : MonoBehaviour
{
    public MainSceneDirector MSD;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "DownStair")
        {
            MSD.LoadNextLevel(true);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "UpStair")
        {
            MSD.LoadNextLevel(false);
        }
    }
}