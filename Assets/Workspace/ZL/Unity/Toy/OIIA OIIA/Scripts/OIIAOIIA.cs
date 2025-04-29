using UnityEngine;

namespace ZL.Unity.OIIAOIIA
{
    [AddComponentMenu("ZL/OIIAOIIA/OIIAOIIA")]

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