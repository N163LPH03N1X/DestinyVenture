using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public float LookSensitivity;
    public float MoveSensitivity;
    private Rigidbody rb;
    public float SnappingSpeed;
    bool MapPaused;
    Transform playerTransform;
    Vector3 MouseMoveDirection;
    Vector3 MouseStrafeDirection;

    public float MouseMoveSpeed;
    public float MouseStrafeSpeed;
    public bool Invert = true;

    void Start()
    {
        MoveSensitivity = LookSensitivity;
        rb = GetComponent<Rigidbody>();
        

    }

    void Update()
    {
        // Mouse Look Movement ==============================================================================//
        MapPaused = PauseMenu.MapPaused;

        float MouseY = Input.GetAxisRaw("Mouse Y");
        float MouseX = Input.GetAxisRaw("Mouse X");

       
        //=============================================Looking on X Axis ============================================//
        if (MapPaused)
        {
            //Mouse
            if (MouseX > 0)
            {
                this.transform.Rotate(Vector3.down * -MouseX * LookSensitivity * Time.fixedDeltaTime, Space.Self);
            }
            if (MouseX < 0)
            {
                this.transform.Rotate(Vector3.up * MouseX * LookSensitivity * Time.fixedDeltaTime, Space.Self);
               
            }

            //=============================================Looking on Y Axis ============================================//


            //Inversion
            if (Invert)
            {
                // Mouse
                if (MouseY > 0)
                {
                    this.transform.Rotate(Vector3.left * MouseY * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
                if (MouseY < 0)
                {
                    this.transform.Rotate(Vector3.right * -MouseY * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
            }
            //Normal
            else
            {
                //Mouse
                if (MouseY > 0)
                {
                    this.transform.Rotate(Vector3.left * -MouseY * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
                if (MouseY < 0)
                {
                    this.transform.Rotate(Vector3.right * MouseY * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
            }

            // Keyboard Movement =====================================================================================//
            float HorizontalMovement = Input.GetAxisRaw("Horizontal");
            float VerticalMovement = Input.GetAxisRaw("Vertical");
            if (HorizontalMovement > 0)
            {
                if (Input.GetButton("Sprint"))
                {
                    this.transform.Translate(Vector3.left * -HorizontalMovement * LookSensitivity * 2 * Time.fixedDeltaTime, Space.Self);
                }
                else
                {
                    this.transform.Translate(Vector3.left * -HorizontalMovement * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
            }
            if (HorizontalMovement < 0)
            {
                if (Input.GetButton("Sprint"))
                {
                    this.transform.Translate(Vector3.right * HorizontalMovement * LookSensitivity * 2 * Time.fixedDeltaTime, Space.Self);
                }
                else
                {
                    this.transform.Translate(Vector3.right * HorizontalMovement * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
            }
            if (VerticalMovement > 0)
            {
                if (Input.GetButton("Sprint"))
                {
                    this.transform.Translate(Vector3.back * -VerticalMovement * LookSensitivity * 2 * Time.fixedDeltaTime, Space.Self);
                }
                else
                {
                    this.transform.Translate(Vector3.back * -VerticalMovement * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
            }
            if (VerticalMovement < 0)
            {
                if (Input.GetButton("Sprint"))
                {
                    this.transform.Translate(Vector3.forward * VerticalMovement * LookSensitivity * 2 * Time.fixedDeltaTime, Space.Self);
                }
                else
                {
                    this.transform.Translate(Vector3.forward * VerticalMovement * LookSensitivity * Time.fixedDeltaTime, Space.Self);
                }
                
            }


            float Axisz = transform.rotation.eulerAngles.z;
            // 0-45------------------------------------------------------------------------------------
            if (Axisz > 4f && Axisz < 41f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }

            // 45-90-----------------------------------------------------------------------------------
            if (Axisz > 49f && Axisz < 86f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }

            // 90-135----------------------------------------------------------------------------------
            if (Axisz > 94f && Axisz < 131f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }

            // 135 - 180-------------------------------------------------------------------------------
            if (Axisz > 139f && Axisz < 176f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }

            // 180-225---------------------------------------------------------------------------------
            if (Axisz > 184f && Axisz < 221f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }

            // 225-270---------------------------------------------------------------------------------
            if (Axisz > 229f && Axisz < 266f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }

            // 270-315---------------------------------------------------------------------------------
            if (Axisz > 274f && Axisz < 311f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }

            // 315-360---------------------------------------------------------------------------------
            if (Axisz > 319f && Axisz < 356f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.fixedDeltaTime, Space.Self);

            }
        }
    }
}
