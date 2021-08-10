namespace Game
{
    public sealed class WorldController : BaseController
    {
        #region Fields

        private readonly BaseController[] _controllers;
        private readonly IFixedExecutable[] _fixedExecutableControllers;
        
        #endregion


        #region ClassLifeCycle

        public WorldController()
        {
            base.IsEnabled = true;

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

            foreach (BaseController controller in _controllers)
            {
                controller.IsEnabled = true;
                if (controller is IInitializable initializable)
                {
                    initializable.Initialize();
                }
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
            foreach (IExecutable controller in _controllers)
            {
                controller.Execute(deltaTime);
            }
        }

        #endregion
    }
}