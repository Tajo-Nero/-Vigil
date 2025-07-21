using UnityEngine;

namespace ZL.Unity.Vigil
{
    [AddComponentMenu("ZL/Vigil/Anomaly")]

    [DisallowMultipleComponent]

    public sealed class Anomaly : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private Animator animator;

        [SerializeField]

        private GameObject exchangeTarget;

        private Pose poseOrigin;

        private void Awake()
        {
            poseOrigin = new(transform.position, transform.rotation);
        }

        private void OnEnable()
        {
            if (exchangeTarget != null)
            {
                exchangeTarget.SetActive(false);
            }
        }

        private void OnDisable()
        {
            transform.SetPositionAndRotation(poseOrigin);

            if (animator != null)
            {
                animator.Rebind();
            }
            
            if (exchangeTarget != null)
            {
                exchangeTarget.SetActive(true);
            }
        }
    }
}