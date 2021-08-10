namespace Game
{
    public sealed class PlayerModel : BeingModel
    {
        #region Methods

        protected override void Awake()
        {
            base.Awake();
            base.ActionDataProvider = new PlayerActionDataProvider(base.tag);
        }

        #endregion
    }
}