using DG.Tweening;

using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Tweening;

namespace ZL.Unity.Tweening
{
    [DisallowMultipleComponent]

    public abstract class AlphaFader : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        protected ComponentValueTweener<FloatTweener, float, float, FloatOptions> tweener;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [ReadOnlyWhenEditMode]

        [Button("ToggleFaded")]

        protected bool isFadedIn = false;

        public bool IsFadedIn
        {
            get => isFadedIn;
        }

        [Space]

        [SerializeField]

        protected UnityEvent onFadeInEvent;

        public UnityEvent OnFadeInEvent => onFadeInEvent;

        [Space]

        [SerializeField]

        protected UnityEvent onFadedInEvent;

        public UnityEvent OnFadedInEvent => onFadedInEvent;

        [Space]

        [SerializeField]

        protected UnityEvent onFadeOutEvent;

        public UnityEvent OnFadeOutEvent => onFadeOutEvent;

        [Space]

        [SerializeField]

        protected UnityEvent onFadedOutEvent;

        public UnityEvent OnFadedOutEvent => onFadedOutEvent;

#if UNITY_EDITOR

        public void ToggleFaded()
        {
            if (isFadedIn == true)
            {
                FadeOut();
            }

            else
            {
                FadeIn();
            }
        }

#endif

        private void Awake()
        {
            if (isFadedIn == true)
            {
                tweener.Value = 1f;

                gameObject.SetActive(true);
            }

            else
            {
                tweener.Value = 0f;

                gameObject.SetActive(false);
            }
        }

        public virtual void FadeIn(float duration = -1f)
        {
            gameObject.SetActive(true);

            isFadedIn = true;

            onFadeInEvent.Invoke();

            tweener.Tween(1f, duration).OnComplete(OnFadedIn);
        }

        protected void OnFadedIn()
        {
            onFadedInEvent.Invoke();
        }

        public void FadeOut(float duration = -1f)
        {
            isFadedIn = false;

            onFadeOutEvent.Invoke();

            tweener.Tween(0f, duration).OnComplete(OnFadedOut);
        }

        protected void OnFadedOut()
        {
            onFadedOutEvent.Invoke();

            gameObject.SetActive(false);
        }
    }
}

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/UGUI Screen")]

    [RequireComponent(typeof(CanvasGroupAlphaTweener))]

    public class UGUIScreen : AlphaFader
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParentOnly]

        [ReadOnly(true)]

        private UGUIScreenGroup screenGroup;

        public override void FadeIn(float duration = -1f)
        {
            screenGroup?.SwapCurrent(this);

            base.FadeIn(duration);
        }
    }
}