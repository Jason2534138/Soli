using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonPlatfrom2 : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    private int current = 0;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    [SerializeField] private BoxCollider2D[] _detect;
    private bool _isBlocked;
    private int _blockingObject = 0;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        this.transform.position = _path[0].position;
    }
    private void Update()
    {
        Debug.Log(_blockingObject);
        _isBlocked = _blockingObject > 0 ? true : false;
        if (!_isBlocked)
        {
            if (Vector2.Distance(this.transform.position, _path[current].position) > 0.5f)
            {
                Vector3 dir;
                dir = (_path[current].position - transform.position).normalized;
                //_rb.MovePosition(_rb.position + (Vector2)(dir * _speed * Time.deltaTime));
                _rb.velocity = dir * _speed * Time.deltaTime * 60;
            }else
            {
                _rb.velocity = Vector2.zero;
                ChangePath();
            } 
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void ChangePath()
    {
        _detect[current].enabled = false;
        current += 1;
        if (current >= _path.Length) current = 0;
        _detect[current].enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) _blockingObject += 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) _blockingObject -= 1;
    }
}
