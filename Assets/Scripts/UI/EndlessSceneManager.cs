using System;
using Assets.Scripts.Endless;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EndlessSceneManager : MonoBehaviour
    {
        #region variables

        [Header("Pause:")]
        public Transform Obstacles;
        public GameObject Blur;
        public RectTransform PauseButton;
        public RectTransform HomeButton;
        public RectTransform ResetButton;
        public RectTransform BestTimePause;
        public RectTransform TimeTextPause;
        public RectTransform TimePause;
        [Header("Lose:")]
        public GameObject LoseWindow;
        public RectTransform TimeText;
        public RectTransform BestTimeText;
        public RectTransform LoseResetButton;
        public RectTransform LoseHomeButton;

        private bool _paused;

        #endregion

        private void OnEnable()
        {
            EventBroker.LoseGame += LoseGame;
        }

        private void OnDisable()
        {
            EventBroker.LoseGame -= LoseGame;
        }

        public void PauseBtn()
        {
            if (_paused)
            {
                Time.timeScale = 1;
                EventBroker.CallUnPauseGame();
                Blur.SetActive(false);
                LeanTween.moveY(HomeButton, -100, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f);
                LeanTween.moveY(ResetButton, -100, 0.3f).setEase(LeanTweenType.easeInOutSine);
                LeanTween.scale(HomeButton, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f);
                LeanTween.scale(BestTimePause, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f);
                LeanTween.scale(ResetButton, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInOutSine).setOnComplete(o =>
                {
                    HomeButton.gameObject.SetActive(false);
                    ResetButton.gameObject.SetActive(false);
                    BestTimePause.gameObject.SetActive(false);
                });
                for (var i = 0; i < Obstacles.childCount; i++)
                {
                    Obstacles.GetChild(i).GetComponent<ObstacleMover>().Begin();
                }
            }
            else
            {
                EventBroker.CallPauseGame();
                Blur.SetActive(true);
                HomeButton.gameObject.SetActive(true);
                ResetButton.gameObject.SetActive(true);
                BestTimePause.gameObject.SetActive(true);
                if (PlayerPrefs.GetString("Best") != "")
                {
                    BestTimePause.GetComponent<Text>().text = "Best: " + PlayerPrefs.GetString("Best");
                }
                LeanTween.moveY(HomeButton, -180, 0.3f).setEase(LeanTweenType.easeInOutSine);
                LeanTween.moveY(ResetButton, -280, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f);
                LeanTween.scale(HomeButton, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine);
                LeanTween.scale(ResetButton, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f);
                LeanTween.scale(BestTimePause, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f).setOnComplete(
                    o =>
                    {
                        Time.timeScale = 0;
                    });

                for (var i = 0; i < Obstacles.childCount; i++)
                {
                    Obstacles.GetChild(i).GetComponent<ObstacleMover>().Stop();
                }
            }
            _paused = !_paused;
        }

        public void HomeBtn()
        {
            SceneManager.LoadScene(0);
        }

        public void ResetBtn()
        {
            SceneManager.LoadScene(1);
        }

        private void LoseGame()
        {
            var time = TimePause.GetComponent<Text>().text;
            var best = PlayerPrefs.GetString("Best");

            PauseButton.gameObject.SetActive(false);
            ResetButton.gameObject.SetActive(false);
            HomeButton.gameObject.SetActive(false);
            TimePause.gameObject.SetActive(false);
            TimeTextPause.gameObject.SetActive(false);
            BestTimePause.gameObject.SetActive(false);

            Blur.gameObject.SetActive(true);
            LoseWindow.SetActive(true);
            TimeText.gameObject.SetActive(true);
            BestTimeText.gameObject.SetActive(true);
            LoseHomeButton.gameObject.SetActive(true);
            LoseResetButton.gameObject.SetActive(true);

            var curTimeInt = Convert.ToInt32(time.Replace(":", ""));
            var bestTimeInt = 0;

            if (best != "")
            {
                bestTimeInt = Convert.ToInt32(best.Replace(":", ""));
            }

            if (bestTimeInt < curTimeInt)
            {
                PlayerPrefs.SetString("Best", time);
            }

            TimeText.GetComponent<Text>().text = "Time: " + time;
            BestTimeText.GetComponent<Text>().text = "Best Time: " + PlayerPrefs.GetString("Best");

            LeanTween.scale(LoseWindow, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine);
            LeanTween.scale(TimeText, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine);
            LeanTween.scale(BestTimeText, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine);
            LeanTween.scale(LoseResetButton, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine);
            LeanTween.scale(LoseHomeButton, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine);
        }
    }
}
