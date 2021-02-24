using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPowerBlock : MonoBehaviour {

    AudioSource audioSrc;
    public AudioClip BlockHit;
    public bool Startsound = false;
    public MeshRenderer meshRend;
    bool On;
    float fadePerSecond = 1.0f;
    Color color;
    bool PowerGauntletsEnabled;
    public float Alpha;
    bool Pushing;
    bool Pulling;
    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();
        meshRend.material.color = new Color(color.r, color.g, color.b, 0.0f);
    }

    // Update is called once per frame
 
    void Update () {

        Alpha = color.a;

        PowerGauntletsEnabled = Gauntlets.PowerGauntlets;
        if (PowerGauntletsEnabled)
        {
            On = PlayerController.ActivatePowBlock;
            if (On)
            {
                color = meshRend.material.color;

                if (color.a > 1)
                {
                    meshRend.material.color = new Color(color.r, color.g, color.b, 1);
                    color.a = 1;
                }
                else if (color.a < 1)
                {
                    meshRend.material.color = new Color(color.r, color.g, color.b, color.a + (fadePerSecond * Time.deltaTime));
                }
            }
            else
            {
                color = meshRend.material.color;

                if (color.a < 0)
                {
                    meshRend.material.color = new Color(color.r, color.g, color.b, 0);
                    color.a = 0;
                }
                else if (color.a > 0)
                {
                    meshRend.material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Wall") &&Startsound))
        {
            audioSrc.PlayOneShot(BlockHit);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Startsound = true;
    
        }
    }
}
