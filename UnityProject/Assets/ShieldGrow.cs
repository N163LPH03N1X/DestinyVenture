using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGrow : MonoBehaviour {
    MeshRenderer FadeMaterial;
    public float fadePerSecond;
    public float scaleSpeed;
    public bool particle;
    Color color;
    private void Start()
    {
        if (!particle)
        {
            FadeMaterial = GetComponent<MeshRenderer>();
            color.a = 0.0f;
        }
       
    }

    private void Update()
    {
        StartCoroutine(ScaleOverTime(5));
        if (!particle)
        {
            color = FadeMaterial.material.color;
            FadeMaterial.material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
        }
        
    }
    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(5.0f, 5.0f, 5.0f);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime * scaleSpeed;
            yield return null;
        }
        while (currentTime <= time);

        Destroy(gameObject);
    }
}
