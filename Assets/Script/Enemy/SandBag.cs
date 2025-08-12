using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBag : MonoBehaviour
{
    private Health health;

    private float recoverTime;
    enum State { Idle, Stuned, Dead};

    State state;

    private bool stateComplete = false;
    //private bool isStuned;
    private bool isDead;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.ZeroHealth += die;
        health.ZeroStun += Stun;
        health.HitStun += Hit;
        

    }
    // Update is called once per frame
    void Update()
    {
        if (stateComplete) SeclectNewState();
        UpdateState();
    }

    private void SeclectNewState()
    {
        stateComplete = false;
        

        if (isDead) state = State.Dead;
        else if (health.isStuned)
        {
            state = State.Stuned;
            health.isStuned = false;
        }
        else if (health.healthSystem.GetHealth() != 0) state = State.Idle;

        Debug.Log(state);
    }

    void UpdateState()
    {
        switch (state)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Stuned:
                UpdateStuned();
                break;
            case State.Dead:
                Destroy(this.gameObject);
                break;
        }
    }

    private void UpdateStuned()
    {
        
        recoverTime += Time.deltaTime;
        if(!health.isStuned) health.isStuned = true;
        
    }

    private void UpdateIdle()
    {
        if (health.healthSystem.GetHealthPercent() == 0) stateComplete = true;
    }
    private void Hit(object sender, EventArgs e)
    {

    }
    private void die(object sender, EventArgs e) 
    {
        isDead = true;  
    }
    private void Stun(object sender, EventArgs e)
    { 
        health.isStuned = true;
    }
}
