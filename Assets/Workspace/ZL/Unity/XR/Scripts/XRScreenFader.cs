using DG.Tweening;

using UnityEngine;

using ZL.Unity.Tweening;

using ZL.Unity.Directing;

namespace ZL.Unity.XR
{
    [AddComponentMenu("ZL/XR/XR Screen Fader")]

    [RequireComponent(typeof(MaterialAlphaTweener))]

    public sealed class XRScreenFader : AlphaFader
    {
        public override void FadeIn(float duration = -1f)
        {
            gameObject.SetActive(true);

            isFadedIn = true;

            onFadeInEvent.Invoke();

            tweener.Tween(1f, duration).OnComplete(OnFadedIn);
        }
    }
}