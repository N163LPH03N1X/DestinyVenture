using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour{

    Animator anim;
    AudioSource audioSrc;
    public AudioClip BigDoor;
    public int crystalCount;
    public bool openDoor = false;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

    }
  
    private void Update()
    {
        crystalCount = Totem.count;
        if (crystalCount == 3 && !openDoor)
        {
            audioSrc.PlayOneShot(BigDoor);
            OpenFinalDoor(); 
            openDoor = true;
        }
    }
    public void OpenFinalDoor()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("FinalDoorUnlocked");
    }



}
