using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.Endless
{
    public class SelfDestroyer : MonoBehaviour
    {

        #region variables

        public float LiveTime = 20;

        #endregion

        private void Start()
        {
            Destroy(gameObject, LiveTime);
        }
    }
}
