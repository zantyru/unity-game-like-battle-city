using UnityEngine;


namespace Game
{
    public sealed class SpawnerModel : BaseModel
    {
        #region Fields
        
        [SerializeField] private GameObject _prefab = default;
        [SerializeField] private Directions _direction = Directions.NONE;

        #endregion


        #region Methods

        public void Spawn()
        {
            GameObject instantiatedObject = Object.Instantiate<GameObject>(_prefab, base.Position, Quaternion.identity);
            if (instantiatedObject.TryGetComponent<DirectionModel>(out var directionModel))
            {
                directionModel.HeadDirection = _direction;
                directionModel.MotionDirection = _direction;
            }
        }

        #endregion
    }
}