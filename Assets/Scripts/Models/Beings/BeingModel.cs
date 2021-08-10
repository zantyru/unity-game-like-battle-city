using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BeingModel : BaseModel, IActeable
    {
        #region Fields
        
        [SerializeField, Range(0.0f, 1000.0f)] private float _speed = 10.0f; // units per second
        private Directions _headDirection = Directions.NONE;
        private Rigidbody2D _rigidbody = default;
        private LevelGridBehaviour _levelGridBehaviour = default;
        private BeingBodyModel _beingBodyModel = default;

        #endregion


        #region Properties

        public Directions HeadDirection
        {
            get => _headDirection;
            set
            {
                PreviousHeadDirection = _headDirection;
                _headDirection = value;
                if (_beingBodyModel)
                {
                    _beingBodyModel.SetDirection(_headDirection);
                }
            }
        }

        public Directions PreviousHeadDirection { get; private set; } = Directions.NONE;

        public Directions PreviousMotionDirection { get; private set; } = Directions.NONE;

        public IActionDataProvider ActionDataProvider { get; protected set; } = default;

        #endregion


        #region IActeable

        public void Do(ActionData actionData, float deltaTime)
        {
            Directions newHeadDirection = actionData.HeadDirection;
            if (newHeadDirection == Directions.NONE)
            {
                newHeadDirection = PreviousHeadDirection;
            }
            HeadDirection = newHeadDirection;
            Move(actionData.MotionDirection, deltaTime);
        }

        #endregion


        #region Methods

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        protected void Move(Directions motionDirection, float deltaTime)
        {
            if (motionDirection == Directions.NONE) return;

            Vector3 motion = motionDirection.GetVector3();
            Vector3 previousMotion = PreviousMotionDirection.GetVector3();
            bool isNotCollineary = !motion.IsXYCollineary(previousMotion);

            Vector2 position = _rigidbody.position;
            if (isNotCollineary)
            {
                if (motionDirection == Directions.UP || motionDirection == Directions.DOWN)
                {
                    // Snapping `x`
                    float gridCellSizeX = _levelGridBehaviour.CellSizeX;
                    float gridOriginX = _levelGridBehaviour.OriginX;
                    float diff = position.x - gridOriginX;
                    float steps = diff / gridCellSizeX;
                    float nearSnapStep = Mathf.Floor(steps + 0.5f);
                    position.x = gridOriginX + nearSnapStep * gridCellSizeX;
                }
                else if (motionDirection == Directions.LEFT || motionDirection == Directions.RIGHT)
                {
                    // Snapping `y`
                    float gridCellSizeY = _levelGridBehaviour.CellSizeY;
                    float gridOriginY = _levelGridBehaviour.OriginY;
                    float diff = position.y - gridOriginY;
                    float steps = diff / gridCellSizeY;
                    float nearSnapStep = Mathf.Floor(steps + 0.5f);
                    position.y = gridOriginY + nearSnapStep * gridCellSizeY;
                }
            }

            Vector2 resultPosition = position + motionDirection.GetVector2() * deltaTime * _speed;
            _rigidbody.MovePosition(resultPosition);
            PreviousMotionDirection = motionDirection;
        }

        private void Start()
        {
            _levelGridBehaviour = FindObjectsOfType<LevelGridBehaviour>()[0];
            _beingBodyModel = GetComponentInChildren<BeingBodyModel>();
        }

        #endregion
    }
}