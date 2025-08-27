using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthSystem healthSystem;

    

    private void Start()
    {
        healthSystem = new HealthSystem(100);
    }
}
   
