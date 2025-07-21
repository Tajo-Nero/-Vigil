using System.Linq;
using UnityEngine;

public class ReceiverSound : MonoBehaviour
{
    private PhoneBox_Ctrl _phoneBoxCtrl;
    public Texture newTexture;   

    private MeshRenderer phoneBoxRenderer;
    private MeshRenderer firstChildRenderer;
    private Light childLight;
    private bool isSoundPlaying = false;
    private float maxRange = 10f;
    private float maxIntensity = 10f;
    private float maxSpotAngle = 120f;
    private float increaseRate = 0.1f;

    void Start()
    {
        _phoneBoxCtrl = GetComponentInParent<PhoneBox_Ctrl>();
        if (_phoneBoxCtrl == null) return;

        phoneBoxRenderer = _phoneBoxCtrl.transform.GetChild(1).GetComponent<MeshRenderer>();
        firstChildRenderer = phoneBoxRenderer.transform.GetChild(0).GetComponent<MeshRenderer>();
        childLight = _phoneBoxCtrl.GetComponentInChildren<Light>();

        if (childLight != null)
        {
            childLight.range = 5f;
            childLight.intensity = 4f;
            childLight.spotAngle = 100f;
        }        
    }

    public void OnSelectEntered()
    {
        _phoneBoxCtrl?.StopSound();
        isSoundPlaying = false;
        CancelInvoke("RandomizeLight");

        if (childLight != null)
        {
            childLight.enabled = false;
            childLight.range = 5f;
            childLight.intensity = 4f;
            childLight.spotAngle = 100f;
        }

        ChangeMaterial(phoneBoxRenderer);
        ChangeMaterial(firstChildRenderer);
        ChangeLightColor();       
    }

    private void ChangeMaterial(MeshRenderer renderer)
    {
        if (renderer == null) return;

        Material[] materials = renderer.materials;
        if (materials.Length > 2)
        {
            materials[2].SetTexture("_MainTex", newTexture);
            renderer.materials = materials.ToArray();
        }
    }

    private void ChangeLightColor()
    {
        if (childLight == null) return;

        childLight.color = Color.red;
        StartLightBlinking();
    }

    private void StartLightBlinking()
    {
        if (childLight == null) return;

        isSoundPlaying = true;
        InvokeRepeating("RandomizeLight", 0f, Random.Range(0.1f, 0.5f));
    }

    private void RandomizeLight()
    {
        if (!isSoundPlaying || childLight == null) return;

        childLight.enabled = !childLight.enabled;
        float nextToggleTime = Random.Range(0.2f, 0.5f);
        Invoke("RandomizeLight", nextToggleTime);

        if (childLight.range < maxRange)
            childLight.range += increaseRate;

        if (childLight.intensity < maxIntensity)
            childLight.intensity += increaseRate;

        if (childLight.spotAngle < maxSpotAngle)
            childLight.spotAngle += increaseRate * 2;
    }
}