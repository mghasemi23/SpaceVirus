using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{

    public float bulletTime = 0.1f;
    public Canvas Canvas;

    [Header("HP")]
    public Image Armor;
    public Image Health;

    [Header("Bullets")]
    public RectTransform NormalBullet;
    public RectTransform FireBullet;
    public RectTransform IceBullet;

    [Header("Tactical")]
    public Image Shield;
    public Text ShieldCoolDown;
    public Color TacticalLoading;
    public Color TacticalLoaded;
    private float shieldCD;
    private float shieldCurrentCD = 0;
    private bool startTactical = false;

    [Header("Ultimate")]
    public Image Ultimate;
    public Text UltimateCoolDown;
    public Color UltimateLoading;
    public Color UltimatLoaded;
    private float ultimateCD;
    private float ultimateCurrentCD = 0;
    private bool startUltimate = false;


    private Vector3 firstBullet = new Vector3(99, 152, 0);
    private Vector3 secondBullet = new Vector3(99, 132, 0);
    private Vector3 thirdBullet = new Vector3(183, 90, 0);

    private void OnEnable()
    {
        Broker.ChangeToDefault += GotoDefault;
        Broker.ChangeToFire += GotoFire;
        Broker.ChangeToIce += GotoIce;

        Broker.UltimateCooldown += StartUltimateCooldown;
        Broker.TacticalCooldown += StartTacticalCooldown;

        if (PlayerPrefs.GetInt("HasTactical") == 0)
        {
            Shield.transform.parent.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HasUltimate") == 0)
        {
            Ultimate.transform.parent.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Bullet") == 0)
        {
            FireBullet.gameObject.SetActive(false);
            IceBullet.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Bullet") == 1)
        {
            IceBullet.gameObject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        Broker.ChangeToDefault -= GotoDefault;
        Broker.ChangeToFire -= GotoFire;
        Broker.ChangeToIce -= GotoIce;

        Broker.UltimateCooldown -= StartUltimateCooldown;
        Broker.TacticalCooldown -= StartTacticalCooldown;
    }

    private int state = 0; //default=0 , fire= 1, Ice =2

    private void GotoDefault()
    {
        LeanTween.scale(IceBullet, Vector3.one * 0.3f, bulletTime).setEase(LeanTweenType.easeInOutSine);
        LeanTween.move(IceBullet.gameObject, firstBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);

        LeanTween.scale(FireBullet, Vector3.one * 0.3f, bulletTime).setEase(LeanTweenType.easeInOutSine);
        LeanTween.move(FireBullet.gameObject, secondBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);

        LeanTween.scale(NormalBullet, Vector3.one, bulletTime).setEase(LeanTweenType.easeInOutSine);
        LeanTween.move(NormalBullet.gameObject, thirdBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);
    }


    private void GotoFire()
    {

        if (PlayerPrefs.GetInt("Bullet") == 1)
        {

            LeanTween.scale(NormalBullet, Vector3.one * 0.3f, bulletTime).setEase(LeanTweenType.easeInOutSine);
            LeanTween.move(NormalBullet.gameObject, secondBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);
        }
        else
        {
            LeanTween.scale(NormalBullet, Vector3.one * 0.3f, bulletTime).setEase(LeanTweenType.easeInOutSine);
            LeanTween.move(NormalBullet.gameObject, firstBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);

            LeanTween.scale(IceBullet, Vector3.one * 0.3f, bulletTime).setEase(LeanTweenType.easeInOutSine);
            LeanTween.move(IceBullet.gameObject, secondBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);
        }
        LeanTween.scale(FireBullet, Vector3.one, 0.1f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.move(FireBullet.gameObject, thirdBullet * Canvas.scaleFactor, 0.1f).setEase(LeanTweenType.easeInOutSine);
    }

    private void GotoIce()
    {
        LeanTween.scale(FireBullet, Vector3.one * 0.3f, bulletTime).setEase(LeanTweenType.easeInOutSine);
        LeanTween.move(FireBullet.gameObject, firstBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);

        LeanTween.scale(NormalBullet, Vector3.one * 0.3f, bulletTime).setEase(LeanTweenType.easeInOutSine);
        LeanTween.move(NormalBullet.gameObject, secondBullet * Canvas.scaleFactor, bulletTime).setEase(LeanTweenType.easeInOutSine);

        LeanTween.scale(IceBullet, Vector3.one, 0.1f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.move(IceBullet.gameObject, thirdBullet * Canvas.scaleFactor, 0.1f).setEase(LeanTweenType.easeInOutSine);
    }


    private void StartUltimateCooldown(float cooldown)
    {
        UltimateCoolDown.text = cooldown.ToString();
        UltimateCoolDown.gameObject.SetActive(true);

        ultimateCD = cooldown;
        ultimateCurrentCD = 0;
        startUltimate = true;
    }


    private void StartTacticalCooldown(float cooldown)
    {

        ShieldCoolDown.text = cooldown.ToString();
        ShieldCoolDown.gameObject.SetActive(true);
        Shield.color = TacticalLoading;

        shieldCD = cooldown;
        shieldCurrentCD = 0;
        startTactical = true;
    }

    private void Update()
    {




        if (startTactical)
        {
            if (shieldCurrentCD < shieldCD)
            {
                shieldCurrentCD += Time.deltaTime;
                ShieldCoolDown.text = ((int)(shieldCD - shieldCurrentCD)).ToString();
                Shield.fillAmount = shieldCurrentCD / shieldCD;
            }
            else
            {
                ShieldCoolDown.gameObject.SetActive(false);
                Shield.color = TacticalLoaded;
                startTactical = false;
            }
        }


        if (startUltimate)
        {
            if (ultimateCurrentCD < ultimateCD)
            {
                ultimateCurrentCD += Time.deltaTime;
                UltimateCoolDown.text = ((int)(ultimateCD - ultimateCurrentCD)).ToString();
                Ultimate.fillAmount = ultimateCurrentCD / ultimateCD;
            }
            else
            {
                UltimateCoolDown.gameObject.SetActive(false);
                Ultimate.color = TacticalLoaded;
                startUltimate = false;
            }
        }
    }
}
