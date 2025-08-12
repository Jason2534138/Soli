using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour
{
    [SerializeField] private float offsetY = 0f;
    private Vector3 offset = new Vector3(0f, 0, -10f);
    private float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private PlayerTeleport playerTeleport;

    private void Start()
    {
        transform.position = target.position + offset;
    }
    // Update is called once per frame
    void Update()
    {
        offset.y = offsetY;
        Vector3 targetPosition = target.position + offset;
        /*if (Input.GetKeyDown(KeyCode.T) && playerTeleport.currentTeleporter != null)
        { 
                
                transform.position = playerTeleport.currentTeleporter.GetComponent<Teleporter>().GetDestination().position + offset * 0.5f;
            
        }*/
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);




    }
}
