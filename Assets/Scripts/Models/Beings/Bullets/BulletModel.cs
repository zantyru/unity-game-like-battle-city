namespace Game
{
    public sealed class BulletModel : BeingModel
    {
        #region Methods

        protected override void Start()
        {
            base.Start();
            base.ActionDataProvider = new BulletActionDataProvider(base._directionModel.MotionDirection);
        }
        
        // private void OnCollisionEnter()
        // {
        //     Destroy(this.gameObject);
        // }

        #endregion
    }
}