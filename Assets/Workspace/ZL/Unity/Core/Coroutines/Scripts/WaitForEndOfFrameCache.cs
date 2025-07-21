using UnityEngine;

namespace ZL.Unity.Coroutines
{
    public static class WaitForEndOfFrameCache
    {
        private static readonly WaitForEndOfFrame waitForEndOfFrame;

        static WaitForEndOfFrameCache()
        {
            waitForEndOfFrame = new();
        }

        public static WaitForEndOfFrame Get()
        {
            return waitForEndOfFrame;
        }
    }
}