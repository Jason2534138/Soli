using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonPlatfrom : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    private int current;
    private Vector2 _refSpeed = Vector2.zero;
    private float _isColliding;
    
    private void Start()
    {
        this.transform.position = _path[0].position;
    }
    private void LateUpdate()
    {
        if (!(_isColliding > 0)) this.transform.position = Vector2.SmoothDamp((Vector2)this.transform.position, (Vector2)_path[current].position, ref _refSpeed, 3f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Prop")) _isColliding -= 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Prop")) _isColliding += 1;
        current += 1;
        if (current >= _path.Length) current = 0;
    }
}
