using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{

    public class PlanetMovement : MonoBehaviour
    {
        #region variables

        public Vector2 Limit;
        public float MoveTime;
        public Canvas Canvas;

        private Vector3 _startPosition;
        #endregion

        [UsedImplicitly]
        private void Start()
        {
            _startPosition = transform.position;
            Limit = Limit * Canvas.scaleFactor;
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            var t = 0f;
            var start = transform.position;
            var end = RandomPosition();
            while (t < 1)
            {
                t += Time.deltaTime / MoveTime;
                transform.position = Vector3.Lerp(start, end, t);
                yield return null;
            }

            StartCoroutine(Move());
        }

        private Vector3 RandomPosition()
        {
            var x = Limit.x * Random.Range(-1f, 1f);
            var y = Limit.y * Random.Range(-1f, 1f);
            return new Vector3(_startPosition.x + x, _startPosition.y + y);
        }
    }
}
