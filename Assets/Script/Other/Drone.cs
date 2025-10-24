using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class DroneMovement : MonoBehaviour
{
    private bool isFacingRight = false;
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField]

    enum State {active, follow};
    State state;

    private Vector3 targetPosition;

    private void Start()
    {
        transform.position = target.position + offset;
    }
    // Update is called once per frame
    void Update()
    {


        switch (state)
        {
            case State.active:
                Active();
                break;
            case State.follow:
                Follow();
                break;
        }
        
    }
    private void Active()
    {

    }
    private void ChangeState()
    {
        if(state == State.active) state = State.follow;
        else if(state == State.follow) state = State.active;
    }
    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        Flip();
    }
    private void Flip()
    {
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector2 mouse = Input.mousePosition;
        Vector2 diraction = (mouse - playerScreenPosition).normalized;
        if (isFacingRight && diraction.x < 0f || !isFacingRight && diraction.x > 0f)
        {
            isFacingRight = !isFacingRight;
            rb.transform.Rotate(0f, 180f, 0f);
            
        }
    }
}
