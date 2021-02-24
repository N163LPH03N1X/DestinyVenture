using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]


public class PlayerController : MonoBehaviour
{
    public static float stamina = 100;
    public static bool StaminaFull;
    public static bool ActivatePowBlock;
    public static bool fallDamage;
    bool reset;
    public float walkSpeed = 6.0f;
    public float pullSpeed = 6.0f;
    public float runSpeed = 11.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float fallingDamageThreshold = 10.0f;
    public float slideSpeed = 12.0f;
    public float antiBumpFactor = .75f;
    public float regenStamTime;
    public float regenSpeed;
    public float decreaseSpeed;
    public float pushForce = 2.0f;
    public float pullForce = 5;
    public float playerSpeed;
    public float speedBootsWalkSpeed;
    public float speedBootsRunSpeed;
    public float spikeBootsWalkSpeed;
    public float spikeBootsRunSpeed;
    public float timePerStep;
    public float stepsPerSecond;
    public float a = 0.24f;
    public float n = 2.0f;
    public float b = 1.68f;
    public float c = 159.0f;
    private float lastFootstepPlayedTime;
    private float velocityPollPeriod = 0.2f; // 5 times a second
    public float PullTimer = 2.0f;
    private float fallStartLevel;
    private float slideLimit;
    private float rayDistance;
    private float currentFootstepsWaitingPeriod;

    public bool stopDrain;
    public bool limitDiagonalSpeed = true;
    public bool toggleRun = false;
    public bool slideWhenOverSlopeLimit = false;
    public bool slideOnTaggedObjects = false;
    public bool airControl = false;
    public bool playerFroze = false;
    public bool blockTerritory = false;
    private bool playerControl = false;
    private bool interactSFX;
    private bool canRun;
    public int antiBunnyHopFactor = 1;
    private int jumpTimer;

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    Transform myTransform;
    RaycastHit hit;
    Rigidbody rb;
    Vector3 contactPoint;
    AudioSource audioSrc;
    AnimatedSword SwordAni;
    Interaction playerInt;
    PlayerStats PlayST;
    Animator anim;
    WeaponSelection WeapSelect;
    Transform thingToPull;
    Vector3 lastPlayerPosition;

    public AudioSource audioSrc2;
    public AudioClip tired;
    public AudioClip Jump;
    public AudioClip footStep;
    public AudioClip footStepWater;
    public AudioClip footStepIce;
    public AudioClip footStepGrass;
    public AudioClip footStepWood;
    public AudioClip footStepMetal;
    public AudioClip fallhurt;
    public AudioClip fallOof;
    public AudioClip land;
    public AudioClip buttonUISFX;
    public AudioClip drag;
    public AudioClip grab;

    public static bool isMoving = false;
    public static bool isGrounded = true;
    public static bool isDraining = false;
    public static bool isRunning = false;
    public static bool isJumping = false;
    public static bool isPulling = false;
    public static bool isPushing = false;
    public static bool pullBobbing = true;

    public bool isMovingForward;
    public bool isMovingBack;
    public bool isIdle;
    public bool isMovingRight;
    public bool isMovingLeft;
    public bool IsRunning;
    public bool isAttacking;
    public bool isDisabled;
    public bool isLoading;
    public bool isDrowning;
    public bool isFalling = false;

    bool water;
    bool ice;
    bool grass;
    bool metal;
    bool wood;
    bool BroadSwordActive;
    bool MagicSwordActive;
    bool ThunderSwordActive;
    bool IceSwordActive;
    bool dragSound;
    bool grabSound;
    bool startPulling;
    public GameObject SwordModel;

    bool SpeedBootsEnabled;
    bool SpikeBootsEnabled;
    bool StrengthGauntletsEnabled;
    bool PowerGauntletsEnabled;
    bool GripGauntletsEnabled;
    public bool FlameBreastActive;
    public bool FlameBreastEnabled;
    public bool PhantomBreastActive;
    public bool FrostBreastActive;
    bool UnarmedEnabled;
    public bool ActivateBreast;

    public bool isAnimating;

