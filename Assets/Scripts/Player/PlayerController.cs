using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool playerInvertedControls;
    // this is the whirlwind effect when jumping - changes made my Jimmy
    public ParticleSystem whirlwind;
    [Header("Camera")]
    private GameObject _mainCamera;
    // the target for cinemachine camera that it will follow
    [SerializeField]public GameObject cinemachineCameraTarget;
    [SerializeField]private float _cinemachineTargetYaw;
    [SerializeField]private float _cinemachineTargetPitch;
    // clamps for camera
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    [Header("Player Movement")]
    [SerializeField] private CharacterController controller;
    [Tooltip("Speed of movement")]
    public float moveSpeed;
    [Tooltip("Acceleration and deceleration")]
    public float speedChangeRate = 10.0f;
    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    [SerializeField] private float _verticalVelocity;
    [SerializeField] private float distToGround;

    private float jumpVelocity;
    [SerializeField] private float gravity;
    public float _terminalVelocity = 20f;
    public bool isGrounded;
    private float timeInAir;
    public float JumpTimeout = 0.50f;
    private float _jumpTimeoutDelta;

    private float horizontalPressTimeCounter;
    private float verticalPressTimeCounter;

    private int jumpDelay;

    private Vector3 movementInputDirection;
    // this is for projectile use. When player is not inputting, movement goes to 0 but we want to prevent that
    private Vector3 lastMovementInputDirection;

    Animator animator;

    public int pineconeCount;
    public Text PineconeText;

    public Image powerIcon;
    public Sprite nothingSprite;
    public Sprite airSprite;
    public Sprite waterSprite;
    public Sprite lightSprite;

    public Text PowerText;
    private TutorialTextUI tutText;

    [Header("Elemental Magic")]
    [SerializeField] private ElementState currentElement;
    public enum ElementState
    {
        NONE,
        LIGHT,
        WATER,
        AIR
    }
    [SerializeField] private GameObject waterProjectile;
    [SerializeField] private GameObject lightProjectile;
    [SerializeField] private GameObject coneProjectile;
    [SerializeField] private float shootingCooldown;
    private float _shootingCooldownTimer;
    private Vector3 _projectileHeading;
    private Vector2 look;
    public float mouseSensitivity = 3.0f;
    private bool jump;

    void Awake() 
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpVelocity = 3;
        playerInvertedControls = GameStarter.invertedControls;
        currentElement = ElementState.NONE;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        timeInAir = 0;
        animator = GetComponent<Animator>();
        jumpDelay = 0;
        pineconeCount = 0;
        _cinemachineTargetYaw = cinemachineCameraTarget.transform.rotation.eulerAngles.y;
        if (powerIcon == null) powerIcon = GameObject.Find("PowerImage").GetComponent<Image>();
        if (PineconeText == null) PineconeText = GameObject.Find("PineconesText").GetComponent<Text>();
        powerIcon.sprite = nothingSprite;
        //PineconeText.text = "Pinecones: " + pineconeCount;
        //PowerText.text = "Power: None";
        int tempText = (pineconeCount + LevelSwitcher.globalPineCount);
        PineconeText.text = "" + tempText;
        tutText = GameObject.Find("Pop-up Text").GetComponent<TutorialTextUI>();


    }

    // mostly just for camera stuff
    void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation() 
    {
        // if there is an input, 0.01f is just so small mouse movements do not trigger
        if (look.sqrMagnitude >= 0.01f)
        {
            _cinemachineTargetYaw += look.x;
            if (playerInvertedControls)
            {
                _cinemachineTargetPitch += look.y;
            }
            else
            {
                _cinemachineTargetPitch -= look.y;
            }
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch,
            _cinemachineTargetYaw, 0.0f);

        GameObject target = GameObject.Find("PlayerFollowCamera");
        _projectileHeading = (target.transform.position - gameObject.transform.position);
        _projectileHeading = new Vector3(-_projectileHeading.x, 0.0f, -_projectileHeading.z);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    public void ActivatePower(string power)
    {
        if (power == "Light")
        {
            jumpVelocity = 3;
            Debug.Log("ACTIVE POWER: Light");
            FindObjectOfType<AudioManager>().Play("SwitchToLight");
            currentElement = ElementState.LIGHT;
            gameObject.layer = 6;
            powerIcon.sprite = lightSprite;
            //PowerText.text = "Power: Light";
        }
        else if (power == "Air")
        {
            jumpVelocity = 12;
            Debug.Log("ACTIVE POWER: Air");
            FindObjectOfType<AudioManager>().Play("SwitchToAir");
            currentElement = ElementState.AIR;
            gameObject.layer = 7;
            powerIcon.sprite = airSprite;
            //PowerText.text = "Power: Air";
        }
        else if (power == "WaterElement")
        {
            jumpVelocity = 3;
            Debug.Log("ACTIVE POWER: Water");
            FindObjectOfType<AudioManager>().Play("SwitchToWater");
            currentElement = ElementState.WATER;
            gameObject.layer = 8;
            powerIcon.sprite = waterSprite;
            //PowerText.text = "Power: Water";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Change layer based on Element
        switch (currentElement)
        {
            case ElementState.LIGHT:
                gameObject.layer = 6;
                animator.SetBool("isShooting", false);
                //animator.SetBool("crashEnemy", false);

                if (gameObject.transform.parent != null)
                {
                    gameObject.transform.parent.gameObject.layer = 6;
                    Transform[] ts = gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform t in ts)
                    {
                        t.gameObject.layer = 6;
                    }
                }
                else
                {
                    //Debug.Log("No parent layer was changed");
                }
                powerIcon.sprite = lightSprite;
                break;
            case ElementState.AIR:
                gameObject.layer = 7;
                animator.SetBool("isShooting", false);
                //animator.SetBool("crashEnemy", false);

                if (gameObject.transform.parent != null)
                {
                    gameObject.transform.parent.gameObject.layer = 7;
                    Transform[] ts = gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform t in ts)
                    {
                        t.gameObject.layer = 7;
                    }
                }
                else
                {
                    //Debug.Log("No parent layer was changed");
                }
                //Debug.Log(powerIcon);
                //Debug.Log(airSprite);
                powerIcon.sprite = airSprite;
                break;
            case ElementState.WATER:
                gameObject.layer = 8;
                animator.SetBool("isShooting", false);
               // animator.SetBool("crashEnemy", false);

                if (gameObject.transform.parent != null)
                {
                    gameObject.transform.parent.gameObject.layer = 8;
                    Transform[] ts = gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform t in ts)
                    {
                        t.gameObject.layer = 8;
                    }
                }
                else
                {
                    //Debug.Log("No parent layer was changed");
                }
                //powerIcon.sprite = waterSprite;
                break;
        }
        JumpAndGravity();
        IsGroundedRaycast();
        HandleMovement();
        if (_shootingCooldownTimer > 0f) {
            _shootingCooldownTimer -= Time.deltaTime;
        }
    }



    private void HandleMovement()
    {
        float targetSpeed = moveSpeed;
        if (movementInputDirection == Vector3.zero) targetSpeed = 0.0f;

        // players current horizontal speed
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed,
                Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(movementInputDirection.x, 0.0f, movementInputDirection.z).normalized;

        if (movementInputDirection != Vector3.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        // ANIMATE - moving animation

    }

    private void JumpAndGravity()
        {
            if (isGrounded)
            {
            Debug.Log("grounded");
                // reset the fall timeout timer

                // update animator if using character
                // if (_hasAnimator)
                // {
                //     _animator.SetBool(_animIDJump, false);
                //     _animator.SetBool(_animIDFreeFall, false);
                // }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -5f;
                    animator.SetBool("isJumping", false);
                }

            // Jump
            if (jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(jumpVelocity * -2f * gravity);

                    // update animator if using character
                    // if (_hasAnimator)
                    // {
                    //     _animator.SetBool(_animIDJump, true);
                    // }

                    FindObjectOfType<AudioManager>().Play("Jump");
                    if (jumpVelocity > 5) triggerWhirlwind();
                    // ANIMATE - start jump here!
                    animator.SetBool("isJumping", true);
                    controller.height = 2;
                    jump = false;
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                    controller.height = 3.7f;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;
                // update animator if using character
                // if (_hasAnimator)
                // {
                //     _animator.SetBool(_animIDFreeFall, true);
                // }

                // if we are not grounded, do not jump
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            _verticalVelocity += gravity * Time.deltaTime;
        }

    // takes in from hardware input, passes to variable movementInputDirection handled in HandleMovement()
    // !!! input.xy are the input horizontal values, but for the rest of code xz are horizontal, converting it here!!!
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movementInputDirection.x = input.x;
        movementInputDirection.z = input.y;
        
        animator.SetFloat("Velocity Z", Mathf.Abs(movementInputDirection.z));
        animator.SetFloat("Velocity X", movementInputDirection.x);
    }

    void OnLook(InputValue value) 
    {
        Vector2 lookVal = value.Get<Vector2>();
        look = lookVal / mouseSensitivity;
    }

    // handles button press for elemental magic
    void OnFire(InputValue value)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "FinalLevel" && LevelSwitcher.globalPineCount > 0)
        {
            if (coneProjectile == null)
            {
                Debug.LogWarning("No projectile attached, magic will not fire");
                return;
            }
            if (_shootingCooldownTimer > 0f)
            {
                return;
            }
            GameObject coneProj = Instantiate(coneProjectile, new Vector3(0, 0, 0), Quaternion.identity);
            coneProj.transform.position = (gameObject.transform.position + _projectileHeading * 0.2f);
            pProjectileController lightppc = coneProj.GetComponent<pProjectileController>();
            lightppc.SetDirection(_projectileHeading / 3);
            _shootingCooldownTimer = shootingCooldown;
            FindObjectOfType<AudioManager>().Play("ShootLight");
            LevelSwitcher.globalPineCount -= 1;
            tutText.CloseTextBox();
        }
            switch (currentElement)
        {
            case ElementState.AIR:
                jump = true;
                // check grounded
                if (isGrounded)
                {
                    jumpDelay = 1;
                    animator.SetBool("isJumping", true);
                }
                break;
            case ElementState.LIGHT:
                if (lightProjectile == null)
                {
                    Debug.LogWarning("No projectile attached, magic will not fire");
                    return;
                }
                if (_shootingCooldownTimer > 0f)
                {
                    return;
                }
                GameObject lightProj = Instantiate(lightProjectile, new Vector3(0, 0, 0), Quaternion.identity);
                lightProj.transform.position = (gameObject.transform.position +  _projectileHeading*0.2f);
                pProjectileController lightppc = lightProj.GetComponent<pProjectileController>();
                lightppc.SetDirection(_projectileHeading/3);
                _shootingCooldownTimer = shootingCooldown;
                FindObjectOfType<AudioManager>().Play("ShootLight");
                break;
            case ElementState.WATER:
                if (waterProjectile == null)
                {
                    Debug.LogWarning("No projectile attached, magic will not fire");
                    return;
                }
                if (_shootingCooldownTimer > 0f)
                {
                    return;
                }
                GameObject newProj = Instantiate(waterProjectile, new Vector3(0, 0, 0), Quaternion.identity);
                newProj.transform.position = (gameObject.transform.position + _projectileHeading * 0.2f);
                pProjectileController waterppc = newProj.GetComponent<pProjectileController>();
                waterppc.SetDirection(_projectileHeading / 3);
                _shootingCooldownTimer = shootingCooldown;
                FindObjectOfType<AudioManager>().Play("ShootWater");
                break;
        }
    }

    private void IsGroundedRaycast()
    {
        /*        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                        transform.position.z);
                isGrounded = Physics.CheckSphere(spherePosition, distToGround);*/
        isGrounded = Physics.Raycast(gameObject.transform.position, -Vector3.up, distToGround + 0.1f);
    }

    // this is the function to play the whirlwind effect - changes made by jimmy
    void triggerWhirlwind()
    {
        if (whirlwind == null)
        {
            Debug.LogWarning("Whirlwind object is not attached to player!");
            return;
        }
        whirlwind.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger enter");
        Scene currentScene = SceneManager.GetActiveScene();
        if (other.gameObject.CompareTag("Pinecone") && currentScene.name != "FinalLevel") 
        {
            //Debug.Log("PINECONE PINE CONE");
            other.gameObject.SetActive(false);
            pineconeCount = pineconeCount + 1;
            FindObjectOfType<AudioManager>().Play("CollectAcorn");
        }
        if (other.gameObject.CompareTag("FallingDeath"))
        {
            Debug.Log("FALLING DAETH");
            GameManager.DieByFall();
        }
    }

    void Update() {
        int tempText = (pineconeCount + LevelSwitcher.globalPineCount);
        PineconeText.text = "" + tempText;
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Debug.Log("hi");
            tutText.Toggle();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            // check grounded
            if (isGrounded)
            {
                jumpDelay = 1;
                animator.SetBool("isJumping", true);
            }
            
        }
            /*switch (currentElement)
        {
            case ElementState.AIR:
                // check grounded
                if (isGrounded)
                {
                    //rb.AddForce(new Vector3(0, jumpVelocity, 0), ForceMode.VelocityChange);
                    jumpDelay = 1;
                    animator.SetBool("isJumping", true);
                }
                // apply jump force
                break;
            case ElementState.LIGHT:
                if (lightProjectile == null)
                {
                    Debug.LogWarning("No projectile attached, magic will not fire");
                    return;
                }
                if (_shootingCooldownTimer > 0f)
                {
                    return;
                }
                GameObject lightProj = Instantiate(lightProjectile, new Vector3(0, 0, 0), Quaternion.identity);
                lightProj.transform.position = (gameObject.transform.position +  _projectileHeading*0.2f)+ new Vector3(0.0f, 3.0f, 0.0f);
                pProjectileController lightppc = lightProj.GetComponent<pProjectileController>();
                lightppc.SetDirection(_projectileHeading/3);
                _shootingCooldownTimer = shootingCooldown;
                FindObjectOfType<AudioManager>().Play("ShootLight");
                break;
            case ElementState.WATER:
                if (waterProjectile == null)
                {
                    Debug.LogWarning("No projectile attached, magic will not fire");
                    return;
                }
                if (_shootingCooldownTimer > 0f)
                {
                    return;
                }
                GameObject newProj = Instantiate(waterProjectile, new Vector3(0, 0, 0), Quaternion.identity);
                newProj.transform.position = (gameObject.transform.position + _projectileHeading * 0.2f) + new Vector3(0.0f, 3.0f, 0.0f);
                pProjectileController waterppc = newProj.GetComponent<pProjectileController>();
                waterppc.SetDirection(_projectileHeading / 3);
                _shootingCooldownTimer = shootingCooldown;
                FindObjectOfType<AudioManager>().Play("ShootWater");
                break;*/
        
    }
}

