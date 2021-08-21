using System;
using System.Collections.Generic;


namespace Game
{
    public abstract class BaseController : IExecutable, IFixedExecutable
    {
        #region Fields

        private readonly Dictionary<int, BaseModel> _models = new Dictionary<int, BaseModel>();
           
        #endregion


        #region Properties

        public bool IsEnabled { get; set; } = false;

        public abstract Type AppropriateModelType { get; }

        protected IEnumerable<BaseModel> Models => _models.Values;

        #endregion


        #region IExecutable

        public void Execute(float deltaTime)
        {
            if (IsEnabled)
            {
                foreach (BaseModel model in Models)
                {
                    if (model.IsDestroyingSelf)
                    {
                        continue;
                    }
                    ProcessModel(model, deltaTime);
                }
            }
        }

        #endregion


        #region IFixedExecutable

        public void FixedExecute(float deltaTime)
        {
            if (IsEnabled)
            {
                foreach (BaseModel model in Models)
                {
                    if (model.IsDestroyingSelf)
                    {
                        continue;
                    }
                    FixedProcessModel(model, deltaTime);
                }
            }
        }

        #endregion


        #region Methods

        public void AttachModel(BaseModel model) => _models.Add(model.GetInstanceID(), model);

        public void DetachModel(BaseModel model) => _models.Remove(model.GetInstanceID());

        protected virtual void ProcessModel(BaseModel model, float deltaTime) { }

        protected virtual void FixedProcessModel(BaseModel model, float deltaTime) { }

        #endregion
    }
}