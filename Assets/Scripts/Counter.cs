using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Counter : MonoBehaviour
    {

        #region variables

        public bool Endless;
        public Text Time;

        private float _time;
        private string _seconds = "00";
        private string _minutes = "00";
        private bool _pause;

        #endregion

        private void OnEnable()
        {
            EventBroker.WinGame += EndCount;
            EventBroker.PauseGame += PauseUnPause;
            EventBroker.UnPauseGame += PauseUnPause;
        }

        private void OnDisable()
        {
            EventBroker.WinGame -= EndCount;
            EventBroker.PauseGame -= PauseUnPause;
            EventBroker.UnPauseGame -= PauseUnPause;
        }

        private void Update()
        {
            if (_pause)
                return;

            _time += UnityEngine.Time.deltaTime;

            if (Mathf.Floor(_time / 60) > 0 && Mathf.Floor(_time / 60) < 10)
                _minutes = Mathf.Floor(_time / 60).ToString("0");

            if (Mathf.Floor(_time / 60) > 10)
                _minutes = Mathf.Floor(_time / 60).ToString("00");

            _seconds = Mathf.Floor(_time % 60).ToString("00");

            if (Endless)
            {
                Time.text = _minutes + ":" + _seconds;
            }
        }

        private void PauseUnPause()
        {
            _pause = !_pause;
        }


        private void EndCount(Vector2 endPos)
        {
            EventBroker.CallStopTimeCounter(_minutes + ":" + _seconds);
        }
    }
}
