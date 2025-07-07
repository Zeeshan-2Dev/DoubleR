using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Camera mainCam;
    private Vector3 originalPos;
    private Vector3 velocity;


    [SerializeField] private float smoothSpeed;

    private void Start()
    {
        mainCam = Camera.main;
        originalPos = transform.position - player.position;
    }

    private void LateUpdate()
    {
        Vector3 pos = player.position + originalPos;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothSpeed);
    }
}
