using System;
using System.Collections.Generic;


namespace Game
{
    public sealed class BeingsController : BaseController
    {
        #region Fields

        private readonly Dictionary<int, ActionData> _actionData = new Dictionary<int, ActionData>();

        #endregion


        #region Properties

        public override Type AppropriateModelType => typeof(BeingModel);
            
        #endregion


        #region Methods

        protected override void ProcessModel(BaseModel model, float deltaTime)
        {
            base.ProcessModel(model, deltaTime);
            BeingModel being = model as BeingModel;
            _actionData[being.GetInstanceID()] = being.ActionDataProvider.GetActionData();
        }

        protected override void FixedProcessModel(BaseModel model, float deltaTime)
        {
            base.FixedProcessModel(model, deltaTime);
            BeingModel being = model as BeingModel;
            if (_actionData.TryGetValue(being.GetInstanceID(), out var actionData))
            {
                being.Do(actionData, deltaTime);
            }
        }

        #endregion   
    }
}