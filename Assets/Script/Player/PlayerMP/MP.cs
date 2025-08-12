using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP : MonoBehaviour
{
    public PlayerMPSystem playerMPSystem;
    public MPBar mpBar;
    // Start is called before the first frame update
    void Start()
    {
        playerMPSystem = new PlayerMPSystem(100);
        mpBar.Setup(playerMPSystem);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            playerMPSystem.MPUp(20);
        }
    }
}
