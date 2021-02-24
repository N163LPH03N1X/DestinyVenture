using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBobbing : MonoBehaviour
{

    private float timer = 0.0f;
    float bobbingSpeed;
    float bobbingAmount;
    float midpoint = 0.0f;
    bool isJumping;
    bool isPaused;
    bool isDisabled;
    bool isLoading;

    void Update()
    {
        bobbingSpeed = MykesHeadBobbing.bobbingSpeed;
        bobbingAmount = MykesHeadBobbing.bobbingAmount;
        isJumping = PlayerController.isJumping;
        isPaused = PauseMenu.Paused;
        isDisabled = SceneLoader.isDisabled;
        isLoading = SceneLoader.isLoading;

        if (!isJumping && !isPaused && !isDisabled && !isLoading)
        {
            float waveslice = 0.0f;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

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
                totalAxes = Mathf.Clamp(totalAxes, 0.1f, 0.1f);
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