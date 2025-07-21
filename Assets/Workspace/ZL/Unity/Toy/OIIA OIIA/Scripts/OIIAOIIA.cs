using UnityEngine;

using ZL.Unity.Motions;

namespace ZL.Unity.OIIAOIIA
{
    [AddComponentMenu("ZL/OIIA OIIA")]

    [DisallowMultipleComponent]

    public sealed class OIIAOIIA : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Alias("OIIA")]

        private GameObject oiia;

        [SerializeField]

        private GameObject maxwell;

        [SerializeField]

        [UsingCustomProperty]

        [AddIndent]

        private AudioSource audioSource;

        [SerializeField]

        [UsingCustomProperty]

        [AddIndent]

        private Spinner spinner;

        [SerializeField]

        [UsingCustomProperty]

        [AddIndent]

        private PingPong pingPong;

        public void StartOIIA(float speed)
        {
            audioSource.pitch = speed;

            spinner.speed = speed;

            pingPong.speed = speed;

            StartOIIA();
        }

        public void StartOIIA()
        {
            oiia.SetActive(false);

            maxwell.SetActive(true);
        }

        public void StopOIIA()
        {
            maxwell.SetActive(false);

            oiia.SetActive(true);
        }
    }
}