using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private Light2D Light2D;

    private Vector3 offset = new Vector3(0, 0, 0);
    private float smoothTime = 0f;
    private Vector3 velocity = Vector3.zero;
    private bool lightOn = false;
    [SerializeField] private Transform target;
    private void Start()
    {
        transform.position = target.position + offset;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2)) 
        {
            if (lightOn) Light2D.enabled = false;
            else Light2D.enabled = true;

            lightOn = !lightOn;
        }
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector2 mouse = Input.mousePosition;
        Vector2 diraction = (mouse - playerScreenPosition).normalized;
        Quaternion originalRotarion = transform.rotation;

        float angle = Mathf.Atan2(diraction.y, diraction.x) * Mathf.Rad2Deg;
        

        Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        this.transform.rotation = rotation;
    }
}
