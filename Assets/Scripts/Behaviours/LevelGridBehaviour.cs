using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(Grid))]
    public sealed class LevelGridBehaviour : MonoBehaviour
    {
        #region Fields
        
        private Grid _grid = default;

        #endregion


        #region Properties
        
        public float CellSizeX => _grid.cellSize.x;
        public float CellSizeY => _grid.cellSize.y;
        public float OriginX => _grid.transform.position.x;
        public float OriginY => _grid.transform.position.y;

        #endregion


        #region Methods

        private void Awake() => _grid = GetComponent<Grid>();

        #endregion
    }
}