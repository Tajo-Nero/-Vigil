using UnityEngine;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Application Manager (Singleton)")]

    public sealed class ApplicationManager : MonoSingleton<ApplicationManager>
    {
        [Space]

        [SerializeField]

        private BoolPref runInBackgroundPref = new("Run In Background", true);

        public BoolPref RunInBackgroundPref
        {
            get => runInBackgroundPref;
        }

        [Space]

        [SerializeField]

        private IntPref targetFrameRatePref = new("Target Frame Rate", 60);

        public IntPref TargetFrameRatePref
        {
            get => targetFrameRatePref;
        }

        protected override void Awake()
        {
            base.Awake();

            runInBackgroundPref.OnValueChangedAction += (value) =>
            {
                Application.runInBackground = value;
            };

            runInBackgroundPref.TryLoadValue();

            targetFrameRatePref.OnValueChangedAction += (value) =>
            {
                Application.targetFrameRate = value;
            };

            targetFrameRatePref.TryLoadValue();
        }

        public static void Pause()
        {
            FixedTime.Pause();
        }

        public static void Resume()
        {
            FixedTime.Resume();
        }

        public void Quit()
        {
            FixedApplication.Quit();
        }
    }
}