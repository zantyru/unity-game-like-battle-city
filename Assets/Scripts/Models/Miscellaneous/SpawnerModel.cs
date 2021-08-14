using UnityEngine;


namespace Game
{
    public sealed class SpawnerModel : BaseModel
    {
        #region Fields
        
        [SerializeField] private GameObject _prefab = default;
        [SerializeField] private Directions _direction = Directions.NONE;

        #endregion
    }
}