namespace Game
{
    public sealed class DirectionModel : BaseModel
    {
        #region Fields
        
        private Directions _headDirection = Directions.NONE;
        private Directions _motionDirection = Directions.NONE;

        #endregion


        #region Properties

        public Directions HeadDirection
        {
            get => _headDirection;
            set
            {
                PreviousHeadDirection = _headDirection;
                _headDirection = value;
            }
        }

        public Directions PreviousHeadDirection { get; private set; } = Directions.NONE;

        public Directions MotionDirection
        {
            get => _motionDirection;
            set
            {
                PreviousMotionDirection = _motionDirection;
                _motionDirection = value;
            }
        }

        public Directions PreviousMotionDirection { get; private set; } = Directions.NONE;

        #endregion
    }
}