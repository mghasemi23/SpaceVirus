using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFollower : MonoBehaviour
{
    public GameObject Boss;
    public Image hpImage;
    private float hp = 2500;


    void Update()
    {
        if (Boss == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = Camera.main.WorldToScreenPoint(Boss.transform.position + Vector3.up * 10);
        }
    }

    private void OnEnable()
    {
        Broker.boosHP += ChangeHP;
    }

    private void OnDisable()
    {
        Broker.boosHP -= ChangeHP;
    }

    void ChangeHP(float damage)
    {
        hp -= damage;
        hpImage.fillAmount = hp / 2500;
    }
}
