using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class EndPortal : MonoBehaviour
    {

        [UsedImplicitly]
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Asteroid")
            {
                DelayDestroy(col.gameObject);
            }
            else if (col.gameObject.tag == "Player")
            {
                EventBroker.CallWinGame(transform.position);
            }
        }

        private void DelayDestroy(GameObject obj)
        {
            Destroy(obj);
        }
    }
}
