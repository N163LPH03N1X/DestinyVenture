using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStart : MonoBehaviour {

    public Text PressStart;
    public GameObject MainMenu;
    public bool menuStart = true;
    AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        MainMenu.SetActive(false);
       
    }

	
	// Update is called once per frame
	void Update () {
      
        //Cursor.lockState = CursorLockMode.None;
        PressStart.color = new Color(PressStart.color.r, PressStart.color.g, PressStart.color.b, Mathf.PingPong(Time.time, 1));

        if (Input.GetButtonDown("Start") && menuStart || Input.GetMouseButtonDown(0) && menuStart)
        {
            audioSrc.Play();
            PressStart.enabled = false;
            MainMenu.SetActive(true);
            menuStart = false;
        }
    }
}
