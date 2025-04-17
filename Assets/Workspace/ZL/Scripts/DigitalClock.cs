using System.Collections;

using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Digital Clock")]

    [DisallowMultipleComponent]

    public sealed class DigitalClock : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private TextMeshProUGUI text;

        [Space]

        [SerializeField]

        private bool isRunning = true;

        [Space]

        [SerializeField]

        private int hour = 0;

        public int Hour
        {
            get => hour;

            set
            {
                hour = value;

                if (hour < 0 || 23 < hour)
                {
                    hour = 0;
                }
            }
        }

        [SerializeField]

        private int minute = 0;

        public int Minute
        {
            get => minute;

            set
            {
                minute = value;

                if (minute > 59)
                {
                    minute = 0;

                    Hour += 1;
                }

                else if (minute < 0)
                {
                    minute = 59;

                    Hour -= 1;
                }
            }
        }

        [SerializeField]

        private float seconds = 0f;

        public float Seconds
        {
            get => seconds;

            set
            {
                seconds = value;

                if (seconds >= 60f)
                {
                    seconds = Mathf.Repeat(seconds, 60f);

                    Minute += 1;
                }

                else if (seconds < 0f)
                {
                    seconds = Mathf.Repeat(seconds, 60f);

                    Minute -= 1;
                }
            }
        }

        private void OnValidate()
        {
            Seconds = seconds;

            Minute = minute;

            Hour = hour;

            if (text != null)
            {
                text.text = $"{hour:D2}:{minute:D2}";
            }
        }

        private void OnEnable()
        {
            StartCoroutine(BlinkingColonRoutine());
        }

        private void Update()
        {
            if (isRunning == false)
            {
                return;
            }

            Seconds += Time.deltaTime;
        }

        private IEnumerator BlinkingColonRoutine()
        {
            while (true)
            {
                text.text = $"{hour:D2}:{minute:D2}";

                yield return new WaitForSeconds(0.5f);

                text.text = $"{hour:D2} {minute:D2}";

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}