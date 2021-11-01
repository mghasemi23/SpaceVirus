using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts;
/// <summary>
/// This class controls player movement
/// </summary>
public class Controller : MonoBehaviour
{
    [Header("GameObject/Component References")]
    [Tooltip("The animator controller used to animate the player.")]
    public RuntimeAnimatorController animator = null;
    [Tooltip("The Rigidbody2D component to use in \"Astroids Mode\".")]
    public Rigidbody2D myRigidbody = null;

    [Header("Movement Variables")]
    [Tooltip("The speed at which the player will move.")]
    public float moveSpeed = 10.0f;
    [Tooltip("The speed at which the player rotates in asteroids movement mode")]
    public float rotationSpeed = 60f;
    [Tooltip("players dodge distance")]
    public float dodgeDistance = 30;
    [Tooltip("players dodge cooldown time")]
    public float dodgeCoolDown = 1;
    public GameObject shadow;
    public Collider2D Area;
    private AudioSource dodgeSound;

    [Header("Tactical")]
    public Shield Shield;
    public float shieldTime;
    public float shieldCoolDown;

    [Header("Ultimate")]
    public Ultimate UltimatePrefab;
    public int ultimateCoolDown;
    public int ultimateTime;


    //The InputManager to read input from
    private InputManager inputManager;

    /// <summary>
    /// Enum which stores different aiming modes
    /// </summary>
    public enum AimModes { AimTowardsMouse, AimForwards };

    [Header("Aim")]
    [Tooltip("The aim mode in use by this player:\n" +
        "Aim Towards Mouse: Player rotates to face the mouse\n" +
        "Aim Forwards: Player aims the direction they face (doesn't face towards the mouse)")]
    public AimModes aimMode = AimModes.AimTowardsMouse;

    /// <summary>
    /// Enum to handle different movement modes for the player
    /// </summary>
    public enum MovementModes { MoveHorizontally, MoveVertically, FreeRoam, Astroids };

    [Tooltip("The movmeent mode used by this controller:\n" +
        "Move Horizontally: Player can only move left/right\n" +
        "Move Vertically: Player can only move up/down\n" +
        "FreeRoam: Player can move in any direction and can aim\n" +
        "Astroids: Player moves forward/back in the direction they are facing and rotates with horizontal input")]
    public MovementModes movementMode = MovementModes.FreeRoam;


    // Whether the player can aim with the mouse or not
    private bool canAimWithMouse
    {
        get
        {
            return aimMode == AimModes.AimTowardsMouse;
        }
    }

