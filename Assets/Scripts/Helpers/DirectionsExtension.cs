using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public static class DirectionsExtension
    {
        #region Fields
        
        private static readonly Dictionary<Directions, Vector3> _mappingVector3
            = new Dictionary<Directions, Vector3>()
                {
                    {Directions.NONE, Vector3.zero},
                    {Directions.LEFT, Vector3.left},
                    {Directions.RIGHT, Vector3.right},
                    {Directions.UP, Vector3.up},
                    {Directions.DOWN, Vector3.down},
                };
        
        private static readonly Dictionary<Directions, Vector3> _mappingVector2
            = new Dictionary<Directions, Vector3>()
                {
                    {Directions.NONE, Vector2.zero},
                    {Directions.LEFT, Vector2.left},
                    {Directions.RIGHT, Vector2.right},
                    {Directions.UP, Vector2.up},
                    {Directions.DOWN, Vector2.down},
                };

        #endregion


        #region Methods

        public static Vector3 GetVector3(this Directions direction) => _mappingVector3[direction];

        public static Vector2 GetVector2(this Directions direction) => _mappingVector2[direction];

        #endregion
    }
}