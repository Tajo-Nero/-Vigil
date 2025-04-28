using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/Audio Mixer Manager Receiver")]

    [DisallowMultipleComponent]

    public sealed class AudioMixerManagerReceiver : MonoSingletonReceiver<AudioMixerManager>
    {
        public void LoadVolumes()
        {
            Target.LoadVolumes();
        }

        public void SaveVolumes()
        {
            Target.SaveVolumes();
        }

        public float GetVolume(string key)
        {
            return Target.GetVolume(key);
        }

        public void SetVolume(string key, float value)
        {
            Target.SetVolume(key, value);
        }
    }
}