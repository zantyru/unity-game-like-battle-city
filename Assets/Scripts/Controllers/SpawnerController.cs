using System;


namespace Game
{
    public sealed class SpawnerController : BaseController, IFixedExecutable
    {
        #region Properties
        
        public override Type AppropriateModelType => typeof(SpawnerModel);

        #endregion


        #region IFixedExecutable

        public void FixedExecute(float deltaTime)
        {
            foreach (SpawnerModel spawner in base.Models)            
            {
                ActionData actionData = new ActionData()
                {
                    HeadDirection = spawner.SpawnDirection,
                    MotionDirection = spawner.SpawnDirection,
                    IsShooting = false
                };
                spawner.Do(actionData, deltaTime);
            }
        }
            
        #endregion


        #region Methods
        
        protected override void _Execute(float deltaTime) { }

        #endregion
    }
}