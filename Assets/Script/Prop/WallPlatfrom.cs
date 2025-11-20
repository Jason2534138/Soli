using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WallPlatfrom : MonoBehaviour, IControllableProp
{
    [SerializeField] private Transform[] _path;
    [SerializeField] private Transform[] _detect;
    private int _detectCurrent = 0;
    private int current = 0;
    private Vector2 _refSpeed = Vector2.zero;
    private float _isColliding;
    [SerializeField]private LayerMask _layerMask;
    Rigidbody2D _rb;
    [SerializeField] private float _speed;
    //rivate bool _isBlocked = false;

    private void Start()
    {
        this.transform.position = _path[current].position;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
       
        if (Vector2.Distance(this.transform.position, _path[current].position) > 0.1f && !Physics2D.OverlapCircle(_detect[_detectCurrent].position, 0.1f, _layerMask))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, _path[current].position, _speed / 10);
        }
        else _rb.velocity = Vector2.zero;
        
    }
    private void ChangePath()
    {
        current += 1;
        if (current >= _path.Length) current = 0;
        _detectCurrent = current + 1;
        if (_detectCurrent >= _detect.Length) _detectCurrent = 0;
    }

    public void Switch()
    {
        ChangePath();
    }
    

}
