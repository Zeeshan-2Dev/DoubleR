using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float leftLane = -4;
    public const float centerLane = 0;
    public const float rightLane = 4;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private Rigidbody rb;

    [SerializeField] private float smoothMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    public void Update()
    {
        SwipeLeftRight();
    }

    private void SwipeLeftRight()
    {
        //this works only in mobile or unity simulator

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

            if(endTouchPos.x < startTouchPos.x)
            {
                // Swipe Left
                if(transform.position.x == centerLane)
                {
                    MoveLeft(leftLane);
                }
                if(transform.position.x == leftLane)
                {
                    MoveLeft(centerLane);
                }
            }
            if(endTouchPos.x > startTouchPos.x)
            {
                // Swipe Right
                if (transform.position.x == centerLane)
                {
                    MoveRight(rightLane);
                }
                if (transform.position.x == rightLane)
                {
                    MoveRight(centerLane);
                }
            }
        }
    }

    private void MoveRight(float lane)
    {
        transform.DOMoveX(lane, smoothMove);
    }
    private void MoveLeft(float lane)
    {
        transform.DOMoveX(lane, smoothMove);
    }
}
