using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChanger : MonoBehaviour
{
    public Sprite Defaultgun;
    public Sprite FireGun;
    public Sprite IceGun;

    public ParticleSystem Fire;
    public ParticleSystem Ice;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChnageToDefault()
    {
        spriteRenderer.sprite = Defaultgun;
        Fire.Stop();
        Ice.Stop();
    }

    public void ChangeToFire()
    {
        spriteRenderer.sprite = FireGun;
        Ice.Stop();
        Fire.Play();
    }

    public void ChangeToIce()
    {
        spriteRenderer.sprite = IceGun;
        Fire.Stop();
        Ice.Play();
    }
}
