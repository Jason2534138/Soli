using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generator : MonoBehaviour, IControllableProp
{
    [SerializeField] private GameObject[] _controllableObject;

    private bool _playerInRange = false;

    private void Update()
    {
        // 只有玩家在範圍內時，才檢查輸入
        if (_playerInRange && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Toggle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = true;
            Debug.Log("Player entered Generator range.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;
            Debug.Log("Player exited Generator range.");
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
