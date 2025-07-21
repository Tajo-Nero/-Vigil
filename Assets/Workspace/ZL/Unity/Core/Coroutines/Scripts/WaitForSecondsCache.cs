using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Coroutines
{
    public static partial class WaitForSecondsCache
    {
        private static readonly Dictionary<float, WaitForSeconds> waitForSecondsDictionary;

        static WaitForSecondsCache()
        {
            waitForSecondsDictionary = new(WaitForSecondsComparerCache.Get());
        }

        public static WaitForSeconds Get(float seconds)
        {
            if (waitForSecondsDictionary.TryGetValue(seconds, out var waitForSeconds) == false)
            {
                waitForSecondsDictionary.Add(seconds, waitForSeconds = new(seconds));
            }

            return waitForSeconds;
        }

        public static void Clear()
        {
            waitForSecondsDictionary.Clear();
        }
    }
}