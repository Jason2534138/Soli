using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    public HealthBar healthBar;
    public StunBar stunBar;

    Rigidbody2D rb;

    public event EventHandler ZeroHealth;
    public event EventHandler ZeroStun;
    

    public bool isStuned;

    //public IntVariable health, maxHealth;
    
    private void Start()
    {
        healthSystem = new HealthSystem(100);
        rb = GetComponent<Rigidbody2D>();
        if  (stunBar != null)stunBar.Setup(healthSystem);
        healthBar.Setup(healthSystem);
        healthSystem.HealthReachZero += Die;
        healthSystem.IsStuned += Stun;
        

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
   
