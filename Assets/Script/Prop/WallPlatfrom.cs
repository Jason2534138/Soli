using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatfrom : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    private int current;
    private Vector2 _refSpeed = Vector2.zero;
    private float _isColliding;
    Rigidbody2D _rb;
    [SerializeField] private float _speed;

    private void Start()
    {
        this.transform.position = _path[0].position;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) ChangePath();
        if(Vector2.Distance(this.transform.position, _path[current].position) > 0.1f && !(_isColliding > 0))
        {
            Debug.Log("moving");
            Vector2 vel;
            Vector3 dir;
            vel = _rb.velocity;
            dir = (_path[current].position - transform.position).normalized;
            _rb.velocity = _speed * dir;
            Debug.Log(_rb.velocity);
        }else _rb.velocity = Vector2.zero;
        //if (!(_isColliding > 0)) this.transform.position = Vector2.SmoothDamp((Vector2)this.transform.position, (Vector2)_path[current].position, ref _refSpeed, 0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Prop")) _isColliding += 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Prop")) _isColliding -= 1;
    }
    private void ChangePath()
    {
        current += 1;
        if (current >= _path.Length) current = 0;
    }
}
