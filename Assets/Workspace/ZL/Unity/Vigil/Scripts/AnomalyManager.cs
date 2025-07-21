using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Collections;

namespace ZL.Unity.Vigil
{
    [AddComponentMenu("ZL/Vigil/Anomaly Manager")]

    [DisallowMultipleComponent]

    public sealed class AnomalyManager : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private Anomaly[] anomalies;

        private LinkedList<Anomaly> anomaliesQueue = new();

        private Anomaly currentAnomaly = null;

        public void OccurRandomAnomaly()
        {
            if (anomaliesQueue.Count == 0)
            {
                anomalies.CopyTo(ref anomaliesQueue);

                return;
            }

            if (RandomEx.Toss(1f) == true)
            {
                var node = RandomEx.Get(anomaliesQueue);

                anomaliesQueue.Remove(node);

                currentAnomaly = node.Value;

                currentAnomaly.SetActive(true);
            }
        }

        public bool GuessAnomaly(bool isThere)
        {
            return isThere == (currentAnomaly != null);
        }

        public void DisableCurrentAnomaly()
        {
            if (currentAnomaly != null)
            {
                currentAnomaly.SetActive(false);

                currentAnomaly = null;
            }
        }
    }
}