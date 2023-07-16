using UnityEngine;

public class Cow_Mars : MonoBehaviour
{
    public Rigidbody2D rb;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;
    Vector3 moveDirection;

    void Awake()
    {
        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = UnityEngine.Random.Range(decisionTime.x, decisionTime.y);
    }

    public void ChooseMoveDirection()
    {
        int randomNumber = UnityEngine.Random.Range(1, 5);
        switch (randomNumber)
        {
            case 1:
                moveDirection = Vector3.up;
                break;
            case 2:
                moveDirection = Vector3.down;
                break;
            case 3:
                moveDirection = Vector3.left;
                break;
            default:
                moveDirection = Vector3.right;
                break;
        }
    }


    public void FixedUpdate()
    {
        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = UnityEngine.Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }
        rb.velocity = moveDirection;
    }
}

