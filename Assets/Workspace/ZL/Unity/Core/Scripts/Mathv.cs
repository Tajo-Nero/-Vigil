using System;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class Mathv
    {
        public static Vector2 Round(in Vector2 v, int digits)
        {
            return new()
            {
                x = MathF.Round(v.x, digits),

                y = MathF.Round(v.y, digits)
            };
        }

        public static Vector3 Round(in Vector3 v, int digits)
        {
            return new()
            {
                x = MathF.Round(v.x, digits),

                y = MathF.Round(v.y, digits),

                z = MathF.Round(v.z, digits)
            };
        }
    }
}