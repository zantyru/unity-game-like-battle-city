namespace Game
{
    public sealed class BulletModel : BeingModel
    {
        #region Methods
        
        private void OnCollisionEnter()
        {
            Destroy(this.gameObject);
        }

        #endregion
    }
}