using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class InGameSceneManager : MonoBehaviour
    {
        #region variables

        [Header("Pause:")]
        public GameObject Blur;
        public RectTransform PauseButton;
        public RectTransform HomeButton;
        public RectTransform ResetButton;
        [Header("Win:")]
        public GameObject WinWindow;
        public RectTransform TimeText;
        public RectTransform WinNextButton;
        public RectTransform WinResetButton;
        public RectTransform WinHomeButton;
        public RectTransform[] Stars;
        [Header("Sprite:")]
        public Sprite FullStar;

        private readonly Vector2 _starScale = new Vector2(0.32f, 0.32f);
        private readonly Vector2 _fullStarScale = new Vector2(0.4f, 0.4f);
        private bool _paused;
        private int _starCollected;

        #endregion

        private void OnEnable()
        {
            EventBroker.CollectStar += CollectStar;
            EventBroker.WinGame += Win;
            EventBroker.StopTimeCounter += UpdateTimer;
        }

        private void OnDisable()
        {
            EventBroker.CollectStar -= CollectStar;
            EventBroker.WinGame -= Win;
            EventBroker.StopTimeCounter -= UpdateTimer;
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
                LeanTween.scale(ResetButton, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInOutSine).setOnComplete(o =>
                {
                    HomeButton.gameObject.SetActive(false);
                    ResetButton.gameObject.SetActive(false);
                });
            }
            else
            {
                EventBroker.CallPauseGame();
                Blur.SetActive(true);
                HomeButton.gameObject.SetActive(true);
                ResetButton.gameObject.SetActive(true);
                LeanTween.moveY(HomeButton, -180, 0.3f).setEase(LeanTweenType.easeInOutSine);
                LeanTween.moveY(ResetButton, -280, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f);
                LeanTween.scale(HomeButton, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine);
                LeanTween.scale(ResetButton, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(0.1f).setOnComplete(
                    o =>
                    {
                        Time.timeScale = 0;
                    });
            }
            _paused = !_paused;
        }

        public void HomeBtn()
        {
            SceneManager.LoadScene(0);
        }

        public void ResetBtn()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextBtn()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void CollectStar()
        {
            _starCollected++;
        }

        private void Win(Vector2 endPos)
        {
            Invoke("WinAnimation", 1f);
        }

        private void WinAnimation()
        {
            PauseButton.gameObject.SetActive(false);
            HomeButton.gameObject.SetActive(false);
            ResetButton.gameObject.SetActive(false);
            WinWindow.SetActive(true);
            Blur.SetActive(true);
            LeanTween.scale(WinNextButton, Vector3.one, 0.2f).setEase(LeanTweenType.easeInOutSine);
            LeanTween.scale(WinResetButton, Vector3.one, 0.2f).setEase(LeanTweenType.easeInOutSine);
            LeanTween.scale(WinHomeButton, Vector3.one, 0.2f).setEase(LeanTweenType.easeInOutSine);
            LeanTween.scale(TimeText, Vector3.one, 0.2f).setEase(LeanTweenType.easeInOutSine);
            if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 21)
            {
                WinNextButton.GetComponent<Button>().interactable = false;
            }
            var delay = 0.2f;
            var i = 0;
            for (; i < 3; i++)
            {
                if (i < _starCollected)
                {
                    Stars[i].GetComponent<Image>().sprite = FullStar;
                    var obj = Stars[i];
                    LeanTween.scale(obj, _fullStarScale, 0.3f).setEase(LeanTweenType.easeInOutSine).setDelay(delay).setOnComplete(o =>
                    {
                        LeanTween.scale(obj, _starScale, 0.1f).setEase(LeanTweenType.easeInOutSine);
                    });
                }
                else
                {
                    LeanTween.scale(Stars[i], _starScale, 0.2f).setDelay(delay);
                }
                delay += 0.4f;
            }
        }

        private void UpdateTimer(string endTime)
        {
            TimeText.GetComponent<Text>().text = "Time: " + endTime;
        }
    }
}
