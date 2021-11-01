using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MainMenuSceneManager : MonoBehaviour
    {
        #region variables

        public Camera MainCamera;
        public Animator TitleAnimator;
        //public AudioMixer MasterAudioMixer;
        //public GameObject Music;
        [Header("Main:")]
        public RectTransform Stars;
        public RectTransform[] BackgroundPlanets;
        public RectTransform Blur;
        public RectTransform Title;
        public RectTransform BackButton;
        public RectTransform StartButton;
        public RectTransform SettingButton;
        public RectTransform ExitButton;
        public RectTransform InfoButton;
        public RectTransform[] Virus;
        [Header("Levels:")]
        public RectTransform LevelsParent;
        public RectTransform[] Levels;
        [Header("Info:")]
        public RectTransform InfoText;
        [Header("Setting:")]
        public RectTransform SettingParent;
        public RectTransform MusicText;
        public Slider MusicSlider;
        public RectTransform SFXText;
        public Slider SFXSlider;
        public RectTransform Abilities;
        public Toggle abilitTogg;
        public RectTransform[] MusicSliderItems;
        public RectTransform[] SFXSliderItems;
        public RectTransform[] Toggle;
        [Header("Mixer:")]
        public AudioMixer MusicAM;
        public AudioMixer SFXAM;

        private AudioSource _clickAudioSource;
        private int state = 0;
        private const float FadeTime = 1f;

        #endregion


        [UsedImplicitly]
        private void Start()
        {
            Application.targetFrameRate = 60;
            Time.timeScale = 1;
            //DontDestroyOnLoad(Music);
            _clickAudioSource = GetComponent<AudioSource>();

            ChangeVolume();
            ChangeAbilities();
            StartMainMenu();
        }

        public void StartGravity()
        {
            TitleAnimator.enabled = true;
        }

        public void StartBtn()
        {
            state = 1;

            LeanTween.alpha(StartButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(InfoButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(ExitButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(SettingButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setOnComplete(o =>
            {
                BackButton.gameObject.SetActive(true);
                LevelsParent.gameObject.SetActive(true);

                StartButton.gameObject.SetActive(false);
                InfoButton.gameObject.SetActive(false);
                SettingButton.gameObject.SetActive(false);
                ExitButton.gameObject.SetActive(false);
            });
            LeanTween.alpha(BackButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            for (var i = 0; i < Levels.Length; i++)
                LeanTween.alpha(Levels[i], 1, FadeTime).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);

        }

        public void InfoBtn()
        {
            state = 2;

            LeanTween.alpha(StartButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(InfoButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(ExitButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(SettingButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setOnComplete((() =>
              {
                  InfoText.gameObject.SetActive(true);
                  BackButton.gameObject.SetActive(true);

                  StartButton.gameObject.SetActive(false);
                  InfoButton.gameObject.SetActive(false);
                  SettingButton.gameObject.SetActive(false);
                  ExitButton.gameObject.SetActive(false);
              }));
            LeanTween.textAlpha(InfoText, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(BackButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
        }

        public void SettingBtn()
        {
            state = 3;

            LeanTween.alpha(StartButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(InfoButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(ExitButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(SettingButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setOnComplete((() =>
              {
                  SettingParent.gameObject.SetActive(true);
                  BackButton.gameObject.SetActive(true);

                  StartButton.gameObject.SetActive(false);
                  InfoButton.gameObject.SetActive(false);
                  SettingButton.gameObject.SetActive(false);
                  ExitButton.gameObject.SetActive(false);
              }));
            LeanTween.alpha(BackButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.textAlpha(MusicText, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.textAlpha(SFXText, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.textAlpha(Abilities, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            for (var i = MusicSliderItems.Length - 1; i >= 0; i--)
                LeanTween.alpha(MusicSliderItems[i], 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            for (var i = SFXSliderItems.Length - 1; i >= 0; i--)
                LeanTween.alpha(SFXSliderItems[i], 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            for (var i = Toggle.Length - 1; i >= 0; i--)
                LeanTween.alpha(Toggle[i], 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
        }


        public void ExitBtn()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }


        public void GetSliderValue(float v)
        {
            PlayerPrefs.SetFloat("Volume", v);
            PlayerPrefs.Save();
            ChangeVolume();
        }

        public void BackBtn()
        {
            //_clickAudioSource.Play();
            switch (state)
            {
                case 1:
                    BackFromLevelsToMenu();
                    return;
                case 2:
                    BackFromInfoToMenu();
                    return;
                case 3:
                    BackFromSettingToMenu();
                    return;
            }
        }

        public void StartLevel(int level)
        {
            //_clickAudioSource.Play();
            for (var i = 0; i < Levels.Length; i++)
                LeanTween.alpha(Levels[i], 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(BackButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.textAlpha(Title, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(Stars, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            foreach (var _planet in BackgroundPlanets)
                LeanTween.alpha(_planet, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            foreach (var _virus in Virus)
                LeanTween.alpha(_virus, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            StartCoroutine(LoadSceneWithDelay(level));
        }

        private void StartMainMenu()
        {
            foreach (var _planet in BackgroundPlanets)
                LeanTween.alpha(_planet, 1, FadeTime).setEase(LeanTweenType.easeInOutSine);

            LeanTween.alpha(Stars, 1, FadeTime).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
            {
                LeanTween.alpha(Blur, 1, FadeTime).setEase(LeanTweenType.easeInOutSine);
                LeanTween.alpha(StartButton, 1, FadeTime).setEase(LeanTweenType.easeInOutSine);
                LeanTween.alpha(InfoButton, 1, FadeTime).setEase(LeanTweenType.easeInOutSine);
                LeanTween.alpha(SettingButton, 1, FadeTime).setEase(LeanTweenType.easeInOutSine);
                LeanTween.alpha(ExitButton, 1, FadeTime).setEase(LeanTweenType.easeInOutSine);
                LeanTween.scale(Title, Vector2.one * 1.2f, FadeTime).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
                {
                    LeanTween.scale(Title, Vector2.one, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
                });
            });
        }

        private void BackFromInfoToMenu()
        {
            state = 0;

            LeanTween.textAlpha(InfoText, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(BackButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
              {
                  StartButton.gameObject.SetActive(true);
                  InfoButton.gameObject.SetActive(true);
                  SettingButton.gameObject.SetActive(true);
                  ExitButton.gameObject.SetActive(true);

                  InfoText.gameObject.SetActive(false);
                  BackButton.gameObject.SetActive(false);
              });
            LeanTween.alpha(InfoButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(StartButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(SettingButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(ExitButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
        }

        private void BackFromSettingToMenu()
        {
            state = 0;

            LeanTween.alpha(BackButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            for (var i = MusicSliderItems.Length - 1; i >= 0; i--)
                LeanTween.alpha(MusicSliderItems[i], 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            for (var i = SFXSliderItems.Length - 1; i >= 0; i--)
                LeanTween.alpha(SFXSliderItems[i], 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            for (var i = Toggle.Length - 1; i >= 0; i--)
                LeanTween.alpha(Toggle[i], 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.textAlpha(MusicText, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.textAlpha(Abilities, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);
            LeanTween.textAlpha(SFXText, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setOnComplete((() =>
              {
                  StartButton.gameObject.SetActive(true);
                  InfoButton.gameObject.SetActive(true);
                  SettingButton.gameObject.SetActive(true);
                  ExitButton.gameObject.SetActive(true);

                  SettingParent.gameObject.SetActive(false);
                  BackButton.gameObject.SetActive(false);
              }));
            LeanTween.alpha(StartButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(InfoButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(SettingButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(ExitButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
        }

        private void BackFromLevelsToMenu()
        {
            state = 0;

            for (var i = 0; i < Levels.Length; i++)
                LeanTween.alpha(Levels[i], 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine);

            LeanTween.alpha(BackButton, 0, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setOnComplete(o =>
              {
                  StartButton.gameObject.SetActive(true);
                  InfoButton.gameObject.SetActive(true);
                  SettingButton.gameObject.SetActive(true);
                  ExitButton.gameObject.SetActive(true);

                  LevelsParent.gameObject.SetActive(false);
                  BackButton.gameObject.SetActive(false);
              });

            LeanTween.alpha(StartButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(InfoButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(SettingButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);
            LeanTween.alpha(ExitButton, 1, FadeTime / 2).setEase(LeanTweenType.easeInOutSine).setDelay(FadeTime / 2);

        }

        private void ChangeVolume()
        {
            MusicAM.SetFloat("Volume", PlayerPrefs.GetFloat("music"));
            SFXAM.SetFloat("Volume", PlayerPrefs.GetFloat("sound"));
            MusicSlider.value = PlayerPrefs.GetFloat("music");
            SFXSlider.value = PlayerPrefs.GetFloat("sound");
        }

        private void Quit()
        {
            Application.Quit();
        }

        private IEnumerator LoadSceneWithDelay(int sceneNumber)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneNumber);
        }

        private void ChangeAbilities()
        {
            if (PlayerPrefs.GetInt("Unlocked") == 1)
            {
                abilitTogg.isOn = true;
            }
        }

        public void UnlockAbilities()
        {
            PlayerPrefs.SetInt("Bullet", 2);
            PlayerPrefs.SetInt("HasTactical", 1);
            PlayerPrefs.SetInt("HasUltimate", 1);
            PlayerPrefs.SetInt("Unlocked", 1);
        }

        public void ChangeMusicVolume()
        {
            PlayerPrefs.SetFloat("music", MusicSlider.value);
            MusicAM.SetFloat("Volume", PlayerPrefs.GetFloat("music"));
        }


        public void ChangeSoundVolume()
        {
            PlayerPrefs.SetFloat("sound", SFXSlider.value);
            SFXAM.SetFloat("Volume", PlayerPrefs.GetFloat("sound"));
        }
    }
}