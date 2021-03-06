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
            var spawnerController = new SpawnerController();

            _controllers = new BaseController[]
            {
                beingsController,
                spawnerController,
                // Place controller instances here
            };

            _fixedExecutableControllers = new IFixedExecutable[]
            {
                beingsController,
                spawnerController,
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

        new public void FixedExecute(float deltaTime)
        {
            foreach (IFixedExecutable controller in _fixedExecutableControllers) 
            {
                controller.FixedExecute(deltaTime);
            }
        }

        new public void Execute(float deltaTime)
        {
            ProcessJustInstantiatedObjects();
            ProcessDeadObjects(); // Influence to `FixedUpdate`
            foreach (BaseController controller in _controllers)
            {
                controller.Execute(deltaTime);
            }
        }

        private void ProcessJustInstantiatedObjects()
        {
            foreach (BaseModel model in BaseModel.JustInstantiatedObjects)
            {
                Type modelType = model.GetType();
                foreach (Type appropriateModelType in _mapping.Keys)
                {
                    if (modelType.IsSubclassOf(appropriateModelType) || modelType == appropriateModelType)
                    {
                        List<BaseController> controllersForModel = _mapping[appropriateModelType];
                        foreach (BaseController controller in controllersForModel)
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
                Type modelType = model.GetType();
                foreach (Type appropriateModelType in _mapping.Keys)
                {
                    if (modelType.IsSubclassOf(appropriateModelType) || modelType == appropriateModelType)
                    {
                        List<BaseController> controllersForModel = _mapping[appropriateModelType];
                        foreach (BaseController controller in controllersForModel)
                        {
                            controller.DetachModel(model);
                        }
                    }
                }
            }
            BaseModel.ClearDeadObjects();
        }

        #endregion
    }
}