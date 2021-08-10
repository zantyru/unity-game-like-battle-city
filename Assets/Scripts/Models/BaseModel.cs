using UnityEngine;


namespace Game
{
    public abstract class BaseModel : MonoBehaviour
    {
        #region Properties
        
        public Vector3 Position
        {
            get => this.transform.position;
            set => this.transform.position = value;
        }
        
        #endregion
    }
}