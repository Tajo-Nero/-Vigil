using DG.Tweening.Plugins.Options;

using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Audio Listener Volume Tweener (Singleton)")]

    public sealed class AudioListenerVolumeTweener : ComponentValueTweener<FloatTweener, float, float, FloatOptions>, ISingleton<AudioListenerVolumeTweener>
    {
        protected override float Value
        {
            get => AudioListener.volume;
            
            set => AudioListener.volume = value;
        }

        protected override void Awake()
        {
            if (ISingleton<AudioListenerVolumeTweener>.TrySetInstance(this) == true)
            {
                base.Awake();
            }
        }

        private void OnDestroy()
        {
            ISingleton<AudioListenerVolumeTweener>.Release(this);
        }
    }
}