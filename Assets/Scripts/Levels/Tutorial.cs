using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class Tutorial : MonoBehaviour
    {

        #region variables

        public GameObject Blur;
        public GameObject TutorialText;
        public GameObject OkButton;

        #endregion


        private void Start()
        {
            if (PlayerPrefs.GetInt("firstTime") == 0)
            {
                Blur.SetActive(true);
                TutorialText.SetActive(true);
                OkButton.SetActive(true);
            }
        }


        public void OkBtn()
        {
            PlayerPrefs.SetInt("firstTime", 1);
            Blur.SetActive(false);
            TutorialText.SetActive(false);
            OkButton.SetActive(false);
        }
    }
}
