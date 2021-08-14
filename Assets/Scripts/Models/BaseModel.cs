using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public abstract class BaseModel : MonoBehaviour
    {
        #region Fields

        private static readonly Queue<BaseModel> _justInstantiatedObjects = new Queue<BaseModel>();  // Queue: because no need indexing
        private static readonly Queue<BaseModel> _deadObjects = new Queue<BaseModel>();

        #endregion


        #region Properties
        
        public Vector3 Position
        {
            get => this.transform.position;
            set => this.transform.position = value;
        }

        public static IEnumerable<BaseModel> JustInstantiatedObjects => _justInstantiatedObjects;

        public static IEnumerable<BaseModel> DeadObjects => _deadObjects;

        #endregion


        #region Methods

        public static void ClearJustInstantiatedObjects() => _justInstantiatedObjects.Clear();

        public static void ClearDeadObjects() => _deadObjects.Clear();

        protected virtual void Awake() => _justInstantiatedObjects.Enqueue(this);

        #endregion
    }
}