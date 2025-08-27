using System;
using System.Diagnostics;
using Unity.Burst.CompilerServices;

public class HealthSystem 
{
    public event Action OnHealthChanged;
    public event Action Die;
    public event Action Hit;

    private int health;
    private int healthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    #region Health Functions
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
            Die?.Invoke();
        }

        OnHealthChanged?.Invoke();
        Hit?.Invoke();
    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        if(health > healthMax)
        {
            health = healthMax;
        }
        OnHealthChanged?.Invoke();
    }
    #endregion
        
}
