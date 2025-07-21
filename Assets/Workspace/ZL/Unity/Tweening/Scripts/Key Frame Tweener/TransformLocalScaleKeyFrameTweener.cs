using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Transform Local Scale Key Frame Tweener")]

    [RequireComponent(typeof(TransformLocalScaleTweener))]

    public sealed class TransformLocalScaleKeyFrameTweener : KeyFrameTweener<TransformLocalScaleTweener, Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        public override void SetKeyFrame(int index)
        {
            keyFrames.Index = index;

            if (keyFrames.TryGetCurrent(out var localScale) == true)
            {
                transform.localScale = localScale;
            }
        }
    }
}