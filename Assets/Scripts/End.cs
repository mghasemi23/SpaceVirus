using UnityEngine;

public class End : MonoBehaviour
{
    public GameManager gameManager;
    public int level = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.LevelCleared();
            GivePrize();
        }
    }

    private void GivePrize()
    {
        switch (level)
        {
            case 1:
                PlayerPrefs.SetInt("Bullet", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("Bullet", 2);
                break;
            case 3:
                PlayerPrefs.SetInt("HasTactical", 1);
                break;
            case 4:
                PlayerPrefs.SetInt("HasUltimate", 1);
                break;
            default:
                break;
        }
    }
}