    void Awake()
    {
        isGrounded = true;
        anim = GetComponent<Animator>();
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
    private void Start()
    {
        InvokeRepeating("EstimatePlayerVelocity_InvokeRepeating", 1.0f, velocityPollPeriod);

        WeapSelect = GetComponent<WeaponSelection>();
        controller = GetComponent<CharacterController>();
        PlayST = GetComponent<PlayerStats>();
        playerInt = GetComponent<Interaction>();
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        SwordAni = SwordModel.GetComponent<AnimatedSword>();
        playerSpeed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;
        myTransform = transform;
        canRun = true;
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void Update()
    {
        

        SpikeBootsEnabled = Boots.SpikeBoots;
        isDisabled = SceneLoader.isDisabled;
        isLoading = SceneLoader.isLoading;
        isAttacking = AnimatedSword.isAttacking;
        isDrowning = WaterDamage.isDrowning;
        SpeedBootsEnabled = Boots.SpeedBoots;
        StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
        PowerGauntletsEnabled = Gauntlets.PowerGauntlets;
        GripGauntletsEnabled = Gauntlets.GripGauntlets;
        isAnimating = AnimatedSword.isAnimating;
        UnarmedEnabled = WeaponSelection.UnarmedEnabled;
       

        Regenerate();

        if (Input.GetAxis("Horizontal") > 0)
        {
            SwitchMovement(MovementType.right);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            SwitchMovement(MovementType.left);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            SwitchMovement(MovementType.forward);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            SwitchMovement(MovementType.back);
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            SwitchMovement(MovementType.none);
        }
        if (Time.time - lastFootstepPlayedTime > currentFootstepsWaitingPeriod && isMoving && isGrounded && !isJumping)
        {
            isDrowning = WaterDamage.isDrowning;
            if (!isDrowning)
            {
                if (isRunning)
                {
                    audioSrc.volume = Random.Range(0.5f, 1.0f);
                    audioSrc.pitch = Random.Range(1.8f, 2.1f);
                    SetFootStep();
                }
                else
                {
                    audioSrc.volume = Random.Range(0.3f, 0.6f);
                    audioSrc.pitch = Random.Range(0.6f, 0.8f);
                    SetFootStep();
                }
            }
            else
                audioSrc.clip = null;
            audioSrc.Play();
            lastFootstepPlayedTime = Time.time;
        }
        if (isPulling)
        {
            if (PullTimer > 0)
            {
                PullTimer -= Time.deltaTime;
            }
            if (PullTimer < 0)
            {
                PullTimer = 2;
            }
            if (PullTimer < 1)
            {
                if (dragSound)
                {
                    audioSrc.PlayOneShot(drag);
                    anim.SetBool("Idle", false);
                    anim.SetTrigger("Pull");
                    dragSound = false;
                }
                startPulling = true;
                pullSpeed = 3;
                playerSpeed = pullSpeed;
            }
            else if (PullTimer > 1)
            {
                startPulling = false;
                dragSound = true;
                pullSpeed = 0;
                playerSpeed = pullSpeed;
              
            }
        }
        else if (!isPulling && !isPushing && !isLoading && !isDisabled)
        {

            if (Input.GetButton("Sprint") && canRun && !playerFroze && isMoving && isGrounded)
            {
                PowerGauntletsEnabled = Gauntlets.PowerGauntlets;
                SpeedBootsEnabled = Boots.SpeedBoots;
                isDrowning = WaterDamage.isDrowning;
                StaminaFull = false;
                isRunning = true;

                if (SpeedBootsEnabled && !isDrowning)
                    playerSpeed = speedBootsRunSpeed;
                else if (SpeedBootsEnabled & isDrowning)
                    playerSpeed = speedBootsRunSpeed / 2;
                else if (SpikeBootsEnabled & !isDrowning)
                    playerSpeed = spikeBootsRunSpeed;
                else if (SpikeBootsEnabled & isDrowning)
                    playerSpeed = spikeBootsRunSpeed / 2;
                else if (isDrowning && !SpeedBootsEnabled)
                    playerSpeed = runSpeed / 2;
                else
                    playerSpeed = runSpeed;
            }
            else
            {
                isRunning = false;
                if (SpeedBootsEnabled && !isDrowning)
                    playerSpeed = speedBootsWalkSpeed;
                else if (SpeedBootsEnabled & isDrowning)
                    playerSpeed = speedBootsWalkSpeed / 2;
                else if (SpikeBootsEnabled & !isDrowning)
                    playerSpeed = spikeBootsWalkSpeed;
                else if (SpikeBootsEnabled & isDrowning)
                    playerSpeed = spikeBootsWalkSpeed / 2;
                else if (isDrowning && !SpeedBootsEnabled)
                    playerSpeed = walkSpeed / 2;
                else
                    playerSpeed = walkSpeed;
            }
        }
        if (Input.GetButton("Grab") && blockTerritory && !isMovingLeft && !isMovingRight && !isJumping && isGrounded && !isDisabled && !isAnimating)
        {
            isAttacking = AnimatedSword.isAttacking;
            if (!isAttacking)
            {
                WeapSelect.EnableWeapon(5);
                if (grabSound)
                {
                    reset = false;
                    audioSrc.PlayOneShot(grab);
                    grabSound = false;
                }
                if (thingToPull != null)
                {
                    Vector3 D = transform.position - thingToPull.transform.position;
                    float dist = D.magnitude;
                    Vector3 pullDir = D.normalized;
                    if (dist > 0)
                    {
                        ActivatePowBlock = true;
                        if (PullTimer > 1)
                        {
                            thingToPull.GetComponent<Rigidbody>().velocity += pullDir * (pullForce * Time.deltaTime);
                        }

                    }

                    if (isMovingBack && !isPulling)
                    {
                        anim.SetBool("Idle", true);
                        anim.Rebind();
                        isPulling = true;
                        PullTimer = 2;
                    }
                    else if (isMovingForward)
                    {
                        isPushing = true;
                    }
                }
            }
        }
        else if (Input.GetButtonUp("Grab") && !isDisabled && !isAnimating && !isAttacking || isIdle && !reset && !isDisabled && !isAnimating && !isAttacking)
        {

            isPushing = false;
            anim.SetBool("Idle", true);
            anim.Rebind();
            ActivatePowBlock = false;
            grabSound = false;
            isPulling = false;
            PullTimer = 0;
            thingToPull = null;
            reset = true;

            if (SpeedBootsEnabled && !isDrowning)
                playerSpeed = speedBootsWalkSpeed;
            else if (SpeedBootsEnabled & isDrowning)
                playerSpeed = speedBootsWalkSpeed / 2;
            else if (SpikeBootsEnabled & !isDrowning)
                playerSpeed = spikeBootsWalkSpeed;
            else if (SpikeBootsEnabled & isDrowning)
                playerSpeed = spikeBootsWalkSpeed / 2;
            else if (isDrowning && !SpeedBootsEnabled)
                playerSpeed = walkSpeed / 2;
            else
                playerSpeed = walkSpeed;

        }
        if (Input.GetButtonUp("Grab") && !isDisabled && !isAnimating && !isAttacking)
        {
            RenableSwords();
        }
    }
    public void Regenerate()
    {
        isLoading = SceneLoader.isLoading;
        ActivateBreast = ElementDamage.activateBreast;
        FrostBreastActive = Breast.FrostBreastActive;
        PhantomBreastActive = Breast.PhantBreastActive;
        FlameBreastActive = Breast.FlameBreastActive;
        FlameBreastEnabled = Breast.FlameBreast;
        if (canRun || !isLoading)
        {
           
            if (!isRunning && !ActivateBreast && !PhantomBreastActive)
            {
                
                if (!isDraining && !FlameBreastEnabled)
                {

                    stamina += Time.deltaTime * regenSpeed;
                    if (stamina >= 100)
                    {
                        StaminaFull = true;
                        stamina = 100;
                    }
                }
                else if (FlameBreastEnabled)
                {
                    stamina += Time.deltaTime / 4;
                    if (stamina >= 100)
                    {
                        StaminaFull = true;
                        stamina = 100;
                    }
                }
            }
            else if(isRunning)
            {
                if (PowerGauntletsEnabled)
                    stamina -= Time.deltaTime * decreaseSpeed / 2;
                else
                    stamina -= Time.deltaTime * decreaseSpeed;

                if (stamina < 0)
                {
                    stamina = 0;
                    regenStamTime += 2;
                    audioSrc2.PlayOneShot(tired);
                    canRun = false;
                }
            }
            else if (FlameBreastActive && ActivateBreast)
            {
                if (PowerGauntletsEnabled)
                    stamina -= Time.deltaTime * decreaseSpeed / 4;
                else
                    stamina -= Time.deltaTime * decreaseSpeed/ 2;
                if (stamina < 0)
                {
                    stamina = 0;
                    regenStamTime += 5;
                    audioSrc2.PlayOneShot(tired);
                    canRun = false;
                }
            }
            else if (PhantomBreastActive)
            {
                if (stamina > 0)
                {
                    if (PowerGauntletsEnabled)
                        stamina -= Time.deltaTime * decreaseSpeed;
                    else
                        stamina -= Time.deltaTime * decreaseSpeed * 2;
                }
                else if (stamina < 0)
                {
                    stamina = 0.1f;
                    regenStamTime += 5;
                    audioSrc2.PlayOneShot(tired);
                    canRun = false;
                }
            }
            else if (FrostBreastActive && ActivateBreast)
            {
                if (PowerGauntletsEnabled)
                    stamina -= Time.deltaTime * decreaseSpeed / 4;
                else
                    stamina -= Time.deltaTime * decreaseSpeed / 2;
                if (stamina < 0)
                {
                    stamina = 0;
                    regenStamTime += 5;
                    audioSrc2.PlayOneShot(tired);
                    canRun = false;
                }
            }

        }
       
        if (regenStamTime > 0)
        {
            regenStamTime -= Time.deltaTime;
        }
        else if (regenStamTime < 0)
        {
            regenStamTime = 0;
            canRun = true;
        }
    }
    public IEnumerator ReductionStam()
    {
        if (stamina > 0.1)
        {
            for (int t = 100; t > 0; t--)
            {
                if (stamina <= 1)
                {
                    StartCoroutine(ReductionHealth());
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                    stamina -= 1;
                    if (t <= 0.1 || stopDrain)
                    {
                        t = 100;
                        break;
                    }
                }

            }
        }
    }
    public IEnumerator ReductionHealth()
    {
        for (float t = 100; t > 0; t--)
        {
            yield return new WaitForSeconds(0.1f);
            PlayST.doDamage(1);
            if (t <= 0.1 || stopDrain)
            {
                t = 100;
                break;
            }
        }
    }
    public void reducePlayer(bool True)
    {
        if (True)
        {
            isDraining = true;
            stopDrain = false;
            StartCoroutine(ReductionStam());
        }
        else
        {
            isDraining = false;
            stopDrain = true;
        }
    }
    public void ResetStam()
    {
        stamina += 100;
        regenStamTime = 0;
        canRun = true;
        if (stamina >= 100)
        {
            stamina = 100;
        }
    }
    public void GivePlayerStamina(int amount)
    {
        stamina += amount;
        if (stamina >= 100)
        {
            StaminaFull = true;
            stamina = 100;
        }
    }
    public void RemovePlayerStamina(float amount)
    {
        StaminaFull = false;
        stamina -= amount;
        if (stamina <= 0)
        {
            regenStamTime += 5;
            stamina = 0;
            canRun = false;
        }
    }
    void playJumpAnimation(bool True)
    {
        if (True && !isDisabled && !UnarmedEnabled)
        {
            SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Jump, true);
        }
        else
        {
            if (!isDisabled && !UnarmedEnabled)
            { 
                SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Jump, false);
            }
        }
    }
    void playFallAnimation(bool True)
    {
        if (True && !isDisabled && !UnarmedEnabled)
        {
            SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Fall, true);
        }
        else
        {
            if (!isDisabled && !UnarmedEnabled)
            {
                SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Fall, false);
            }
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isJumping = false;
        if (!fallDamage)
        {
            isDrowning = WaterDamage.isDrowning;
            if (isJumping && !isDrowning)
            {
                audioSrc.clip = land;
                audioSrc.volume = Random.Range(0.3f, 0.3f);
                audioSrc.pitch = Random.Range(0.2f, 1.1f);
                audioSrc.Play();
                playJumpAnimation(false);

            }
            else if (!isGrounded && !isDrowning)
            {
                audioSrc.clip = land;
                audioSrc.volume = Random.Range(0.05f, 0.2f);
                audioSrc.pitch = Random.Range(0.2f, 1.1f);
                audioSrc.Play();
                playJumpAnimation(false);
            }
        }
        if (StrengthGauntletsEnabled)
        {
            if (hit.gameObject.CompareTag("StrengthBlockSwitch") && isPushing && !isMovingLeft && !isMovingRight)
            {

                anim.SetBool("Idle", false);
                anim.SetTrigger("Push");
                ActivatePowBlock = true;
                playerInt.EnableXUI(false);
                Rigidbody body = hit.collider.attachedRigidbody;
                if (body == null || body.isKinematic)
                    return;

                if (hit.moveDirection.y < -0.3F)
                    return;

                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                body.velocity = pushDir * pushForce;
            }

            if (hit.gameObject.CompareTag("StrengthBlockSwitch"))
            {
                thingToPull = hit.transform;
            }
        }
        if (PowerGauntletsEnabled)
        {

            if (hit.gameObject.CompareTag("PowerBlockSwitch") && isPushing && !isMovingLeft && !isMovingRight)
            {
                anim.SetBool("Idle", false);
                anim.SetTrigger("Push");
                ActivatePowBlock = true;
                playerInt.EnableXUI(false);
                Rigidbody body = hit.collider.attachedRigidbody;
                if (body == null || body.isKinematic)
                    return;

                if (hit.moveDirection.y < -0.3F)
                    return;

                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                body.velocity = pushDir * pushForce;
            }

            if (hit.gameObject.CompareTag("PowerBlockSwitch"))
            {
                thingToPull = hit.transform;
            }
        }
        if (GripGauntletsEnabled)
        {
            if (hit.gameObject.CompareTag("GripBlockSwitch") && isPushing && !isMovingLeft && !isMovingRight)
            {
                anim.SetBool("Idle", false);
                anim.SetTrigger("Push");
                ActivatePowBlock = true;
                playerInt.EnableXUI(false);
                Rigidbody body = hit.collider.attachedRigidbody;
                if (body == null || body.isKinematic)
                    return;

                if (hit.moveDirection.y < -0.3F)
                    return;

                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                body.velocity = pushDir * pushForce;


            }

            if (hit.gameObject.CompareTag("GripBlockSwitch"))
            {
                thingToPull = hit.transform;
            }
        }
        if (SpikeBootsEnabled)
        {
            if (hit.gameObject.CompareTag("IceFloor"))
                SwitchGround(GroundType.metal);
            else if (hit.gameObject.CompareTag("Grass"))
                SwitchGround(GroundType.metal);
            else if (hit.gameObject.CompareTag("Metal"))
                SwitchGround(GroundType.metal);
            else if (hit.gameObject.CompareTag("Wood"))
                SwitchGround(GroundType.metal);
            else
                SwitchGround(GroundType.metal);
        }
        else
        {
            if (hit.gameObject.CompareTag("IceFloor"))
                SwitchGround(GroundType.ice);
            else if (hit.gameObject.CompareTag("Grass"))
                SwitchGround(GroundType.grass);
            else if (hit.gameObject.CompareTag("Metal"))
                SwitchGround(GroundType.metal);
            else if (hit.gameObject.CompareTag("Wood"))
                SwitchGround(GroundType.wood);
            else
                SwitchGround(GroundType.none);
        }
       
