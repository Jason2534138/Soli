using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int HP;
    public HealthSystem healthSystem;
    public void SetUp(int Health)
    {
        healthSystem = new HealthSystem(Health);
    }
}
   
