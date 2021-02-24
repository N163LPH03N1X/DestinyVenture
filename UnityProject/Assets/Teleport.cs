using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    AudioSource audioSrc;
    public AudioClip portal;
    public Transform Destination;       // Gameobject where they will be teleported to
    public string TagList = "|Player|"; // List of all tags that can teleport

    public void OnTriggerEnter(Collider other)
    {
        // If the tag of the colliding object is allowed to teleport
        if (other.gameObject.CompareTag("Player"))
        {
            audioSrc = GetComponent<AudioSource>();
            audioSrc.PlayOneShot(portal);
            // Update other objects position and rotation
            other.transform.position = Destination.transform.position;
            other.transform.rotation = Destination.transform.rotation;
            other.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 0, 0));
            Destroy(gameObject, 5);
        }
    }
}
