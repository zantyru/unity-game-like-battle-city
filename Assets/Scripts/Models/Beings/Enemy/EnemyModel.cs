namespace Game
{
    public sealed class EnemyModel : BeingModel
    {
        #region Methods

        protected override void Awake()
        {
            base.Awake();
            base.ActionDataProvider = new EnemyActionDataProvider();
        }

        #endregion
    }
}