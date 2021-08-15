namespace Game
{
    public sealed class BulletActionDataProvider : IActionDataProvider
    {
        #region Fields

        private readonly Directions _motionDirection = Directions.NONE;

        #endregion


        #region ClassLifeCycle

        public BulletActionDataProvider(Directions initialDirection)
        {
            _motionDirection = initialDirection;
        }
        
        #endregion


        #region IActionDataProvider
        
        public ActionData GetActionData()
        {
            return new ActionData()
            {
                HeadDirection = _motionDirection,
                MotionDirection = _motionDirection,
                IsShooting = false  // Idea! Bullet shoots bullets! Bullet hell.
            };
        }

        #endregion
    }
}