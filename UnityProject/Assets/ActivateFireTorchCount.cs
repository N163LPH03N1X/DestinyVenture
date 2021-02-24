using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFireTorchCount : MonoBehaviour {
    public int Count;
    public int ObjectCount;
    public List<Transform> objects;
    AudioSource audioSrc;
    public AudioClip activation;
    bool Activated;
    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();
        objects.Clear();
        foreach (Transform child in transform)
        {
            objects.Add(child);
        }
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {

        Count = ActivateFlameTorch.count;

        if (Count >= ObjectCount && !Activated)
        {
            audioSrc.PlayOneShot(activation);
            objects.Clear();
            foreach (Transform child in transform)
            {
                objects.Add(child);
            }
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].gameObject.SetActive(true);
            }
            Activated = true;
        }
    }
}
