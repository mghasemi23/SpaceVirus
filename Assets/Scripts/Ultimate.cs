using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class Ultimate : MonoBehaviour
    {

        #region variables

        public float RotationSpeed = 70f;

        #endregion

        [UsedImplicitly]
        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, RotationSpeed) * Time.deltaTime);
        }

        public void RotationDirection(int direction)
        {
            if (direction % 2 == 1)
            {
                RotationSpeed *= -1;
            }
        }
    }
}
