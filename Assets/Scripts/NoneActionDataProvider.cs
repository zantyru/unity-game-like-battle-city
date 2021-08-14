using UnityEngine;


namespace Game
{
    public sealed class NoneActionDataProvider : IActionDataProvider
    {
        #region IActionDataProvider

        public ActionData GetActionData()
        {
            return new ActionData()
            {
                MotionDirection = Directions.NONE,
                HeadDirection = Directions.NONE,
                IsShooting = false
            };
        }

        #endregion
    }
}