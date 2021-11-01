using UnityEngine;

namespace Assets.Scripts.Endless
{
    public class ObstacleMover : MonoBehaviour
    {

        #region variables

        public float Speed = 0.1f;

        private float _speed;

        #endregion

        private void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Speed, transform.position.z);
        }


        public void Stop()
        {
            _speed = Speed;
            Speed = 0;
        }

        public void Begin()
        {
            Speed = _speed;
        }
    }
}
