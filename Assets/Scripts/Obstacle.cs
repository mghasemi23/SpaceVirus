using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Obstacle : MonoBehaviour
    {
        [UsedImplicitly]
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    EventBroker.CallLoseGame();
                }
                else
                {
                    EventBroker.CallDestroySpaceShip();
                }
            }
        }
    }
}