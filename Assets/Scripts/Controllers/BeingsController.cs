using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public sealed class BeingsController : BaseController, IInitializable, IFixedExecutable
    {
        #region Fields

        private BeingModel[] _beingModels = default;
        private readonly Dictionary<int, ActionData> _actionData = new Dictionary<int, ActionData>();

        #endregion


        #region IInitializable

        public void Initialize()
        {
            _beingModels = Object.FindObjectsOfType<BeingModel>();
        }

        #endregion


        #region IFixedExecutable
        
        public void FixedExecute(float deltaTime)
        {
            foreach (BeingModel being in _beingModels)
            {
                if (_actionData.TryGetValue(being.GetInstanceID(), out var actionData))
                {
                    being.Do(actionData, deltaTime);
                }
                // being.Do(_actionData[being.GetInstanceID()], deltaTime);
            }
        }

        #endregion


        #region Methods

        protected override void _Execute(float deltaTime)
        {
            foreach (BeingModel being in _beingModels)
            {
                _actionData[being.GetInstanceID()] = being.ActionDataProvider.GetActionData();
            }
        }

        #endregion   
    }
}