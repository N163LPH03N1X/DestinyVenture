using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MykesHeadBobbing : MonoBehaviour {

    private float timer = 0.0f;

    public static float bobbingSpeed;
    public static float bobbingAmount;

    public float bobbingRunSpeed;
    public float bobbingRunAmount;

    public float bobbingWalkSpeed;
    public float bobbingWalkAmount;

    float midpoint = 3f;
    bool isGrounded;
    bool isRunning;
    bool Pulling;
    bool startPulling;
    bool isMoving;
    bool isPaused;
    bool isDrowning;
    bool isDisabled;

    bool SpeedBootsEnabled;
    bool SpikeBootsEnabled;

    private void FixedUpdate()
    {
        isMoving = PlayerController.isMoving;
    }

    void Update()
    {
        isGrounded = PlayerController.isGrounded;
        Pulling = PlayerController.isPulling;
        startPulling = PlayerController.pullBobbing;
        SpeedBootsEnabled = Boots.SpeedBoots;
        isRunning = PlayerController.isRunning;
        SpikeBootsEnabled = Boots.SpikeBoots;
        isPaused = PauseMenu.Paused;
        isDrowning = WaterDamage.isDrowning;
        isDisabled = SceneLoader.isDisabled;

        float waveslice = 1.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        if (SpeedBootsEnabled)
        {
            if (isRunning)
            {
                if (isDrowning)
                {
                    bobbingSpeed = (bobbingRunSpeed / 2) + 0.1f;
                    bobbingAmount = (bobbingRunAmount / 2) +0.12f;
                }
                else
                {
                    bobbingSpeed = bobbingRunSpeed + 0.14f;
                    bobbingAmount = bobbingRunAmount+ 0.11f;
                }
            }
            else if (!isRunning)
            {
                if (isDrowning)
                {
                    bobbingSpeed = (bobbingWalkSpeed / 2) + 0.1f;
                    bobbingAmount = (bobbingWalkAmount / 2) + 0.1f;
                }
                else
                {
                    bobbingSpeed = bobbingWalkSpeed + 0.1f;
                    bobbingAmount = bobbingWalkAmount + 0.1f;
                }
            }
        }
        else if (SpikeBootsEnabled)
        {
            if (isRunning)
            {
                if (isDrowning)
                {
                    bobbingSpeed = bobbingRunSpeed / 4;
                    bobbingAmount = bobbingRunAmount / 4;
                }
                else
                {
                    bobbingSpeed = bobbingRunSpeed / 2;
                    bobbingAmount = bobbingRunAmount / 2;
                }
            }
            else if (!isRunning)
            {
                if (isDrowning)
                {
                    bobbingSpeed = bobbingWalkSpeed / 4;
                    bobbingAmount = bobbingWalkAmount / 4;
                }
                else
                {
                    bobbingSpeed = bobbingWalkSpeed / 2;
                    bobbingAmount = bobbingWalkAmount / 2;
                }
            }
        }
        else
        {
            if (isRunning)
            {
                if (isDrowning)
                {
                    bobbingSpeed = bobbingRunSpeed / 2;
                    bobbingAmount = bobbingRunAmount / 2;
                }
                else
                {
                    bobbingSpeed = bobbingRunSpeed;
                    bobbingAmount = bobbingRunAmount;
                }
            }
            else if (!isRunning)
            {
                if (isDrowning)
                {
                    bobbingSpeed = bobbingWalkSpeed / 2;
                    bobbingAmount = bobbingWalkAmount / 2;
                }
                else
                {
                    bobbingSpeed = bobbingWalkSpeed;
                    bobbingAmount = bobbingWalkAmount;
                }
            }
        }



        if (Pulling)
        {
            if (startPulling)
            {
                Vector3 cSharpConversion = transform.localPosition;
                if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
                {
                    timer = 0.0f;
                }
                else
                {
                    waveslice = Mathf.Sin(timer);
                    timer = timer + bobbingSpeed;
                    if (timer > Mathf.PI * 2)
                    {
                        timer = timer - (Mathf.PI * 2);
                    }
                }
                if (waveslice != 0)
                {
                    float translateChange = waveslice * bobbingAmount;
                    float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                    totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                    translateChange = totalAxes * translateChange;
                    cSharpConversion.y = midpoint + translateChange;
                }
                else
                {
                    cSharpConversion.y = midpoint;
                }

                transform.localPosition = cSharpConversion;
            }
        }

        if (isMoving && !Pulling && !isPaused && isGrounded && !isDisabled)
        {
            Vector3 cSharpConversion = transform.localPosition;
            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + bobbingSpeed;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }
            if (waveslice != 0)
            {
                float translateChange = waveslice * bobbingAmount;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;
                cSharpConversion.y = midpoint + translateChange;
            }
            else
            {
                cSharpConversion.y = midpoint;
            }

            transform.localPosition = cSharpConversion;
        }
    }
}