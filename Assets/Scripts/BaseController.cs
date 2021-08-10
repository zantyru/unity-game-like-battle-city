namespace Game
{
    public abstract class BaseController : IExecutable
    {
        #region Properties

        public bool IsEnabled { get; set; } = false;

        #endregion


        #region IExecutable

        public void Execute(float deltaTime)
        {
            if (IsEnabled) _Execute(deltaTime);
        }

        #endregion


        #region Methods

        protected abstract void _Execute(float deltaTime);

        #endregion
    }
}