using System.Collections;

using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.UI;

using ZL.Unity.Coroutines;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Auto Button")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(Button))]

    public sealed class AutoButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private Button button;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Alias("Threshold")]

        private float autoClickThreshold = 0.5f;

        [SerializeField]

        [UsingCustomProperty]

        [Text("Interval")]

        [AddIndent]

        [Alias("Use Curve")]

        private bool useAutoClickIntervalCurve = false;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(useAutoClickIntervalCurve), true)]

        [AddIndent]

        [Alias("")]

        private float autoClickInterval = 0.1f;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(useAutoClickIntervalCurve), false)]

        [AddIndent]

        [Alias("")]

        private AnimationCurve autoClickIntervalCurve;

        private float pressedTime = 0f;

        private void Reset()
        {
            var keyframes = new Keyframe[]
            {
                new(0f, 1f),

                new(3f, 0.1f),
            };

            autoClickIntervalCurve = new(keyframes);

            FixedAnimationUtility.SetKeyRightTangentMode(autoClickIntervalCurve, 0, FixedTangentMode.Auto);

            FixedAnimationUtility.SetKeyLeftTangentMode(autoClickIntervalCurve, 1, FixedTangentMode.Auto);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (button.interactable == false)
            {
                return;
            }

            StartAutoClick();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopAutoClick();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (button.interactable == false)
            {
                return;
            }

            if (pressedTime > 0f || autoClickThreshold == 0f)
            {
                pressedTime = 0f;
            }

            else
            {
                button.onClick.Invoke();
            }
        }

        private void StartAutoClick()
        {
            if (autoClickRoutine != null)
            {
                return;
            }

            autoClickRoutine = AutoClickRoutine();

            StartCoroutine(autoClickRoutine);
        }

        private void StopAutoClick()
        {
            if (autoClickRoutine == null)
            {
                return;
            }

            StopCoroutine(autoClickRoutine);

            autoClickRoutine = null;
        }

        private IEnumerator autoClickRoutine = null;

        private IEnumerator AutoClickRoutine()
        {
            pressedTime = 0f;

            float interval = autoClickThreshold;

            if (useAutoClickIntervalCurve == true)
            {
                while (true)
                {   
                    yield return WaitForInterval(interval);

                    interval = autoClickIntervalCurve.Evaluate(pressedTime);
                }
            }

            else
            {
                while (true)
                {
                    yield return WaitForInterval(interval);

                    interval = autoClickInterval;
                }
            }
        }

        private IEnumerator WaitForInterval(float seconds)
        {
            yield return WaitForSecondsCache.Get(seconds);

            pressedTime += seconds;

            button.onClick.Invoke();
        }
    }
}