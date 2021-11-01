using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broker : MonoBehaviour
{
    public static event Action ChangeToDefault;
    public static event Action ChangeToFire;
    public static event Action ChangeToIce;

    public static void ChangedToDefault()
    {
        ChangeToDefault?.Invoke();
    }

    public static void ChangedToFire()
    {
        ChangeToFire?.Invoke();
    }

    public static void ChangedToIce()
    {
        ChangeToIce?.Invoke();
    }


    public static event Action<float> UltimateCooldown;
    public static event Action<float> TacticalCooldown;

    public static void ResetUltimate(float cooldown)
    {
        UltimateCooldown?.Invoke(cooldown);
    }

    public static void ResetTactical(float cooldown)
    {
        TacticalCooldown?.Invoke(cooldown);
    }

    public static event Action<float> boosHP;

    public static void BossDamage(float damage)
    {
        boosHP?.Invoke(damage);
    }
}
