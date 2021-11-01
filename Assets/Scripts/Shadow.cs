using UnityEngine;

public class Shadow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alpha(gameObject, 0, 1).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
