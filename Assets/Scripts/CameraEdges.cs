using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class CameraEdges : MonoBehaviour
    {
        #region variables
        private Camera _mainCamera;
        private EdgeCollider2D _edgeCollider2D;
        #endregion

        [UsedImplicitly]
        private void Start()
        {
            _mainCamera = gameObject.GetComponent<Camera>();
            _edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();

            RebuildCollider();

        }

        private void RebuildCollider()
        {
            Vector2 bottomLeft = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector2 topRight = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
            var bottomRight = new Vector2(topRight.x, bottomLeft.y);
            var topLeft = new Vector2(bottomLeft.x, topRight.y);

            var newVector2 = _edgeCollider2D.points;

            newVector2[0] = bottomLeft;
            newVector2[1] = bottomRight;
            newVector2[2] = topRight;
            newVector2[3] = topLeft;
            newVector2[4] = bottomLeft;

            _edgeCollider2D.points = newVector2;
        }
    }
}
