using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Material Alpha Tweener")]

    public sealed class MaterialAlphaTweener : ComponentValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        private Material material;

        public Material Material
        {
            get => material;

            set
            {
                material = value;

                color = material.color;
            }
        }

        private Color color;

        public override float Value
        {
            get => color.a;

            set
            {
                color.a = value;

                material.color = color;
            }
        }

        public TweenerCore<float, float, FloatOptions> Tween(Material material, float endValue, float duration = -1f)
        {
            Material = material;

            return Tween(endValue, duration);
        }
    }
}