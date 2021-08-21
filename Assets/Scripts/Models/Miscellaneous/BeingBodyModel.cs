using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class BeingBodyModel : BaseModel
    {
        #region Fields
        
        [SerializeField] private Sprite _leftSprite = default;
        [SerializeField] private Sprite _rightSprite = default;
        [SerializeField] private Sprite _upSprite = default;
        [SerializeField] private Sprite _downSprite = default;
        private readonly Dictionary<Directions, Sprite> _mapping = new Dictionary<Directions, Sprite>();
        private SpriteRenderer _spriteRenderer = default;

        #endregion


        #region Methods

        public void SetDirection(Directions direction)
        {
            // Note: I know about https://forum.unity.com/threads/optimizing-null-check-null-vs-bool-out-functions.482118/
            // It's not time to do micro-optimizations yet
            //if (_spriteRenderer != null)
            //{
                _spriteRenderer.sprite = _mapping[direction];
            //}
        }

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mapping[Directions.NONE] = _spriteRenderer.sprite;
            _mapping[Directions.LEFT] = _leftSprite;
            _mapping[Directions.RIGHT] = _rightSprite;
            _mapping[Directions.UP] = _upSprite;
            _mapping[Directions.DOWN] = _downSprite;
        }

        #endregion
    }
}