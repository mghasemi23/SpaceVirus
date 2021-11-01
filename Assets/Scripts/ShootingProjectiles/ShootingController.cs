using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class which controlls player aiming and shooting
/// </summary>
public class ShootingController : MonoBehaviour
{
    public ProjectileType projectileType;
    public bool isEnemy = true;
    [Header("GameObject/Component References")]
    [Tooltip("Default Projectile.")]
    public GameObject defaultProjectilePrefab = null;

    [Tooltip("Fire Projectile.")]
    public GameObject fireProjectilePrefab = null;

    [Tooltip("Ice Projectile.")]
    public GameObject iceProjectilePrefab = null;

    [Tooltip("The transform in the heirarchy which holds projectiles if any")]
    public Transform projectileHolder = null;

    [Header("Input")]
    [Tooltip("Whether this shooting controller is controled by the player")]
    public bool isPlayerControlled = false;

    [Header("Default Projectile Settings")]
    [Tooltip("The minimum time between projectiles being fired.")]
    public float defaultFireRate = 0.05f;

    [Tooltip("The maximum diference between the direction the" +
        " shooting controller is facing and the direction projectiles are launched.")]
    public float defaultProjectileSpread = 1.0f;


    [Header("Fire Projectile Settings")]
    [Tooltip("The minimum time between projectiles being fired.")]
    public float fireFireRate = 0.05f;

    [Tooltip("The maximum diference between the direction the" +
        " shooting controller is facing and the direction projectiles are launched.")]
    public float fireProjectileSpread = 1.0f;


    [Header("Ice Projectile Settings")]
    [Tooltip("The minimum time between projectiles being fired.")]
    public float iceFireRate = 0.05f;

    [Tooltip("The maximum diference between the direction the" +
        " shooting controller is facing and the direction projectiles are launched.")]
    public float iceProjectileSpread = 1.0f;

    // The last time this component was fired
    private float lastFired = Mathf.NegativeInfinity;

    [Header("Effects")]
    [Tooltip("The effect to create when this fires")]
    public GameObject fireEffect;

    //The input manager which manages player input
    private InputManager inputManager = null;

    public GunChanger GunChanger;

    /// <summary>
    /// Description:
    /// Standard unity function that runs every frame
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Update()
    {
        ProcessInput();
        if (isEnemy)
        {
            return;
        }
        if (PlayerPrefs.GetInt("Bullet") > 0 && Input.GetKeyDown(KeyCode.F))
        {
            ChangeProjectile(ProjectileType.Fire);
        }

        if (PlayerPrefs.GetInt("Bullet") > 1 && Input.GetKeyDown(KeyCode.C))
        {
            ChangeProjectile(ProjectileType.Ice);
        }
    }


    private void ChangeProjectile(ProjectileType type)
    {
        if (projectileType != type)
        {
            projectileType = type;

            if (type == ProjectileType.Fire)
            {
                GunChanger.ChangeToFire();
                Broker.ChangedToFire();
            }
            else
            {
                GunChanger.ChangeToIce();
                Broker.ChangedToIce();
            }
        }
        else
        {
            projectileType = ProjectileType.Default;
            GunChanger.ChnageToDefault();
            Broker.ChangedToDefault();
        }
    }

    /// <summary>
    /// Description:
    /// Standard unity function that runs when the script starts
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Start()
    {
        SetupInput();
    }

    /// <summary>
    /// Description:
    /// Attempts to set up input if this script is player controlled and input is not already correctly set up 
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    void SetupInput()
    {
        if (isPlayerControlled)
        {
            if (inputManager == null)
            {
                inputManager = InputManager.instance;
            }
            if (inputManager == null)
            {
                Debug.LogError("Player Shooting Controller can not find an InputManager in the scene, there needs to be one in the " +
                    "scene for it to run");
            }
        }
    }

    /// <summary>
    /// Description:
    /// Reads input from the input manager
    /// Inputs:
    /// None
    /// Returns:
    /// void (no return)
    /// </summary>
    void ProcessInput()
    {
        if (isPlayerControlled)
        {
            if (inputManager.firePressed || inputManager.fireHeld)
            {
                Fire();
            }
        }
    }

    /// <summary>
    /// Description:
    /// Fires a projectile if possible
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void Fire()
    {
        var _fireRate = 0f;
        switch (projectileType)
        {
            case ProjectileType.Default:
                _fireRate = defaultFireRate;
                break;
            case ProjectileType.Fire:
                _fireRate = fireFireRate;
                break;
            case ProjectileType.Ice:
                _fireRate = iceFireRate;
                break;
            default:
                break;
        }

        //If the cooldown is over fire a projectile
        if ((Time.timeSinceLevelLoad - lastFired) > _fireRate)
        {
            // Launches a projectile
            SpawnProjectile();

            if (fireEffect != null)
            {
                Instantiate(fireEffect, transform.position, transform.rotation, projectileHolder);
            }

            // Restart the cooldown
            lastFired = Time.timeSinceLevelLoad;
        }
    }

    /// <summary>
    /// Description:
    /// Spawns a projectile and sets it up
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void SpawnProjectile()
    {

        var _projectile = defaultProjectilePrefab;
        var _spread = defaultProjectileSpread;

        switch (projectileType)
        {
            case ProjectileType.Fire:
                _projectile = fireProjectilePrefab;
                _spread = fireProjectileSpread;
                break;
            case ProjectileType.Ice:
                _projectile = iceProjectilePrefab;
                _spread = iceProjectileSpread;
                break;
            default:
                break;
        }

        // Check that the prefab is valid
        if (_projectile != null)
        {
            // Create the projectile
            GameObject projectileGameObject = Instantiate(_projectile, transform.position, transform.rotation, null);

            // Account for spread
            Vector3 rotationEulerAngles = projectileGameObject.transform.rotation.eulerAngles;
            rotationEulerAngles.z += Random.Range(-_spread, _spread);
            projectileGameObject.transform.rotation = Quaternion.Euler(rotationEulerAngles);

            // Keep the heirarchy organized
            if (projectileHolder != null)
            {
                projectileGameObject.transform.SetParent(projectileHolder);
            }
        }
    }
}
public enum ProjectileType
{
    Default,
    Fire,
    Ice
}