        contactPoint = hit.point;
    }
    void FallingDamageAlert(float fallDistance)
    {
        fallDamage = true;
        if (fallDistance > 25)
        {
            audioSrc2.PlayOneShot(fallhurt);
            PlayST.doDamage(fallDistance - 25);
        }
        else if(fallDistance > 3 && fallDistance < 25)
            audioSrc2.PlayOneShot(fallOof);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
            SwitchGround(GroundType.water);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("StrengthBlock") && !isPulling && !isAttacking && !isJumping && !blockTerritory)
        {
            StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
            if (StrengthGauntletsEnabled)
            {
                BlockInteraction(true);
            }
            else if (PowerGauntletsEnabled)
            {
                BlockInteraction(false);
            }
            else if (GripGauntletsEnabled)
            {
                BlockInteraction(false);
            }
        }
        if (other.gameObject.CompareTag("PowerBlock") && !isPulling && !isAttacking && !isJumping && !blockTerritory)
        {
            PowerGauntletsEnabled = Gauntlets.PowerGauntlets;
            if (PowerGauntletsEnabled)
            {
                BlockInteraction(true);
            }
            else
            {
                BlockInteraction(false);
            }
        }
        if (other.gameObject.CompareTag("GripBlock") && !isPulling && !isAttacking && !isJumping && !blockTerritory)
        {
            GripGauntletsEnabled = Gauntlets.GripGauntlets;
            if (GripGauntletsEnabled)
            {
                BlockInteraction(true);
            }
            else
            {
                BlockInteraction(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            water = false;
        }
        if (other.gameObject.CompareTag("StrengthBlock"))
        {
            BlockInteraction(false);
        }
        if (other.gameObject.CompareTag("PowerBlock"))
        {
            BlockInteraction(false);
        }
        if (other.gameObject.CompareTag("GripBlock"))
        {
            BlockInteraction(false);
        }

    }
    public void MovePlayer()
    {
        isDisabled = SceneLoader.isDisabled;
        isLoading = SceneLoader.isLoading;
        if (!isLoading && !isDisabled)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;


            if (isGrounded)
            {
                fallDamage = false;
                bool sliding = false;
                if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance))
                {
                    if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                        sliding = true;
                }

                else
                {
                    Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                    if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                        sliding = true;
                }

                if (isFalling)
                {
                    playFallAnimation(false);
                    isFalling = false;
                    if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
                        FallingDamageAlert(fallStartLevel - myTransform.position.y);
                }

                
                if ((sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide"))
                {
                    Vector3 hitNormal = hit.normal;
                    moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                    Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                    moveDirection *= slideSpeed;
                    playerControl = false;
                }

                else
                {
                    moveDirection = new Vector3(inputX, -antiBumpFactor, inputY);
                    moveDirection = myTransform.TransformDirection(moveDirection) * playerSpeed;
                    playerControl = true;
                }
                
                if (!Input.GetButton("Jump"))
                {
                    jumpTimer++;
                }
                   
                else if (jumpTimer >= antiBunnyHopFactor && !isPulling && !isPushing)
                {
                    playJumpAnimation(true);
                    isJumping = true;
                    if (isDrowning)
                    {
                        moveDirection.y = jumpSpeed / 2;
                    }
                    else
                    {
                        audioSrc2.clip = Jump;
                        audioSrc2.volume = Random.Range(0.8f, 1.0f);
                        audioSrc2.pitch = Random.Range(0.8f, 1.0f);
                        audioSrc2.PlayOneShot(Jump);
                        moveDirection.y = jumpSpeed;
                    }
                    jumpTimer = 0;
                }
            }
            else
            {

                if (!isFalling && !isDisabled)
                {
                    playFallAnimation(true);
                    if (isDrowning)
                    {
                        gravity = 5;
                    }
                    else
                    {
                        gravity = 20;
                    }
                    isFalling = true;
                    fallStartLevel = myTransform.position.y;
                }

                if (airControl && playerControl)
                {

                    moveDirection.x = inputX * playerSpeed * inputModifyFactor;
                    moveDirection.z = inputY * playerSpeed * inputModifyFactor;
                    moveDirection = myTransform.TransformDirection(moveDirection);
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            isGrounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
        }
    }
    public void EstimatePlayerVelocity_InvokeRepeating()
    {
        float distanceMagnitude = (transform.position - lastPlayerPosition).magnitude;
        lastPlayerPosition = transform.position;
        float estimatedPlayerVelocity = distanceMagnitude / velocityPollPeriod;
        if (estimatedPlayerVelocity < 15.0f)
        {
            currentFootstepsWaitingPeriod = Mathf.Infinity;
            return;
        }

        if (SpikeBootsEnabled)
        {
            float mappedPlayerSpeed = estimatedPlayerVelocity / 6.0f;
            stepsPerSecond = ((a * Mathf.Pow(mappedPlayerSpeed, n)) + (b * mappedPlayerSpeed) + c) / 60.0f;
            timePerStep = (1.0f / stepsPerSecond);
            currentFootstepsWaitingPeriod = timePerStep;
        }
        else
        {
            float mappedPlayerSpeed = estimatedPlayerVelocity / 5.0f; //Convert the speed so that walking speed is about 6
            stepsPerSecond = ((a * Mathf.Pow(mappedPlayerSpeed, n)) + (b * mappedPlayerSpeed) + c) / 60.0f;
            timePerStep = (1.0f / stepsPerSecond);
            currentFootstepsWaitingPeriod = timePerStep;
        }
            
    }
    public void BlockInteraction(bool on)
    {
        if (on)
        {
            blockTerritory = true;
            playerInt = GetComponent<Interaction>();
            if (!interactSFX)
            {
                audioSrc.PlayOneShot(buttonUISFX);
                interactSFX = true;
            }
            playerInt.EnableXUI(true);

        }
        else
        {
            blockTerritory = false;
            ActivatePowBlock = false;
            playerInt = GetComponent<Interaction>();
            playerInt.EnableXUI(false);
            interactSFX = false;
        }
    }
    public enum MovementType { forward, back, left, right, none, push, pull}
    public enum GroundType { water, wood, metal, ice, none, grass }
    public void SwitchMovement(MovementType type)
    {
        switch (type)
        {
            case MovementType.forward:
                {
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingForward = true;
                    isMovingBack = false;
                    break;
                }
            case MovementType.back:
                {
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingForward = false;
                    isMovingBack = true;
                    break;
                }
            case MovementType.left:
                {
                    isPushing = false;
                    isPulling = false;
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = true;
                    isMovingRight = false;
                    isMovingForward = false;
                    isMovingBack = false;
                    break;
                }
            case MovementType.right:
                {
                    isPushing = false;
                    isPulling = false;
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = false;
                    isMovingRight = true;
                    isMovingForward = false;
                    isMovingBack = false;
                    break;
                }
            case MovementType.none:
                {
                    isIdle = true;
                    isMoving = false;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingForward = false;
                    isMovingBack = false;
                    break;
                }
            case MovementType.push:
                {
                    anim.SetBool("Idle", false);
                    anim.SetTrigger("Push");
                    playerInt.EnableXUI(false);
                    isPulling = false;
                    isPushing = true;
                    break;
                }
            case MovementType.pull:
                {
                    isPushing = false;
                    isPulling = true;
                    playerInt.EnableXUI(false);
                    PullTimer = 2;
                    break;
                }
        }
    }
    public void SwitchGround(GroundType type)
    {
        switch (type)
        {
            case GroundType.water:
                {
                    water = true;
                    wood = false;
                    metal = false;
                    grass = false;
                    ice = false;
                    break;
                }
            case GroundType.wood:
                {
                    water = false;
                    wood = true;
                    metal = false;
                    grass = false;
                    ice = false;
                    break;
                }
            case GroundType.metal:
                {
                    water = false;
                    wood = false;
                    metal = true;
                    grass = false;
                    ice = false;
                    break;
                }
            case GroundType.ice:
                {
                    water = false;
                    wood = false;
                    metal = false;
                    grass = false;
                    ice = true;
                    break;
                }
            case GroundType.none:
                {
                    water = false;
                    wood = false;
                    metal = false;
                    grass = false;
                    ice = false;
                    break;
                }
            case GroundType.grass:
                {
                    water = false;
                    wood = false;
                    metal = false;
                    grass = true;
                    ice = false;
                    break;
                }
        }
    }
    public void SetFootStep()
    {
        if (ice)
            audioSrc.clip = footStepIce;
        else if (grass)
            audioSrc.clip = footStepGrass;
        else if (water)
            audioSrc.clip = footStepWater;
        else if (wood)
            audioSrc.clip = footStepWood;
        else if (metal)
            audioSrc.clip = footStepMetal;
        else
            audioSrc.clip = footStep;
    }
    public void RenableSwords()
    {
        BroadSwordActive = PowerCounter.BroadSword;
        MagicSwordActive = PowerCounter.MagicSword;
        ThunderSwordActive = PowerCounter.ThunderSword;
        IceSwordActive = PowerCounter.IceSword;
        if (BroadSwordActive)
        {
            SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Rebind, true);
            WeapSelect.EnableWeapon(1);
        }
        if (MagicSwordActive)
        {
            SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Rebind, true);
            WeapSelect.EnableWeapon(2);
        }
        if (ThunderSwordActive)
        {
            SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Rebind, true);
            WeapSelect.EnableWeapon(3);
        }
        if (IceSwordActive)
        {
            SwordAni.SelectAnimation(AnimatedSword.PlayerAnimation.Rebind, true);
            WeapSelect.EnableWeapon(4);
        }
    }
}
