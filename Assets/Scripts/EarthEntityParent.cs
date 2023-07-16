using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class EarthEntityParent : MonoBehaviour, IEarthEntities
{
    public GameObject ship;
    public Transform shipPos;
    public Rigidbody2D rb;
    public float dirTimer, audioTimer;
    public bool canSee, beamed, grounded, spinDir;
    public float detectionRange;
    public Vector2 movement, distance, goal, directionVector, moveVector;
    public AudioClip[] beamedClips, passiveClips, spawnClips, spotClips;
    public AudioSource entityAudio;

    public void CanSee()
    {
        int layerMask = 1 << 8;
        if (Physics2D.Raycast(transform.position, distance.normalized, distance.magnitude - 1, layerMask) == false
            && distance.magnitude < detectionRange)
        {
            canSee = true;
        }
        else
        {
            canSee = false;
        }
    }
    public void FindShip()
    {
        shipPos = ship.transform;
        directionVector = (shipPos.position - transform.position);
        distance = directionVector;
        directionVector.Normalize();
    }

    public Vector2 ChooseDir()
    {
        Vector2 dir = new Vector2(UnityEngine.Random.Range(-10, 10), 0);
        dir.x /= 7;
        return dir;
    }

    abstract public void FixedUpdate();

    public void Update()
    {
        /*if (audioTimer < 0)
        {
            PassiveAudio();
            audioTimer = UnityEngine.Random.Range(5, 11);
        } else
        {
            audioTimer -= Time.deltaTime;
        }*/
    }
    abstract public void Move();

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Spaceship").gameObject;
        rb = transform.GetComponent<Rigidbody2D>();
        audioTimer = UnityEngine.Random.Range(5, 11);
    }
    public void BeamedAudio()
    {
        int i = UnityEngine.Random.Range(0, beamedClips.Length);
        entityAudio.PlayOneShot(beamedClips[i], 0.5f);
    }

    public void SpottedAudio()
    {
        int i = UnityEngine.Random.Range(0, spotClips.Length);
        entityAudio.PlayOneShot(spotClips[i], 0.5f);
    }

    public void PassiveAudio()
    {
        int i = UnityEngine.Random.Range(0, passiveClips.Length);
        entityAudio.PlayOneShot(passiveClips[i], 0.5f);
    }

    public void SpawnedAudio()
    {
        int i = UnityEngine.Random.Range(0, spawnClips.Length);
        entityAudio.PlayOneShot(spawnClips[i], 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Beam")
        {
            beamed = true;
            grounded = false;
            BeamedAudio();
            TractorBeamed();
        }
        if (collision.gameObject.name == "Collector")
        {
            Collected();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground placeholder")
        {
            grounded = true;
            dirTimer = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Beam")
        {
            beamed = false;
            moveVector = new Vector2(0, 0);
            //not TractorBeamed();
        }
    }

    public void TractorBeamed()
    {
        //If this method is called it means the cow has been hit by the tractor beam.
        moveVector += directionVector / 15;
        rb.velocity = moveVector;
    }

    abstract public void Collected();
}
