using UnityEngine;


namespace Game
{
    public sealed class EnemyActionDataProvider : IActionDataProvider
    {
        #region Fields

        private const float _CHANGE_DIRECTION_DELAY = 10.0f; // Units
        private const float _TIMER_STEP = 0.5f; // Units
        private float _timer = default;
        private Directions _motionDirection = Directions.NONE;

        #endregion


        #region IActionDataProvider

        public ActionData GetActionData()
        {
            if (_timer > 0.0f)
            {
                _timer -= _TIMER_STEP;
            }
            else
            {
                _timer = _CHANGE_DIRECTION_DELAY;
                switch (Random.Range(0, 4))
                {
                    case 0:
                        _motionDirection = Directions.LEFT;
                        break;
                    case 1:
                        _motionDirection = Directions.UP;
                        break;
                    case 2:
                        _motionDirection = Directions.RIGHT;
                        break;
                    case 3:
                        _motionDirection = Directions.DOWN;
                        break;
                };
            }

            return new ActionData()
            {
                MotionDirection = _motionDirection,
                HeadDirection = _motionDirection
            };
        }

        #endregion
    }
}