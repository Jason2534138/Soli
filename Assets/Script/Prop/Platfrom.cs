using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] path;
    private int currentPoint;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentPoint = 0;
        this.transform.position = path[currentPoint].position;
        Debug.Log(path.Length);
    }

    // Update is called once per frame
    void Update()
    {
        ChangePoint();
        Vector2 dir = path[currentPoint].position - transform.position;
        rb.velocity = dir.normalized * _speed * Time.deltaTime;
    }
    private void ChangePoint()
    {
        if (Vector2.Distance(path[currentPoint].position, transform.position) < 0.1f) currentPoint += 1;
        if(currentPoint >= path.Length) currentPoint = 0;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("hit");
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.
    //    }
    //}
}
