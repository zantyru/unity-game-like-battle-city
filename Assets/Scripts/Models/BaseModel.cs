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

        public static IEnumerable<BaseModel> JustInstantiatedObjects => _justInstantiatedObjects;

        public static IEnumerable<BaseModel> DeadObjects => _deadObjects;
        
        public Vector3 Position { get => this.transform.position; set => this.transform.position = value; }

        public bool IsDestroyingSelf { get; private set; } = false;

        public bool IsDestroyingGameObject { get; private set; } = false;

        #endregion


        #region Methods

        public static void ClearJustInstantiatedObjects() => _justInstantiatedObjects.Clear();

        public static void ClearDeadObjects()
        {
            foreach (BaseModel model in _deadObjects)
            {
                Object.Destroy(model.IsDestroyingGameObject ? (Object)model.gameObject : (Object)model);
            }
            _deadObjects.Clear();
        }

        public virtual void DestroyGameObject()
        {
            IsDestroyingGameObject = true;
            BaseModel[] models = GetComponents<BaseModel>();
            foreach (BaseModel model in models)
            {   
                model.DestroySelf();
            }
        }

        public virtual void DestroySelf()
        {
            IsDestroyingSelf = true;
            _deadObjects.Enqueue(this);
        }

        protected virtual void Awake() => _justInstantiatedObjects.Enqueue(this);

        private void OnDestroy()
        {
            // If the destruction initialized somebody else
            if (!IsDestroyingSelf)
            {
                DestroySelf();
            }
        }

        #endregion
    }
}