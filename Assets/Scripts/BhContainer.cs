using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class BhContainer : MonoBehaviour
    {
        #region variables

        public GameObject BlackHole;

        private const int MaxBlackHoles = 3;
        private Dictionary<int, GameObject> _blackHoles;
        #endregion

        [UsedImplicitly]
        private void OnEnable()
        {
            _blackHoles = new Dictionary<int, GameObject>();
            EventBroker.MakeBlackHole += MakeBlackHole;
            EventBroker.DestroyBlackHole += DestroyBlackHole;
        }

        [UsedImplicitly]
        private void OnDisable()
        {
            EventBroker.MakeBlackHole -= MakeBlackHole;
            EventBroker.DestroyBlackHole -= DestroyBlackHole;
        }

        [UsedImplicitly]
        private void Start()
        {
            InitialBhMake();
        }

        private void InitialBhMake()
        {
            for (var i = 0; i < MaxBlackHoles; i++)
            {
                var bh = Instantiate(BlackHole, transform);
                bh.GetComponent<BlackHole>().SetId(i);
                _blackHoles.Add(i, bh);
            }
        }

        private void MakeBlackHole(int id, Vector2 position)
        {
            _blackHoles[id].transform.position = position;
            _blackHoles[id].SetActive(true);
        }

        private void DestroyBlackHole(int id)
        {
            _blackHoles[id].SetActive(false);
        }
    }
}
