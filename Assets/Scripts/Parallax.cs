using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
 
{

    // https://www.youtube.com/watch?v=zit45k6CUMk

    [SerializeField] public Sprite[] sprites;
    public int length, depth;
    public GameObject cam;
    public float parallaxEffect;
    private Vector2 camStart;
    private GameObject[] segments;
    public int currentSpace = 0;
    private int shift;
    // Start is called before the first frame update
    void Start()
    {
        camStart = new Vector2(cam.transform.position.x, cam.transform.position.y);
        segments = new GameObject[sprites.Length];
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = new GameObject(sprites[i].name);
            segments[i].AddComponent<SpriteRenderer>();
            segments[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
            segments[i].transform.SetParent(gameObject.transform);
        }
        shift = (int) Mathf.Floor(segments.Length / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        // how far we have moved from the start point 
        float distX = (cam.transform.position.x);
        float distY = (cam.transform.position.y);

        // Looping the background
        // how far we have moved relative to the camera
        //float temp = (dist.x * (1 - parallaxEffect));

        // move background

        float l = length * (currentSpace - 0.5f)* (1/parallaxEffect);
        float r = length * (currentSpace + 0.5f) * (1/parallaxEffect);

        if (distX > r)
        {
            currentSpace++;
        } else if (distX < l)
        {
            currentSpace--;
        }
        for (int i = 0; i < segments.Length; i++)
        {
            int j = (Mathf.Abs(currentSpace) + i) % segments.Length  ;
            float x = cam.transform.position.x + (currentSpace + j - shift) * length - distX*parallaxEffect;
            float y = cam.transform.position.y - distY * parallaxEffect;

            segments[i].transform.position = new Vector3(x, y, depth);
        }
    }
}
