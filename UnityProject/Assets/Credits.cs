using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Credits : MonoBehaviour {

    public float speed;
    bool QuitGame = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isActiveAndEnabled)
        {
            var image = GetComponent<RectTransform>();
            if (image.transform.position.y >= 1620.5 && !QuitGame)
            {
                QuitGame = true;
                Application.Quit();
            }
            else
            {
                image.transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
        }
        
	}
}
