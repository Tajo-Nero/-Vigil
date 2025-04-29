using UnityEngine;

namespace ZL.Unity.Motions
{
    public abstract class TransformMotion : MonoBehaviour
    {
        [Space]

        [SerializeField]

        protected Vector3 direction = Vector3.zero;

        [SerializeField]

        protected float speed = 1f;

        protected abstract void Update();
    }
}