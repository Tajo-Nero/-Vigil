using DG.Tweening;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Tweening;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/UGUI Screen")]

    public sealed class UGUIScreen : CanvasGroupAlphaTweener
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParentOnly]

        [ReadOnly(true)]

        private UGUIScreenGroup screenGroup;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [ReadOnlyWhenEditMode]

        [Button("ToggleFaded")]

        private bool isFadedIn = false;

        public bool IsFadedIn
        {
            get => isFadedIn;
        }

        [Space]

        [SerializeField]

        private UnityEvent onFadeInEvent;

        public UnityEvent OnFadeInEvent => onFadeInEvent;

        [Space]

        [SerializeField]

        private UnityEvent onFadedInEvent;

        public UnityEvent OnFadedInEvent => onFadedInEvent;

        [Space]

        [SerializeField]

        private UnityEvent onFadeOutEvent;

        public UnityEvent OnFadeOutEvent => onFadeOutEvent;

        [Space]

        [SerializeField]

        private UnityEvent onFadedOutEvent;

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

        protected override void Awake()
        {
            base.Awake();

            if (isFadedIn == true)
            {
                canvasGroup.alpha = 1f;

                gameObject.SetActive(true);
            }

            else
            {
                canvasGroup.alpha = 0f;

                gameObject.SetActive(false);
            }
        }

        public void FadeIn()
        {
            screenGroup?.SwapCurrent(this);

            gameObject.SetActive(true);

            isFadedIn = true;

            onFadeInEvent.Invoke();

            Tween(1f).OnComplete(OnFadedIn);
        }

        public void FadeIn(float fadeDuration)
        {
            screenGroup?.SwapCurrent(this);

            gameObject.SetActive(true);

            isFadedIn = true;

            onFadeInEvent.Invoke();

            Tween(1f, fadeDuration).OnComplete(OnFadedIn);
        }

        private void OnFadedIn()
        {
            onFadedInEvent.Invoke();
        }

        public void FadeOut()
        {
            isFadedIn = false;

            onFadeOutEvent.Invoke();

            Tween(0f).OnComplete(OnFadedOut);
        }

        public void FadeOut(float fadeDuration)
        {
            isFadedIn = false;

            onFadeOutEvent.Invoke();

            Tween(0f, fadeDuration).OnComplete(OnFadedOut);
        }

        private void OnFadedOut()
        {
            onFadedOutEvent.Invoke();

            gameObject.SetActive(false);
        }
    }
}