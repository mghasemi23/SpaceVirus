using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckProze : MonoBehaviour
{
    public string prize;
    public int condition;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(prize) <= condition)
        {
            GetComponent<Text>().enabled = true;
        }
    }
}
