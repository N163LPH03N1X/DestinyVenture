using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossBow : MonoBehaviour {

    AudioSource audioSrc;
    public AudioClip shoot;
    public AudioClip arrowPickup;
    public Camera CameraPosition;
    public GameObject Player;
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public GameObject whiteScreen;
    GameObject Recoil;
    public float Bullet_Forward_Force;
    public float whiteScreenFlashTimer = 0.1f;
    private float whiteScreenFlashTimerStart;
    public bool whiteScreenFlashEnabled = false;
    public Text ArrowAmmo;
    public GameObject ArrowObject;
    public int Arrows;
    public bool AmmoShot;

    public float fireRate;
    private float nextFire;
    PlayerStats PlayST;
    Recoil recoilComponent;


    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        AmmoShot = true;
        SetCountAmmo();
    }

    // Update is called once per frame
    public void Update()
    {
        StartCoroutine(FindRecoil());

        if (Arrows <= 0)
        {
            Arrows = 0;
            SetCountAmmo();
            AmmoShot = false;
        }
        if (Arrows <= 10)
        {
            ArrowAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        else if (Arrows >= 11)
        {
            ArrowAmmo.color = new Color(1f, 1f, 1f, 1f);
        }


        if (Time.time > nextFire && AmmoShot)
        {
            ArrowObject.SetActive(true);
        }
        else
        {
            ArrowObject.SetActive(false);
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true || Input.GetAxisRaw("Attack") == -1 && Time.time > nextFire && AmmoShot == true)
        {
            Arrows -= 1;
            Rigidbody Temporary_RigidBody;
            GameObject Temporary_Bullet_Handler;
            recoilComponent.StartRecoil(0.1f, -3f, 10f);
            nextFire = Time.time + fireRate;
            audioSrc.PlayOneShot(shoot);

            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left);

            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler, 5.0f);
            SetCountAmmo();
        }

        if (whiteScreenFlashEnabled == true)
        {
            whiteScreen.SetActive(true);
            whiteScreenFlashTimer -= Time.deltaTime;
        }
        if (whiteScreenFlashTimer <= 0)
        {
            whiteScreen.SetActive(false);
            whiteScreenFlashEnabled = false;
            whiteScreenFlashTimer = whiteScreenFlashTimerStart;
        }

    }
    void SetCountAmmo()
    {
        ArrowAmmo.text = Arrows.ToString();
    }
    IEnumerator FindRecoil()
    {
        Recoil = GameObject.Find("/GameControl/Player/PlayerController/MainCamera/Recoil/");
        if (Recoil == null)
        {
            yield return null;
        }
        else
        {
            Recoil = GameObject.Find("/GameControl/Player/PlayerController/MainCamera/Recoil/");
            recoilComponent = Recoil.GetComponent<Recoil>();
        }
    }
    //public void AddArrows(int amount)
    //{
    //    audioSrc.PlayOneShot(arrowPickup);
    //    Arrows += amount;
    //    AmmoShot = true;
    //    whiteScreenFlashEnabled = true;
    //    ArrowObject.SetActive(true);
    //    PlayST = Player.GetComponent<PlayerStats>();
    //    PlayST.GainArrows();
    //    if (Arrows >= 50)
    //    {
    //        Arrows = 50;
    //    }
    //    SetCountAmmo();
    //}
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Arrow"))
    //    {
    //        AddArrows(1);
    //        Destroy(other.gameObject);
    //    }
    //}

}



