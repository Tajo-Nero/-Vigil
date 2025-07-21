using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Transform Local Position Tweener")]

    public sealed class TransformLocalPositionTweener : ComponentValueTweener<Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        public override Vector3 Value
        {
            get => transform.localPosition;

            set => transform.localPosition = value;
        }
    }
}