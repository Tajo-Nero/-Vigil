using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Singleton
{
    [DefaultExecutionOrder(-2)]

    public abstract class PrimaryMonoSingleton<TPrimaryMonoSingleton> : MonoBehaviour, IPrimaryMonoSingleton<TPrimaryMonoSingleton>

        where TPrimaryMonoSingleton : PrimaryMonoSingleton<TPrimaryMonoSingleton>
    {
        private void Reset()
        {
            this.DisallowMultiple();
        }

        protected virtual void Awake()
        {
            ISingleton<TPrimaryMonoSingleton>.TrySetInstance((TPrimaryMonoSingleton)this);
        }

        protected virtual void OnDestroy()
        {
            ISingleton<TPrimaryMonoSingleton>.Release((TPrimaryMonoSingleton)this);
        }

        bool ISingleton<TPrimaryMonoSingleton>.IsDuplicated()
        {
            if (ISingleton<TPrimaryMonoSingleton>.Instance == null)
            {
                return false;
            }

            return true;
        }
    }
}