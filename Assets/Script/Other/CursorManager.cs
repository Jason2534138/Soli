using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
    private Vector2 playerScreenPosition;
    private Vector2 mouse;
    private Vector2 diraction;

    [SerializeField] private Texture2D cursor_1;
    [SerializeField] private Texture2D cursor_2;

    private Vector2 cursorHotspot_1;
    private Vector2 cursorHotspot_2;

    private void Awake()
    {
        cursorHotspot_1 = new Vector2(cursor_1.width / 2, cursor_1.height / 2);
        cursorHotspot_2 = new Vector2(cursor_2.width / 2, cursor_2.height / 2);
        Cursor.SetCursor(cursor_1, cursorHotspot_1, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        mouse = Input.mousePosition;
        diraction = (mouse - playerScreenPosition).normalized;
        if (Input.GetMouseButtonDown(0)) StartCoroutine(nameof(ClickCursor));
    }
    // Update is called once per frame

    
    
    private IEnumerator ClickCursor()
    {
        /*
        Quaternion originalRotarion = transform.rotation;

        float angle = Mathf.Atan2(diraction.y, diraction.x) * Mathf.Rad2Deg;
        float angleCorrection;
        

        if (diraction.x > 0)
        {
            angleCorrection = 0f;
        }
        else
        {
            angleCorrection = 180f;
        }
        Quaternion rotation = Quaternion.AngleAxis(angle - angleCorrection, Vector3.forward);
        */
        //transform.rotation = rotation;
        
        Cursor.SetCursor(cursor_2, cursorHotspot_2, CursorMode.ForceSoftware);
        
        
        yield return new WaitForSeconds(0.2f);
        //transform.rotation = originalRotarion;
        Cursor.SetCursor(cursor_1, cursorHotspot_1, CursorMode.ForceSoftware);

    }
}
