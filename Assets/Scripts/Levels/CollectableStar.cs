using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class CollectableStar : MonoBehaviour
    {
        #region variables

        private bool _collected;
        private AudioSource _collect;

        #endregion

        [UsedImplicitly]
        private void Start()
        {
            _collect = GetComponent<AudioSource>();
        }

        [UsedImplicitly]
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_collected && col.gameObject.tag == "Player")
            {
                _collected = true;
                EventBroker.CallCollectStar();
                DelayDestroy();
            }
        }

        private void DelayDestroy()
        {
            _collect.Play();
            LeanTween.scale(gameObject, Vector3.zero, 0.1f).setEase(LeanTweenType.easeInOutSine).setOnComplete(o =>
            {
                Destroy(gameObject);
            });
        }
    }
}
