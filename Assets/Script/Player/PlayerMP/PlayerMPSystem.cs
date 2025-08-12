using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMPSystem
{
    private int _mp;
    private int _mpMax;

    public event EventHandler OnMPChanged;

    public PlayerMPSystem(int MPMax)
    {
        this._mpMax = MPMax;
        _mp = 0;
    }


    #region Health

    public int GetMp()
    {
        return _mp;
    }
    public float GetMPPercent()
    {
        return (float)_mp / _mpMax;
    }
    public void MPDown(int amount)
    {
        _mp -= amount;
        if (_mp <= 0)
        {
            _mp = 0;
        }
        if (OnMPChanged != null) OnMPChanged(this, EventArgs.Empty);

    }
    public void MPUp(int amount)
    {
        _mp += amount;
        if (_mp <= 0)
        {
            _mp = 0;
        }
        if (OnMPChanged != null) OnMPChanged(this, EventArgs.Empty);

    }

    #endregion
}
