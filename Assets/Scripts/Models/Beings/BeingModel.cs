using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(DirectionModel))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BeingModel : BaseModel, IActeable
    {
        #region Fields
        
        [SerializeField, Range(0.0f, 1000.0f)] private float _speed = 10.0f; // units per second
        protected DirectionModel _directionModel = default;
        private Rigidbody2D _rigidbody = default;
        private BeingBodyModel _beingBodyModel = default;
        private LevelGridBehaviour _levelGridBehaviour = default;

        #endregion


        #region Properties

        public IActionDataProvider ActionDataProvider { get; protected set; } = new NoneActionDataProvider();

        #endregion


        #region IActeable

        public void Do(ActionData actionData, float deltaTime)
        {
            Directions newHeadDirection = actionData.HeadDirection;
            if (newHeadDirection == Directions.NONE)
            {
                newHeadDirection = _directionModel.PreviousHeadDirection;
            }

            _directionModel.HeadDirection = newHeadDirection;
            _directionModel.MotionDirection = actionData.MotionDirection;
            
            _beingBodyModel.SetDirection(_directionModel.HeadDirection);
            Move(_directionModel.MotionDirection, deltaTime);
        }

        #endregion


        #region Methods

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
            _directionModel = GetComponent<DirectionModel>();
        }

        protected virtual void Start()
        {
            _levelGridBehaviour = FindObjectsOfType<LevelGridBehaviour>()[0];
            _beingBodyModel = GetComponentInChildren<BeingBodyModel>();
        }
        
        protected void Move(Directions motionDirection, float deltaTime)
        {
            if (motionDirection == Directions.NONE)
            {
                // Preventing spontaneous sliding after colliding other being
                if (_rigidbody.velocity.sqrMagnitude > 0.0f)
                {
                    _rigidbody.velocity = Vector2.zero;
                }
                return;
            }

            Vector2 position = _rigidbody.position;
            if (motionDirection == Directions.UP || motionDirection == Directions.DOWN)
            {
                position.x = SnapValue(position.x, _levelGridBehaviour.CellSizeX, _levelGridBehaviour.OriginX);
            }
            else if (motionDirection == Directions.LEFT || motionDirection == Directions.RIGHT)
            {
                position.y = SnapValue(position.y, _levelGridBehaviour.CellSizeY, _levelGridBehaviour.OriginY);
            }
            // Apply snapped position
            _rigidbody.position = position;

            Vector2 resultPosition = position + motionDirection.GetVector2() * deltaTime * _speed;
            _rigidbody.MovePosition(resultPosition);
        }

        private float SnapValue(float value, float gridStepSize, float gridOrigin)
        {
            float diff = value - gridOrigin;
            float steps = diff / gridStepSize;
            float nearSnapStep = Mathf.Floor(steps + 0.5f);
            return gridOrigin + nearSnapStep * gridStepSize;
        }

        #endregion
    }
}