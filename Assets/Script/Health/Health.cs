using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthSystem healthSystem;
    public HealthBar healthBar;
    public StunBar stunBar;

    Rigidbody2D rb;

    public event EventHandler ZeroHealth;
    public event EventHandler ZeroStun;
    public event EventHandler HitStun;

    public bool isStuned;
    

    
    private void Start()
    {
        healthSystem = new HealthSystem(100);
        rb = GetComponent<Rigidbody2D>();
        if  (stunBar != null)stunBar.Setup(healthSystem);
        healthBar.Setup(healthSystem);
        healthSystem.HealthReachZero += Die;
        healthSystem.IsStuned += Stun;
        healthSystem.IsHit += Hit;

    }
    public void OnHit(int Damage, int Stun, Vector2 knockBack, int onRight)
    {
        
        Vector2 posKnockBack;
        posKnockBack = new Vector2(knockBack.x * onRight, knockBack.y);

        healthSystem.Damage(Damage);
        
        rb.velocity += posKnockBack;
        
    }
    private void Hit(object sender, EventArgs e)
    {
        HitStun(this, EventArgs.Empty);
        
    }
    private void Die(object sender, EventArgs e)
    {
        ZeroHealth(this, EventArgs.Empty);
    }
    private void Stun(object sender, EventArgs e)
    {
        ZeroStun(this, EventArgs.Empty);
    }
}
   
