using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class PortalRotator : MonoBehaviour
    {

        #region variables

        public float RotationSpeed = 70f;
        public AnimationCurve SpeedCurve;

        private float _currentSpeed;

        #endregion

        [UsedImplicitly]
        private void Update()
        {
            var mappedTime = (transform.rotation.eulerAngles.z % 360) / 360;
            _currentSpeed = RotationSpeed * SpeedCurve.Evaluate(mappedTime);
            transform.Rotate(new Vector3(0, 0, _currentSpeed) * Time.deltaTime);
        }
    }
}
