using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonPlatfrom2 : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    private int current = 0;
    private Vector2 _refSpeed = Vector2.zero;
    private float _isColliding;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform[] _detect;
    private int _detectCurrent = 0;
    [SerializeField] public Vector2 boxSize;
    //private bool _isBlocked;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        this.transform.position = _path[0].position;
    }
    private void Update()
    {

        if (!Physics2D.OverlapBox(_detect[_detectCurrent].position, boxSize, 0, _layerMask))
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
        current += 1;
        if (current >= _path.Length) current = 0;
        _detectCurrent = current + 1;
        if (_detectCurrent >= _detect.Length) _detectCurrent = 0;
    }
    private void OnDrawGizmos()
    {
        foreach (Transform t in _detect)
        {
            Gizmos.DrawWireCube(t.position, boxSize);
        }
    }

    
}
