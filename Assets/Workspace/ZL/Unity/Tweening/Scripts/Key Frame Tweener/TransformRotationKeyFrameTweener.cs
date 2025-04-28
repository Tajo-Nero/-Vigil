using DG.Tweening;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Rotation Key Frame Tweener")]

    [RequireComponent(typeof(TransformRotationTweener))]

    public sealed class TransformRotationKeyFrameTweener : KeyFrameTweener<TransformRotationTweener, QuaternionTweener, Quaternion, Vector3, QuaternionOptions>
    {
        [Space]

        [SerializeField]

        private RotateMode rotateMode;

        public override void SetKeyFrame(int index)
        {
            keyFrames.Index = index;

            if (keyFrames.TryGetCurrent(out var eulerAngles) == true)
            {
                transform.rotation = Quaternion.Euler(eulerAngles);
            }
        }

        protected override void TweenKeyFrame()
        {
            if (keyFrames.TryGetCurrent(out var eulerAngles) == true)
            {
                componentTweener.Tween(eulerAngles).SetRotateMode(rotateMode);
            }
        }
    }
}