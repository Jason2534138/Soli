using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
    }
  
    void Update()
    {
        if (rb.velocity.x != 0)
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);
            

            sr.color = new Color(r, g, b);
        }else sr.color = Color.white;
    }
}
