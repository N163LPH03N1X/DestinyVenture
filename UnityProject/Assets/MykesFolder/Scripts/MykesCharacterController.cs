using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MykesCharacterController : MonoBehaviour {

    public  MykesCharacterController cmd;

    public Transform playerView;
    float xMouseSensitivity = 30.0f;
    float yMouseSensitivity = 30.0f;
    float gravity = 20.0f;
    float friction = 6.0f;
    float moveSpeed = 7.0f;
    float runAcceleration = 14.0f;
    float runDeacceleration = 10.0f;
    float airAcceleration = 2.0f;
    float airDeacceleration = 2.0f; 
    float airControl = 0.3f;  
    float sideStrafeAcceleration = 50f;  
    float sideStrafeSpeed = 1f;
    float jumpSpeed = 8.0f;
    float moveScale = 1.0f;
    GUIStyle style;
    float fpsDisplayRate = 4.0f;
    private float frameCount = 0.0f;
    private float dt = 0.0f;
    private float fps = 0.0f;
    CharacterController controller;
    AudioSource audioSrc;
    public AudioClip jumpSound;
    public bool enableAlwaysRun = false;
    public float rotX = 0.0f;
    public float rotY = 0.0f;
    Vector3 moveDirection = Vector3.zero;
    Vector3 moveDirectionNorm = Vector3.zero;
    Vector3 playerVelocity = Vector3.zero;
    float playerTopVelocity = 0.0f;
    bool grounded = false;
    bool wishJump = false;
    float playerFriction = 0.0f;
    public float forwardmove;
	public float rightmove;
	public float upmove;

    Vector3 playerSpawnPos;
    Quaternion playerSpawnRot;

	void Start () {
        moveScale = gameObject.transform.localScale.y;
      
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        audioSrc = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>(); ;
        cmd = new MykesCharacterController();
        playerSpawnPos = transform.position;
        playerSpawnRot = this.playerView.rotation;
    }
	
	void Update () {

        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0f / fpsDisplayRate)
        {
            fps = Mathf.Round(frameCount / dt);
            frameCount = 0;
            dt -= 1.0f / fpsDisplayRate;
        }
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetMouseButtonDown(0))
                Cursor.lockState = CursorLockMode.Locked;
        }

        rotX -= Input.GetAxis("Mouse Y") * xMouseSensitivity * 0.02f;
        rotY += Input.GetAxis("Mouse X") * yMouseSensitivity * 0.02f;

        if (rotX < -90)
        {
            rotX = -90;
        }
        else if (rotX > 90)
        {
            rotX = 90;
        }

        this.transform.rotation = Quaternion.Euler(0, rotY, 0); // Rotates the collider
        playerView.rotation = Quaternion.Euler(rotX, rotY, 0); // Rotates the camera

        QueueJump();

        if (controller.isGrounded)
        {
            GroundMove();
        } 
        else if (!controller.isGrounded)
        {
            //AirMove();
        }

        controller.Move(playerVelocity * Time.deltaTime);

        var udp = playerVelocity;
        udp.y = 0.0f;

        if (playerVelocity.magnitude > playerTopVelocity)
        {
            playerTopVelocity = playerVelocity.magnitude;
        }

        //if (Input.GetKeyUp(KeyCode.X))
        //{
        //    PlayerExplode();
        //}
            
        //if (Input.GetAxis("Fire1") && isDead)
        //{
        //    PlayerSpawn();
        //}
    }
    public void SetMovementDir()
    {
        cmd.forwardmove = Input.GetAxisRaw("Vertical");
        cmd.rightmove = Input.GetAxisRaw("Horizontal");
    }

    /**
     * Queues the next jump just like in Q3
     */
    public void QueueJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !wishJump)
            wishJump = true;

        if (Input.GetKeyUp(KeyCode.Space))
            wishJump = false;
    }

    /**
     * Execs when the player is in the air
     */
    void AirMove()
    {
        Vector3 wishdir;
        float wishvel = airAcceleration;
        float accel;

        float total;
        float max;
        float scale;

        max = Mathf.Abs(cmd.forwardmove);
        if (Mathf.Abs(cmd.rightmove) > max)
        {
            max = Mathf.Abs(cmd.rightmove);
        }
        if (max < 0)
        {

            max = 0;
        }


        total = Mathf.Sqrt(cmd.forwardmove * cmd.forwardmove + cmd.rightmove * cmd.rightmove);
        scale = moveSpeed * max / (moveScale * total);

        


        SetMovementDir();

        wishdir = new Vector3(cmd.rightmove, 0, cmd.forwardmove);
        transform.TransformDirection(wishdir);

        var wishspeed = wishdir.magnitude;
        wishspeed *= moveSpeed;

        wishdir.Normalize();
        moveDirectionNorm = wishdir;

        wishspeed *= scale;

        // CPM: Aircontrol
        var wishspeed2 = wishspeed;
        if (Vector3.Dot(playerVelocity, wishdir) < 0)
            accel = airDeacceleration;
        else
            accel = airAcceleration;
        // If the player is ONLY strafing left or right
        if (cmd.forwardmove == 0 && cmd.rightmove != 0)
        {
            if (wishspeed > sideStrafeSpeed)
                wishspeed = sideStrafeSpeed;
            accel = sideStrafeAcceleration;
        }

        Accelerate(wishdir, wishspeed, accel);
        if (airControl > 0)
        {
            AirControl(wishdir, wishspeed2);
        }
        playerVelocity.y -= gravity * Time.deltaTime;

    }

    void AirControl(Vector3 wishdir, float wishspeed)
    {
        float zspeed;
        float speed;
        float dot;
        float k;
        

        // Can't control movement if not moving forward or backward
        if (cmd.forwardmove == 0 || wishspeed == 0)
            return;

        zspeed = playerVelocity.y;
        playerVelocity.y = 0;
        /* Next two lines are equivalent to idTech's VectorNormalize() */
        speed = playerVelocity.magnitude;
        playerVelocity.Normalize();

        dot = Vector3.Dot(playerVelocity, wishdir);
        k = 32;
        k *= airControl * dot * dot * Time.deltaTime;

        // Change direction while slowing down
        if (dot > 0)
        {
            playerVelocity.x = playerVelocity.x * speed + wishdir.x * k;
            playerVelocity.y = playerVelocity.y * speed + wishdir.y * k;
            playerVelocity.z = playerVelocity.z * speed + wishdir.z * k;

            playerVelocity.Normalize();
            moveDirectionNorm = playerVelocity;
        }

        playerVelocity.x *= speed;
        playerVelocity.y = zspeed; // Note this line
        playerVelocity.z *= speed;

    }


    public void GroundMove()
    {
        Vector3 wishdir;

        if (!wishJump)
        {
            ApplyFriction(1.0f);
        }

        else
        {
            ApplyFriction(0);
        }
        float total;
        float max;
        float scale;

        max = Mathf.Abs(cmd.forwardmove);
        if (Mathf.Abs(cmd.rightmove) > max)
        {
            max = Mathf.Abs(cmd.rightmove);
        }
        if (max < 0)
        {

            max = 0;
        }


        total = Mathf.Sqrt(cmd.forwardmove * cmd.forwardmove + cmd.rightmove * cmd.rightmove);
        scale = moveSpeed * max / (moveScale * total);

        SetMovementDir();

        wishdir = new Vector3(cmd.rightmove, 0, cmd.forwardmove);
        wishdir = transform.TransformDirection(wishdir);
        wishdir.Normalize();
        moveDirectionNorm = wishdir;

        var wishspeed = wishdir.magnitude;
        wishspeed *= moveSpeed;



        Accelerate(wishdir, wishspeed, runAcceleration);

        // Reset the gravity velocity
        playerVelocity.y = 0;

        if (wishJump)
        {
            playerVelocity.y = jumpSpeed;
            audioSrc.clip = jumpSound;
            audioSrc.Play();
            wishJump = false;
        }
    }
    public void ApplyFriction(float t)
    {
        Vector3 vec = playerVelocity;
        float speed;
        float newspeed;
        float control;
        float drop;

        vec.y = 0.0f;
        speed = vec.magnitude;
        drop = 0.0f;

        if (controller.isGrounded)
        {
            control = speed < runDeacceleration ? runDeacceleration : speed;
            drop = control * friction * Time.deltaTime * t;
        }

        newspeed = speed - drop;
        playerFriction = newspeed;
        if (newspeed < 0)
            newspeed = 0;
        if (speed > 0)
            newspeed /= speed;

        playerVelocity.x *= newspeed;
        // playerVelocity.y *= newspeed;
        playerVelocity.z *= newspeed;

    }
    public void Accelerate(Vector3 wishdir, float wishspeed, float accel)
    {
        float addspeed;
        float accelspeed;
        float currentspeed;

        currentspeed = Vector3.Dot(playerVelocity, wishdir);
        addspeed = wishspeed - currentspeed;
        if (addspeed <= 0)
            return;
        accelspeed = accel * Time.deltaTime * wishspeed;
        if (accelspeed > addspeed)
            accelspeed = addspeed;

        playerVelocity.x += accelspeed * wishdir.x;
        playerVelocity.z += accelspeed * wishdir.z;
    }
    public void LateUpdate()
    {
        if (enableAlwaysRun)
        {
            moveSpeed = 40;
        }
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 40;

        }
        else
        {
            if (!enableAlwaysRun)
            {
                moveSpeed = 20;
            }


        }
    }
}


