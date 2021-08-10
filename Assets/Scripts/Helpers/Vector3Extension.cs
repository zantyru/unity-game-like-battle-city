using UnityEngine;


namespace Game
{
    public static class Vector3Extension
    {
        #region Methods

        public static float CrossXY(this Vector3 self, Vector3 other)
        {
            return self.x * other.y - self.y * other.x;
        }
        
        public static bool IsXYCollineary(this Vector3 self, Vector3 other)
        {
            return Mathf.Abs(self.CrossXY(other)) < Mathf.Epsilon;
        }

        #endregion
    }
}