    // Whether the player's X coordinate is locked (Also assign in rigidbody)
    private bool lockXCoordinate
    {
        get
        {
            return movementMode == MovementModes.MoveVertically;
        }
    }
    // Whether the player's Y coordinate is locked (Also assign in rigidbody)
    public bool lockYCoordinate
    {
        get
        {
            return movementMode == MovementModes.MoveHorizontally;
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once when the script starts before Update
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void Start()
    {
        dodgeSound = GetComponent<AudioSource>();

        SetupInput();

        if (PlayerPrefs.GetInt("HasTactical") == 1)
            StartCoroutine(ResetTactical());


        if (PlayerPrefs.GetInt("HasUltimate") == 1)
            StartCoroutine(ResetUltimate());

    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once per frame
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    void Update()
    {
        // Collect input and move the player accordingly
        HandleInput();
        // Sends information to an animator component if one is assigned
        SignalAnimator();

    }

    /// <summary>
    /// Description:
    /// Sets up the input manager if it is not already set up. Throws an error if none exists
    /// Inputs:
    /// None
    /// Returns:
    /// void
    /// </summary>
    private void SetupInput()
    {
        if (inputManager == null)
        {
            inputManager = InputManager.instance;
        }
        if (inputManager == null)
        {
            Debug.LogWarning("There is no player input manager in the scene, there needs to be one for the Controller to work");
        }
    }

    /// <summary>
    /// Description:
    /// Handles input and moves the player accordingly
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void HandleInput()
    {
        // Find the position that the player should look at
        Vector2 lookPosition = GetLookPosition();
        // Get movement input from the inputManager
        Vector3 movementVector = new Vector3(inputManager.horizontalMoveAxis, inputManager.verticalMoveAxis, 0);
        // Move the player
        MovePlayer(movementVector);
        LookAtPoint(lookPosition);


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Dodge();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Tactical();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ultimate();
        }

    }

    /// <summary>
    /// Description: 
    /// Handles signals to animator components
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void SignalAnimator()
    {
        // Handle Animation
        if (animator != null)
        {

        }
    }

    /// <summary>
    /// Description:
    /// Updates the position the player is looking at
    /// Inputs: 
    /// none
    /// Returns: 
    /// Vector2
    /// </summary>
    /// <returns>Vector2: The position the player should look at</returns>
    public Vector2 GetLookPosition()
    {
        Vector2 result = transform.up;
        if (aimMode != AimModes.AimForwards)
        {
            result = new Vector2(inputManager.horizontalLookAxis, inputManager.verticalLookAxis);
        }
        else
        {
            result = transform.up;
        }
        return result;
    }

    /// <summary>
    /// Description:
    /// Moves the player
    /// Inputs: 
    /// Vector3 movement
    /// Returns: 
    /// void (no return)
    /// </summary>
    /// <param name="movement">The direction to move the player</param>
    private void MovePlayer(Vector3 movement)
    {
        // Set the player's posiiton accordingly

        // Move according to astroids setting
        if (movementMode == MovementModes.Astroids)
        {

            // If no rigidbody is assigned, assign one
            if (myRigidbody == null)
            {
                myRigidbody = GetComponent<Rigidbody2D>();
            }

            // Move the player using physics
            Vector2 force = transform.up * movement.y * Time.deltaTime * moveSpeed;
            Debug.Log(force);
            myRigidbody.AddForce(force);

            // Rotate the player around the z axis
            Vector3 newRotationEulars = transform.rotation.eulerAngles;
            float zAxisRotation = transform.rotation.eulerAngles.z;
            float newZAxisRotation = zAxisRotation - rotationSpeed * movement.x * Time.deltaTime;
            newRotationEulars = new Vector3(newRotationEulars.x, newRotationEulars.y, newZAxisRotation);
            transform.rotation = Quaternion.Euler(newRotationEulars);

        }
        // Move according to the other settings
        else
        {
            // Don't move in the x if the settings stop us from doing so
            if (lockXCoordinate)
            {
                movement.x = 0;
            }
            // Don't move in the y if the settings stop us from doing so
            if (lockYCoordinate)
            {
                movement.y = 0;
            }
            // Move the player's transform
            transform.position = transform.position + (movement * Time.deltaTime * moveSpeed);
        }
    }

    /// <summary>
    /// Description: 
    /// Rotates the player to look at a point
    /// Inputs: 
    /// Vector3 point
    /// Returns: 
    /// void (no return)
    /// </summary>
    /// <param name="point">The screen space position to look at</param>
    private void LookAtPoint(Vector3 point)
    {
        if (Time.timeScale > 0)
        {
            // Rotate the player to look at the mouse.
            Vector2 lookDirection = Camera.main.ScreenToWorldPoint(point) - transform.position;

            if (canAimWithMouse)
            {
                transform.up = lookDirection;
            }
            else
            {
                if (myRigidbody != null)
                {
                    myRigidbody.freezeRotation = true;
                }
            }
        }
    }


    bool dodged = false;
    private void Dodge()
    {
        if (!dodged)
        {
            dodged = true;
            StartCoroutine(ResetDodge());

            Vector2 destination = Camera.main.ScreenToWorldPoint(GetLookPosition()) - transform.position;
            destination = destination.normalized * dodgeDistance;
            if (Area.OverlapPoint(transform.position + (Vector3)destination))
            {
                ShowShadow(transform.position, destination);
                transform.position = transform.position + (Vector3)destination;
                dodgeSound.Play();
            }
        }
    }

    private void ShowShadow(Vector3 start, Vector3 distance)
    {
        var _count = 5;
        distance /= _count;
        var _rotation = transform.rotation;
        var _pos = transform.position;
        for (int i = 0; i < _count; i++)
        {
            Instantiate(shadow, _pos, _rotation);
            _pos += distance;
        }
    }

    private IEnumerator ResetDodge()
    {
        yield return new WaitForSeconds(dodgeCoolDown);
        dodged = false;
    }

    bool tactical = true;
    private void Tactical()
    {
        if (!tactical)
        {
            tactical = true;
            StartCoroutine(ResetTactical());

            Vector2 destination = Camera.main.ScreenToWorldPoint(GetLookPosition()) - transform.position;
            destination = destination.normalized * 3;
            var shield = Instantiate(Shield, transform.position + (Vector3)destination, transform.rotation);
            shield.Time(shieldTime);
        }
    }


    private IEnumerator ResetTactical()
    {
        Broker.ResetTactical(shieldCoolDown);
        yield return new WaitForSeconds(shieldCoolDown);
        tactical = false;
    }


    bool ultimate = true;
    int ultNumber = 0;
    private void Ultimate()
    {
        if (!ultimate)
        {
            ultimate = true;
            Debug.Log("ult");
            ultNumber = ultimateTime * 2;
            var _ult = Instantiate(UltimatePrefab, transform.position, transform.rotation);
            _ult.RotationDirection(0);
            StartCoroutine(SpawnUltimate(1));
        }
    }

    private IEnumerator SpawnUltimate(int wavenumber)
    {
        yield return new WaitForSeconds(0.5f);
        if (wavenumber <= ultNumber)
        {
            var _ult = Instantiate(UltimatePrefab, transform.position, transform.rotation);
            _ult.RotationDirection(wavenumber);
            StartCoroutine(SpawnUltimate(wavenumber + 1));
        }
        else
        {
            StartCoroutine(ResetUltimate());
        }
    }

    private IEnumerator ResetUltimate()
    {
        Broker.ResetUltimate(ultimateCoolDown);
        yield return new WaitForSeconds(ultimateCoolDown);
        ultimate = false;
    }
}
