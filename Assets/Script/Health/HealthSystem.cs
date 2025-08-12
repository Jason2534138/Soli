using System;
using System.Diagnostics;

public class HealthSystem 
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnStunChanged;

    public event EventHandler HealthReachZero;
    public event EventHandler IsStuned;
    public event EventHandler IsHit;

    private int health;
    private int healthMax;

    private int stunHealth;
    private int stunHealthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
       

    }

    #region Health
    public int GetHealth()
    {
        return health;
    }
    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) 
        { 
            health = 0;
            HealthReachZero(this, EventArgs.Empty);
        }

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        if (IsHit != null) IsHit(this, EventArgs.Empty);
    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        if(health > healthMax)
        {
            health = healthMax;
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
    #endregion
    
}
