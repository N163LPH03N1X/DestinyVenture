using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOnHover : MonoBehaviour {

    // Grow parameters
    public float approachSpeed = 0.02f;
    public float growthBound = 2f;
    public float shrinkBound = 0.5f;
    private float currentRatio = 1;
    public bool uiImage;
    public bool uiText;
    public bool wasDisabled;
    // The text object we're trying to manipulate
    private Image image;
    private Text text;

    // And something to do the manipulating
    private Coroutine routine;
    private bool keepGoing = true;
    public bool StartUIHover;

    // Attach the coroutine

    private void OnEnable()
    {
        if (wasDisabled)
        {
            if (uiImage)
            {
                this.image = this.gameObject.GetComponent<Image>();
            }
            if (uiText)
            {
                this.text = this.gameObject.GetComponent<Text>();
            }
            if (StartUIHover)
            {
                InitPulse(true);
            }
        }
      
    }
    void Awake()
    {
        if (uiImage)
        {
            this.image = this.gameObject.GetComponent<Image>();
        }
        if (uiText)
        {
            this.text = this.gameObject.GetComponent<Text>();
        }
        if (StartUIHover)
        {
            InitPulse(true);
        }

    }

    IEnumerator PulseImage()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                // Update our text element
                this.image.transform.localScale = Vector3.one * currentRatio;
              

                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (this.currentRatio != this.shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                // Update our text element
                this.image.transform.localScale = Vector3.one * currentRatio;
              

                yield return new WaitForEndOfFrame();
            }
        }
       
    }
    IEnumerator PulseText()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;


                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (this.currentRatio != this.shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;


                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void StartPulse()
    {
        keepGoing = true;
        if (uiImage)
        {
            this.routine = StartCoroutine(this.PulseImage());
        }
        if (uiText)
        {
            this.routine = StartCoroutine(this.PulseText());
        }
        
    }
    public void StopPulse()
    {
        keepGoing = false;
      
    }
    public void InitPulse(bool True)
    {
        if (True)
        {
            keepGoing = true;
            if (uiImage)
            {
                this.routine = StartCoroutine(this.PulseImage());
            }
            if (uiText)
            {
                this.routine = StartCoroutine(this.PulseText());
            }
            True = false;
        }
       
    }
}