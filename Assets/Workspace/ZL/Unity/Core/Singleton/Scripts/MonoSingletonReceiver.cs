using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Singleton
{
    public abstract class MonoSingletonReceiver<TMonoSingleton> : MonoBehaviour

        where TMonoSingleton : MonoBehaviour, ISingleton<TMonoSingleton>
    {
        protected TMonoSingleton Target
        {
            get => ISingleton<TMonoSingleton>.Instance;
        }
    }
}