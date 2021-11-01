using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] private int _id;

        #region setter/getter
        public void SetId(int id)
        {
            _id = id;
        }

        public int GetId()
        {
            return _id;
        }
        #endregion

        [UsedImplicitly]
        private void OnEnable()
        {

        }

        [UsedImplicitly]
        private void OnDisable()
        {

        }
    }
}
