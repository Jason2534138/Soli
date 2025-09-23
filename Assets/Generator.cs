using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generator : MonoBehaviour, IControllableProp
{
    [SerializeField] private GameObject[] _controllableObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        foreach (var obj in _controllableObject)
        {
            var ctrl = obj.GetComponent<IControllableProp>();
            if (ctrl == null) ctrl = obj.GetComponentInChildren<IControllableProp>();
            if (ctrl != null)
            {
                ctrl.Switch();
            }
        }
    }

    public void Switch() 
    {
        Toggle();
    }
}
