using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallPlatform2 : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    [SerializeField] private Transform[] _detect;
    private int _detectCurrent = 0;
    private int current = 0;
    private Vector2 _refSpeed = Vector2.zero;
    private float _isColliding;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;
    //rivate bool _isBlocked = false;

    private void Start()
    {
        this.transform.position = _path[current].position;
        
    }
    private void Update()
    {

        if (!Physics2D.OverlapCircle(_detect[_detectCurrent].position, 0.1f, _layerMask))
        {
            if (Vector2.Distance(this.transform.position, _path[current].position) > 0.5f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, _path[current].position, 0.08f);
            }
            else
            {
                
                ChangePath();
            }
        }
        
    }


    private void ChangePath()
    {
        current += 1;
        if (current >= _path.Length) current = 0;
        _detectCurrent = current + 1;
        if (_detectCurrent >= _detect.Length) _detectCurrent = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.position.y > this.transform.position.y) collision.transform.parent = this.transform;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.parent == this.transform) collision.transform.parent = null;
    }
    

}
