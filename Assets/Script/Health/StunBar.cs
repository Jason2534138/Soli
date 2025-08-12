using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StunBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Slider slider;


    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        slider = transform.Find("Bar").GetComponent<Slider>();
        
    }

   


}
