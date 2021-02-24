using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIceCollision : MonoBehaviour {
    PlayerStats playST;
    public AudioSource audioSrc;
    public AudioClip playerFrozen;
    MeshRenderer FadeMaterial;
    public float fadePerSecond;
    public float scaleSpeed;
    public float Timer;
    Color color;
    private void Start()
    {
        FadeMaterial = GetComponent<MeshRenderer>();
        //audioSrc.GetComponent<AudioSource>();
        color.a = 0.0f;
    }

    private void Update()
    {
        StartCoroutine(ScaleOverTime(5));
        color = FadeMaterial.material.color;
        FadeMaterial.material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer < 0)
        {
            Timer = 0;
            //Destroy(gameObject);
        }
    }

   
    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(25.0f, 25.0f, 25.0f);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(destinationScale, originalScale, currentTime / time);
            currentTime += Time.deltaTime * scaleSpeed;
            yield return null;
        }
        while (currentTime <= time);

        Destroy(gameObject);
    }
}
