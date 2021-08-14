using System;
using System.Collections.Generic;


namespace Game
{
    public abstract class BaseController : IExecutable
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
            if (IsEnabled) _Execute(deltaTime);
        }

        #endregion


        #region Methods

        public void AttachModel(BaseModel model) => _models.Add(model.GetInstanceID(), model);

        public void DetachModel(BaseModel model) => _models.Remove(model.GetInstanceID());

        protected abstract void _Execute(float deltaTime);

        #endregion
    }
}