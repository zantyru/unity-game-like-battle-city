using UnityEngine;


namespace Game
{
    public sealed class SpawnerModel : BaseModel, IActeable
    {
        #region Fields
        
        [SerializeField] private GameObject _prefab = default;
        [SerializeField] private Directions _direction = Directions.NONE;
        private DirectionModel _directionModel = default;

        #endregion


        #region Properties

        public Directions SpawnDirection
        {
            get {
                Directions spawnDirecton = Directions.NONE;
                // Note: I know about https://forum.unity.com/threads/optimizing-null-check-null-vs-bool-out-functions.482118/
                // It's not time to do micro-optimizations yet 
                if (_directionModel != null)
                {
                    spawnDirecton = _directionModel.HeadDirection;
                }
                return spawnDirecton;
            }
        }

        #endregion


        #region Methods

        public void Do(ActionData actionData, float deltaTime)
        {
            Directions headDirection = actionData.HeadDirection;
            if (headDirection == Directions.NONE)
            {
                headDirection = _direction;
            }

            GameObject instantiatedObject = Object.Instantiate<GameObject>(_prefab, base.Position, Quaternion.identity);
            if (instantiatedObject.TryGetComponent<DirectionModel>(out var directionModel))
            {
                directionModel.HeadDirection = headDirection;
                directionModel.MotionDirection = headDirection;
            }
        }

        #endregion
    }
}