using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Slider slider;
    

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        slider = transform.Find("Bar").GetComponent<Slider>();
        healthSystem.OnHealthChanged += healthSystem_OnHealthChanged;
    }

    private void healthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        slider.value = healthSystem.GetHealthPercent();
    }

    
}
