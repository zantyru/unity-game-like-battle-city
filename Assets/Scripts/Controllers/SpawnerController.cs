using System;


namespace Game
{
    public sealed class SpawnerController : BaseController
    {
        #region Properties
        
        public override Type AppropriateModelType => typeof(SpawnerModel);

        #endregion


        #region Methods
        
        protected override void FixedProcessModel(BaseModel model, float deltaTime)
        {
            base.FixedProcessModel(model, deltaTime);
            SpawnerModel spawner = model as SpawnerModel;
            ActionData actionData = new ActionData()
            {
                HeadDirection = spawner.SpawnDirection,
                MotionDirection = spawner.SpawnDirection,
                IsShooting = false
            };
            spawner.Do(actionData, deltaTime);
        }

        #endregion
    }
}