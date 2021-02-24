using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<Image>().color = newColor;
            if (t >= 0.9)
            {
                transform.GetComponent<Image>().enabled = false;
            }
            else
            {
                transform.GetComponent<Image>().enabled = true;
            }
            yield return null;
        }

    }
    public void FadeOutScreen()
    {
       
        StartCoroutine(FadeTo(0.0f, 3.0f));
    }
    public void FadeInScreen()
    {
        StartCoroutine(FadeTo(1.0f, 3.0f));
    }
    public void SetAlpha(float alpha)
    {
        Color newColor = new Color(0, 0, 0, alpha);
        transform.GetComponent<Image>().color = newColor;
    }
}