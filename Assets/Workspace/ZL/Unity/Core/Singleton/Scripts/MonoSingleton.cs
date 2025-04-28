using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Singleton
{
    [DefaultExecutionOrder(-1)]

    [DisallowMultipleComponent]

    public abstract class MonoSingleton<TMonoSingleton> : MonoBehaviour, ISingleton<TMonoSingleton>

        where TMonoSingleton : MonoSingleton<TMonoSingleton>
    {
        protected virtual void Awake()
        {
            ISingleton<TMonoSingleton>.TrySetInstance((TMonoSingleton)this);
        }

        protected virtual void OnDestroy()
        {
            ISingleton<TMonoSingleton>.Release((TMonoSingleton)this);
        }
    }
}