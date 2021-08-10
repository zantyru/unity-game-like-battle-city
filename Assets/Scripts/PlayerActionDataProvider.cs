using UnityEngine;


namespace Game
{
    public sealed class PlayerActionDataProvider : IActionDataProvider
    {
        #region Fields

        private const string _H_AXIS_POSTFIX = "Horizontal";
        private const string _V_AXIS_POSTFIX = "Vertical";
        private const string _SHOOT_POSTFIX = "Shoot";
        private const string _FIXED_SHOOT_POSTFIX = "FixedShoot";
        private readonly string _hAxis = string.Empty;
        private readonly string _vAxis = string.Empty;
        private readonly string _shoot = string.Empty;
        private readonly string _fixedShoot = string.Empty;
        private Directions _fixedHeadDirection = Directions.NONE;

        #endregion


        #region ClassLifeCycle

        public PlayerActionDataProvider(string playerPrefix)
        {
            _hAxis = $"{playerPrefix}{_H_AXIS_POSTFIX}";
            _vAxis = $"{playerPrefix}{_V_AXIS_POSTFIX}";
            _shoot = $"{playerPrefix}{_SHOOT_POSTFIX}";
            _fixedShoot = $"{playerPrefix}{_FIXED_SHOOT_POSTFIX}";
        }

        #endregion


        #region IActionDataProvider

        public ActionData GetActionData()
        {
            float h = Input.GetAxis(_hAxis);
            float v = Input.GetAxis(_vAxis);
            bool isHPressed = Input.GetButton(_hAxis);
            bool isVPressed = Input.GetButton(_vAxis);
            Directions motionDirection = Directions.NONE;

            if (h < 0.0f && isHPressed)
            {
                motionDirection = Directions.LEFT;
            }
            else if (h > 0.0f && isHPressed)
            {
                motionDirection = Directions.RIGHT;
            }

            if (v < 0.0f && isVPressed)
            {
                motionDirection = Directions.DOWN;
            }
            else if (v > 0.0f && isVPressed)
            {
                motionDirection = Directions.UP;
            }

            bool isFixedShooting = Input.GetButton(_fixedShoot);
            bool isShooting = Input.GetButton(_shoot) || isFixedShooting;
            Directions headDirection = motionDirection;

            if (isFixedShooting)
            {
                headDirection = _fixedHeadDirection;
            }
            else
            {
                _fixedHeadDirection = headDirection;
            }

            return new ActionData()
            {
                MotionDirection = motionDirection,
                HeadDirection = headDirection,
                IsShooting = isShooting
            };
        }

        #endregion
    }
}