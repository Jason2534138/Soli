using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    private void OnEable() 
    {
        CameraManager.Register(GetComponent<CinemachineVirtualCamera>());
    }
    private void OnDisable()
    {
        CameraManager.Unregister(GetComponent<CinemachineVirtualCamera>());
    }
}
