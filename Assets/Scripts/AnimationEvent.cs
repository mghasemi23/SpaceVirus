using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationEvent : MonoBehaviour
    {
        #region variables

        private Animator _animator;

        #endregion

        [UsedImplicitly]
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void DisableAnimation()
        {
            _animator.enabled = false;
        }
    }
}
