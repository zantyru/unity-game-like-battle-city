using System;
using System.Collections.Generic;


namespace Game
{
    public sealed class WorldController : BaseController
    {
        #region Fields

        private readonly BaseController[] _controllers;
        private readonly IFixedExecutable[] _fixedExecutableControllers;
        private readonly Dictionary<Type, List<BaseController>> _mapping = new Dictionary<Type, List<BaseController>>();
        
        #endregion


        #region Properties
        
        public override Type AppropriateModelType => typeof(BaseModel);

        #endregion


        #region ClassLifeCycle

        public WorldController()
        {
            var beingsController = new BeingsController();

            _controllers = new BaseController[]
            {
                beingsController,
                // Place controller instances here
            };

            _fixedExecutableControllers = new IFixedExecutable[]
            {
                beingsController,
                // Place controller instances with fixed execution method here
            };

            base.IsEnabled = true;
            foreach (BaseController controller in _controllers)
            {
                Type controllerModelType = controller.AppropriateModelType;
                List<BaseController> controllersForModel = default;
                if (!_mapping.TryGetValue(controllerModelType, out controllersForModel))
                {
                    controllersForModel = new List<BaseController>();
                    _mapping[controllerModelType] = controllersForModel;
                }
                controllersForModel.Add(controller);

                controller.IsEnabled = true;
            }
        }

        #endregion


        #region Methods

        public void FixedExecute(float deltaTime)
        {
            foreach (IFixedExecutable controller in _fixedExecutableControllers) 
            {
                controller.FixedExecute(deltaTime);
            }
        }

        protected override void _Execute(float deltaTime)
        {
            ProcessJustInstantiatedObjects();
            foreach (BaseController controller in _controllers)
            {
                controller.Execute(deltaTime);
            }
            ProcessDeadObjects();
        }

        private void ProcessJustInstantiatedObjects()
        {
            foreach (BaseModel model in BaseModel.JustInstantiatedObjects)
            {
                foreach (List<BaseController> controllersForModel in _mapping.Values)                
                {
                    foreach (BaseController controller in controllersForModel)
                    {
                        Type modelType = model.GetType();
                        Type appropriateModelType = controller.AppropriateModelType;
                        if (modelType.IsSubclassOf(appropriateModelType) || modelType == appropriateModelType)
                        {
                            controller.AttachModel(model);
                        }
                    }
                }
            }
            BaseModel.ClearJustInstantiatedObjects();
        }

        private void ProcessDeadObjects()
        {
            foreach (BaseModel model in BaseModel.DeadObjects)
            {
                // ...
            }
            BaseModel.ClearDeadObjects();
        }

        #endregion
    }
}