using UnityEngine;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Application Manager Receiver")]

    [DisallowMultipleComponent]

    public sealed class ApplicationManagerReceiver : MonoSingletonReceiver<ApplicationManager>
    {
        public BoolPref RunInBackgroundPref
        {
            get => Target.RunInBackgroundPref;
        }

        public IntPref TargetFrameRatePref
        {
            get => Target.TargetFrameRatePref;
        }

        public void Quit()
        {
            Target.Quit();
        }
    }
}