using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public Text Bullet;
    public GameObject Tactical;
    public GameObject Ultimate;


    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("HasTactical") == 0)
        {
            Tactical.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HasUltimate") == 0)
        {
            Ultimate.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Bullet") == 0)
        {
            Bullet.text = "Bullet Type";
        }
        else if (PlayerPrefs.GetInt("Bullet") == 1)
        {
            Bullet.text = "Bullet Type:\npress F to change to Fire Bullets";
        }
    }
}
