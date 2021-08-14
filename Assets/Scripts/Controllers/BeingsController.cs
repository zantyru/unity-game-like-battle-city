using System;
using System.Collections.Generic;


namespace Game
{
    public sealed class BeingsController : BaseController, IFixedExecutable
    {
        #region Fields

        private readonly Dictionary<int, ActionData> _actionData = new Dictionary<int, ActionData>();

        #endregion


        #region Properties

        public override Type AppropriateModelType => typeof(BeingModel);
            
        #endregion


        #region IFixedExecutable
        
        public void FixedExecute(float deltaTime)
        {
            foreach (BeingModel being in base.Models)
            {
                if (_actionData.TryGetValue(being.GetInstanceID(), out var actionData))
                {
                    being.Do(actionData, deltaTime);
                }
            }
        }

        #endregion


        #region Methods

        protected override void _Execute(float deltaTime)
        {
            foreach (BeingModel being in base.Models)
            {
                _actionData[being.GetInstanceID()] = being.ActionDataProvider.GetActionData();
            }
        }

        #endregion   
    }
}