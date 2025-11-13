using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonPlatfrom : MonoBehaviour, IControllableProp
{
    [SerializeField] private Transform[] _path;
    private int current = 0;
    [SerializeField] private float _speed;
    
    [SerializeField] private BoxCollider2D[] _detect;
    private bool _isBlocked;
    private int _blockingObject = 0;

    private void Start()
    {
        this.transform.position = _path[0].position;
        _detect[current].enabled = true;
    }
    private void Update()
    {
        
        _isBlocked = _blockingObject <= 0? false : true;
        if (Vector2.Distance(this.transform.position, _path[current].position) > 0.1f && !_isBlocked) 
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, _path[current].position, 0.1f);
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.gameObject.transform != this.gameObject.transform.parent)
        {
            _blockingObject += 1;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.gameObject.transform != this.gameObject.transform.parent)
        {
            _blockingObject -= 1;
        

        }
    }
    private void ChangePath()
    {
        
        _detect[current].enabled = false;
        current += 1;
        if (current >= _path.Length) current = 0;
        _detect[current].enabled = true;
        _blockingObject = 0;
    }
    

    public void Switch()
    {
        ChangePath();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > this.transform.position.y) collision.transform.parent = this.transform;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.parent == this.transform) collision.transform.parent = null;
    }

}
