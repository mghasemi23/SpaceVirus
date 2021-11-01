using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class MovingObstacle : MonoBehaviour
    {
        #region variables

        public Transform Obstacle;
        public Transform Pos1, Pos2;
        public float Speed = 0.2f;

        #endregion


        private void Update()
        {
            Obstacle.position = Vector3.Lerp(Pos1.position, Pos2.position, Mathf.PingPong(Time.time * Speed, 1.0f));
        }
    }
}
