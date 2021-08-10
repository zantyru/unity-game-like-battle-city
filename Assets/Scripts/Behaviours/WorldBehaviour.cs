using UnityEngine;


namespace Game
{
    public sealed class WorldBehaviour : MonoBehaviour
    {
        #region Fields

        private WorldController _worldController = default;

        #endregion


        #region Methods

        private void Start()
        {
            _worldController = new WorldController();
        }

        private void Update()
        {
            _worldController.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _worldController.FixedExecute(Time.fixedDeltaTime);
        }
        
        #endregion
    }
}