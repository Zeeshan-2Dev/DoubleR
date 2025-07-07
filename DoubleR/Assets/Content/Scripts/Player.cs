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

    private bool rotateLeft = false;
    private bool rotateRight = false;
    [SerializeField] private float smoothRotationSpeed = 0.2f;
    [SerializeField] private float rotationDegY = 15f;
    [SerializeField] private float rotationDegZ = 5f;
    float lrSign;

    [SerializeField] private float smoothMove;
    [SerializeField] private float moveSpeed = 200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        lrSign = -Mathf.Sign(transform.position.x);
    }

    public void Update()
    {
        SwipeLeftRight();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector3.forward * moveSpeed;
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
                if(transform.position.x == rightLane)
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
                if (transform.position.x == leftLane)
                {
                    MoveRight(centerLane);
                }
            }
        }
    }

    private void MoveRight(float lane)
    {
        transform.DOMoveX(lane, smoothMove).OnStart(RotateRight).OnComplete(RotateRight);
    }
    private void MoveLeft(float lane)
    {
        transform.DOMoveX(lane, smoothMove).OnStart(RotateLeft).OnComplete(RotateLeft);
    }

    private void RotateLeft()
    {
        if(rotateLeft)
        {
            transform.DORotate(new Vector3(0, 0, 0), smoothRotationSpeed);
            rotateLeft = false;
        }
        else
        {
            transform.DORotate(new Vector3(0, rotationDegY * lrSign, rotationDegZ * lrSign), smoothRotationSpeed);
            rotateLeft = true;
        }
    }

    private void RotateRight()
    {
        if (rotateRight)
        {
            transform.DORotate(new Vector3(0, 0, 0), smoothRotationSpeed);
            rotateRight = false;
        }
        else
        {
            transform.DORotate(new Vector3(0, -rotationDegY * lrSign, -rotationDegZ * lrSign), smoothRotationSpeed);
            rotateRight = true;
        }
    }
}
