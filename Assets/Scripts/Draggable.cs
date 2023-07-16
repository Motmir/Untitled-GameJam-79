using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public AudioSource audioSource;
    private AudioClip[] cowSounds;
    private int randomCowSounds;

    private Vector3 GetMouseWorldPosition()
    {
        // Capture mouse position and return WorldPoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void Start()
    {
        //sndMan = this;
        audioSource = GetComponent<AudioSource>();
        cowSounds = Resources.LoadAll<AudioClip>("cowSounds");
    }


    // when you click
    private void OnMouseDown()
    {
        // Play random sound
        print(cowSounds.Length);
        randomCowSounds = Random.Range(0, cowSounds.Length);
        audioSource.PlayOneShot(cowSounds[randomCowSounds]);
        // capture the mouse offset
        // offset = cows.pos - mouse.pos
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }


    // when you hold and drag
    private void OnMouseDrag() 
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset; 
        
    }
}
