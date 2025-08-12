using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer tr;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float force;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        tr.emitting = true;
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector2 mouse = Input.mousePosition;
        Vector2 diraction = (mouse - playerScreenPosition).normalized;
        Quaternion originalRotarion = transform.rotation;

        rb.velocity = diraction * force;
        float angle = Mathf.Atan2(diraction.y, diraction.x) * Mathf.Rad2Deg;


        Quaternion rotation = Quaternion.AngleAxis(angle - 0f, Vector3.forward);

        this.transform.rotation = rotation;
    }
    
    // Update is called once per frame
    /*void Update()
    {
        //if (Physics2D.OverlapCircle(rb.position, 0.1f, groundLayer)) Destroy(gameObject);
    }*/
}
