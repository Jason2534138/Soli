using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPBar : MonoBehaviour
{
    private PlayerMPSystem playerMPSystem;
    private Slider slider;


    public void Setup(PlayerMPSystem playerMPSystem)
    {
        this.playerMPSystem = playerMPSystem;
        slider = transform.Find("Bar").GetComponent<Slider>();
        playerMPSystem.OnMPChanged += playerMPSystem_OnMPChanged;
    }
    private void playerMPSystem_OnMPChanged(object sender, EventArgs e)
    {
        slider.value = playerMPSystem.GetMPPercent();
    }
}
