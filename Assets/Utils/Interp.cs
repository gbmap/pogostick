using UnityEngine;

namespace DRAP.Utils
{
    public enum EInterpType
    {
        Linear,
        Quadratic
    }

    public static class Interp
    {
        public static Vector3 Linear(Vector3 a, Vector3 b, float t)
        {
            return a + t * (b - a);
        }

        public static Vector3 Quadratic(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            float u = 1f - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * a;
            p += 2 * u * t * b;
            p += tt * c;
            return p;
        }

        public static Vector3 Cubic(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            return (1f-t)*Quadratic(a,b,c,t)+t*Quadratic(b,c,d,t);
        }
    }
}